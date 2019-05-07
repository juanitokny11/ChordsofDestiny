using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class MyGameData
{
    public Playerdata playerData;
}

[System.Serializable]
public struct InventoryElement //inventoryElement struc
{
    public int id;
    public string name;
    public int amount;
}

[System.Serializable]
public class Playerdata
{
    public enum Race { Fairy, Dwarf }//enum
    public Race race;
    public string name;//string
    public int hp;//int
    public int armor;//int
    public int mana;//int
    public InventoryElement[] inventory;//array 
}


