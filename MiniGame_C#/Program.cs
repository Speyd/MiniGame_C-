using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MiniGame_C_;
using MiniGame_C_.Items;
using MiniGame_C_.Items.Weapons;
using MiniGame_C_.Items.Foods;
using MiniGame_C_.Mananger;
using static MiniGame_C_.Mananger.BreakManager;
using MiniGame_C_.Units;


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
