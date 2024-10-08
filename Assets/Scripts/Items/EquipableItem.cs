using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : Item
{
    //Every equipable item will have all these feautures plus those of every item

    [Header("Stats")]
    public int evasion;
    public int magicalMight;
    public int magicalMending;
    public int deftness;
    public int agility;
    //any item that can be equiped, weapon, aromour, accessory

    //speical effects on each item can go here
}
