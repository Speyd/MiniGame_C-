using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using static BreakManager;

Console.OutputEncoding = Encoding.Unicode;

Map map = new Map(7, 7);
Player newPlayer = new Player(100, new List<Type>(new Type[] { typeof(Weapon) }));
newPlayer.getInventory().addItem(new Axe(20, 1, 12));
newPlayer.getInventory().addItem(new Apple(51,  57));

UnitManager unitManager = new UnitManager(map);

unitManager.addUnit(newPlayer, 3, 3);
unitManager.addUnit(new Tree(50, new List<Type>(new Type[] { typeof(Axe) })), 0, 0);
unitManager.addUnit(new Stone(20, new List<Type>(new Type[] { typeof(Weapon) })), 4, 1);
map.showMap();

if(map.getElement(3, 3) is Player player)
{
    Console.WriteLine($"Unit: {player.FullName}");
    Console.WriteLine($"Y: {player.Y}");
    Console.WriteLine($"X: {player.X}");
    Console.WriteLine($"Hp: {player.Hp}");
    Console.WriteLine($"Vulnerabilities:\n{player.showVulnerabilities()}");
    Console.Write("Inventory:\n"); player.getInventory().showInventory();
    for(int i = 0; i < player.getInventory().getSize(); i++)
    {
        Console.WriteLine($"{i + 1}. {player.getInventory().getFullInformationItem(i)}");
    }
}

if (map.getElement(0, 0) is Tree tree)
{
    Console.WriteLine($"Unit: {tree.FullName}");
    Console.WriteLine($"Y: {tree.Y}");
    Console.WriteLine($"X: {tree.X}");
    Console.WriteLine($"Hp: {tree.Hp}");
    Console.WriteLine($"Vulnerabilities:\n{tree.showVulnerabilities()}");
}

if (map.getElement(4, 1) is Stone stone)
{
    Console.WriteLine($"Unit: {stone.FullName}");
    Console.WriteLine($"Y: {stone.Y}");
    Console.WriteLine($"X: {stone.X}");
    Console.WriteLine($"Hp: {stone.Hp}");
    Console.WriteLine($"Vulnerabilities:\n{stone.showVulnerabilities()}");
}

unitManager.deleteUnit(0, 0);

//if (newPlayer.getInventory().getItem(0) is Weapon weapon1)
    //initializatorAttack(map.getElement(4, 1), ref weapon1, unitManager);

map.showMap();

if (map.getElement(4, 1) is Stone stone1)
{
    Console.WriteLine($"Unit: {stone1.FullName}");
    Console.WriteLine($"Y: {stone1.Y}");
    Console.WriteLine($"X: {stone1.X}");
    Console.WriteLine($"Hp: {stone1.Hp}");
    Console.WriteLine($"Vulnerabilities:\n{stone1.showVulnerabilities()}");
}

Apple apple = new Apple(51, 571);
Item? bow = newPlayer.getInventory().getItem(0);

initializatorAttack(newPlayer, ref bow, unitManager);

Console.WriteLine($"Hp: {newPlayer.Hp}");
if (bow is null)
    Console.WriteLine("Null");
newPlayer.eatFood(newPlayer.getInventory().getItem(0));
Console.WriteLine($"Hp: {newPlayer.Hp}");

Console.Write("Inventory:\n"); newPlayer.getInventory().showInventory();
for (int i = 0; i < newPlayer.getInventory().getSize(); i++)
{
    Console.WriteLine($"{i + 1}. {newPlayer.getInventory().getFullInformationItem(i)}");
}






class BreakManager
{
    static public void initializatorAttack(Unit? unit, ref Item? item, UnitManager unitManager)
    {
        if (item is Weapon weapon)
        {
            if (unit is not null && weapon.useWeapon(unit))
                unitManager.deleteUnit(unit.Y, unit.X);

            if (weapon.Solidity <= 0)
            {
                item.Keeper?.deleteItem(item.Index);
                item = null;
            }
        }
    }
}

class UnitManager
{
    private Map map;

    public UnitManager(Map _map)
    {
        map = _map;
    }
    public void addUnit(Unit unit, int row, int column)
    {
        if (row < 0 || column < 0 || row >= map.Row || column >= map.Column)
            return;
        else if (map.getElement(row, column) != null)
            return;

        unit.Y = row;
        unit.X = column;

        map.setElement(row, column, unit);
    }
    public void deleteUnit(int row, int column)
    {
        if (row < 0 || column < 0 || row > map.Row || column > map.Column)
            return;

        map.setElement(row, column);
    }
}

class Map
{
    private Unit?[,] world;
    public int Row {  get; init; }
    public int Column { get; init; }

    public Map(int rows, int columns)
    {
        world = new Unit?[rows < 0 ? 1 : rows, columns < 0 ? 1 : columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                world[i, j] = null;
            }
        }

        Row = rows;
        Column = columns;
    }

    public void showMap()
    {
        Console.WriteLine(new string('-', Column + 2));

        for (int i = 0; i < Row; i++)
        {
            for (int j = 0; j < Column; j++)
            {
                if(j == 0)
                    Console.Write('|');

                if (world[i, j] != null)
                    Console.Write(world[i, j].Symbol);
                else 
                    Console.Write(' ');
            }

            Console.WriteLine('|');
        }

        Console.WriteLine(new string('¯', Column + 2));
    }
    public Unit? getElement(int row, int column)
    {
        if (row < 0 || column < 0 || row >= Row || column >= Column)
            return null;

        return world[row, column];
    }
    public void setElement(int row, int column, Unit? unit = null)
    {
        if (row < 0 || column < 0 || row >= Row || column >= Column)
            return;
        
        world[row, column] = unit;
    }
}












abstract class Weapon : Item
{
    public float Damage { get; init; }
    public int Solidity { get; set; }

    public Weapon(float damage, int solidity, int index, string textItem, Inventory? inventory = null)
        :base(index, textItem, inventory)
    {
        this.Damage = damage;
        this.Solidity = solidity;
    }

    public bool findType(Unit unit, Type type)
    {
        foreach (Type weapon in unit.getVulnerabilities())
        {
            if (type.IsAssignableFrom(weapon) || weapon.IsAssignableFrom(type))
                return true;
        }

        return false;
    }
    public abstract bool useWeapon(Unit? unit);

    public override string getFullInfo()
    {
        StringWriter writer = new StringWriter();

        writer.WriteLine($"Name: { this.TextItem }");
        writer.WriteLine($"Damage: { this.Damage }");
        writer.WriteLine($"Solidity: { this.Solidity }");

        return writer.ToString();
    }
}

interface ITool
{
    bool useTool(Unit unit);
}

class Bow : Weapon
{
    public Bow(float damage, int solidity, int index, Inventory? inventory = null)
        : base(damage, solidity, index, "Bow", inventory) 
    { }

    public override bool useWeapon(Unit? unit)
    {
        if (Solidity <= 0 || unit is null)
            return false;

        if (findType(unit, typeof(Bow)) == true)
        {
            unit.Hp -= (int)Math.Round(Damage);
            Solidity--;
            return unit.Hp <= 0 ? true : false;
        }
        return false;
    }

}

class Sword : Weapon
{
    public Sword(float damage, int solidity, int index, Inventory? inventory = null)
        : base(damage, solidity, index, "Sword", inventory)
    { }

    public override bool useWeapon(Unit? unit)
    {
        if (Solidity <= 0 || unit is null)
            return false;

        if (findType(unit, typeof(Sword)) == true)
        {
            unit.Hp -= (int)Math.Round(Damage);
            Solidity--;
            return unit.Hp <= 0 ? true : false;
        }
        return false;
    }
}

class Axe : Weapon, ITool
{
    public Axe(float damage, int solidity, int index, Inventory? inventory = null)
        : base(damage, solidity, index, "Axe", inventory)
    { }

    public bool useTool(Unit unit)
    {
        if (Solidity <= 0)
            return false;

       if(findType(unit, typeof(Axe)) == true) 
       {
           unit.Hp -= (int)Math.Round(Damage);
           Solidity--;
           return true;
       }

        return false;
    }

    public override bool useWeapon(Unit? unit)
    {
        if (Solidity <= 0 || unit is null)
            return false;

        if (findType(unit, typeof(Axe)) == true)
        {
            unit.Hp -= (int)Math.Round(Damage);
            Solidity--;
            return unit.Hp <= 0 ? true : false;
        }
        return false;
    }
}

class Pick : Weapon, ITool
{
    public Pick(float damage, int solidity, int index, Inventory? inventory = null)
        : base(damage, solidity, index, "Pick", inventory)
    { }

    public bool useTool(Unit unit)
    {
        if (Solidity <= 0)
            return false;

        if(findType(unit, typeof(Pick)) == true) 
        {
            unit.Hp -= (int)Math.Round(Damage);
            Solidity--;
            return true;
        }
        return false;
    }

    public override bool useWeapon(Unit? unit)
    {
        if (Solidity <= 0 || unit == null)
            return false;

        if (findType(unit, typeof(Pick)) is true)
        {
            unit.Hp -= (int)Math.Round(Damage);
            Solidity--;
            return unit.Hp <= 0 ? true : false;
        }
        return false;
    }
}



class Food : Item
{
    public int NutritionalValue {  get; init; }
    
    public Food(int nutritionalValue, int index, string textItem)
        :base(index, textItem)
    {
        NutritionalValue = nutritionalValue;
    }

    public override string getFullInfo()
    {
        StringWriter writer = new StringWriter();

        writer.WriteLine($"Name: {this.TextItem}");
        writer.WriteLine($"NutritionalValue: {this.NutritionalValue}");

        return writer.ToString();
    }
}

class Apple : Food
{
    public Apple( int nutritionalValue, int index)
        : base(nutritionalValue, index, "Apple") 
    { }
}

class Meat : Food
{
    public Meat(int nutritionalValue, int index)
            : base(nutritionalValue, index, "Meat")
    { }
}

class Unit
{
    public int X { get; set; }
    public int Y { get; set; }

    protected List<Type> vulnerabilities = new List<Type>();
    public int Hp {  get; set; }

    public string FullName {  get; init; }
    public char Symbol { get; init; }

    public Unit(int x, int y, int hp, List<Type> _vulnerabilities,  char symbol, string fullName)
    {
        this.X = x;
        this.Y = y;
        this.Hp = hp;
        this.Symbol = symbol;
        this.vulnerabilities = _vulnerabilities;
        FullName = fullName;
    }

    public Unit(int hp, List<Type> _vulnerabilities, char symbol, string fullName) 
        :this(0, 0, hp, _vulnerabilities, symbol, fullName)
    {}

    public List<Type> getVulnerabilities()
    {
        return vulnerabilities;
    }
    public string showVulnerabilities()
    {
        StringWriter writer = new StringWriter();

        int index = 0;
        foreach(Type type in this.vulnerabilities)
        {
            index++;
            writer.WriteLine($"{index}. {type.ToString()}");
        }

        if (vulnerabilities.Count == 0)
            writer.WriteLine("NOUN Vulnerabilities");

        return writer.ToString();
    }
}




class Tree : Unit
{
    public Tree(int x, int y, int hp, List<Type> vulnerabilities) : base(x, y, hp, vulnerabilities, 'T', "Tree")
    {}
    public Tree(int hp, List<Type> vulnerabilities) : this(0, 0, hp, vulnerabilities)
    { }
}

class Stone : Unit
{
    public Stone(int x, int y, int hp, List<Type> vulnerabilities) : base(x, y, hp, vulnerabilities, 'S', "Stone")
    { }

    public Stone(int hp, List<Type> vulnerabilities) : this(0, 0, hp, vulnerabilities)
    { }
}


abstract class Item
{
    public Inventory? Keeper { get; set; }
    public int Index{ get; init; }
    public string TextItem {  get; init; }
    public Item(int index, string textItem, Inventory? keeper = null)
    {
        Index = index;
        TextItem = textItem;
        Keeper = keeper;
    }
    public abstract string getFullInfo();
}
class Inventory
{

    private List<Item> inventory = new List<Item>();

    public int findItemIndex(int index)
    {
       int itemIndex = -1;
       foreach (Item item in inventory)
       {
            itemIndex++;
            if (item.Index == index)
                return itemIndex;
       }
        return -1;
    }

    public void addItem(Item item)
    {
        item.Keeper = this;
        inventory.Add(item);
    }
    public void deleteItem(int index)
    {
        int removeIndex = findItemIndex(index);
        if (removeIndex != -1)
            inventory.RemoveAt(removeIndex);
    }

    public void deleteItemIndex(int index)
    {
        inventory.RemoveAt(index);
    }
    public string showInventory()
    {
        StringWriter writer = new StringWriter();

        int index = 0;
        foreach (Item item in inventory)
        {
            index++;
            writer.WriteLine($"{index}.{item.TextItem} | index: {item.Index}");
        }

        return writer.ToString();
    }

    public Item? getItem(int index)
    {
        return index < inventory.Count && index >= 0 ? inventory[index] : null;
    }
    public string getFullInformationItem(int index)
    {
        if (index < 0 || index >= inventory.Count)
            return "ERROR";

        return inventory[index].getFullInfo();
    }

    public int getSize()
    {
        return inventory.Count;
    }
}
class Player : Unit
{

    private Inventory inventory = new Inventory();

    public Player(int x, int y, int hp, List<Type> vulnerabilities) : base(x, y, hp, vulnerabilities, 'P', "Player")
    { }

    public Player(int hp, List<Type> vulnerabilities) : this(0, 0, hp, vulnerabilities)
    { }

    public Inventory getInventory()
    {
        return inventory;
    }

    public void eatFood(Item? item)
    {
        if (item is not null && item is Food food)
        {
            Hp += food.NutritionalValue;
            inventory.deleteItem(item.Index);
        }
    }
}