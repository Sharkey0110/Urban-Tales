using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : ScriptableObject
{
    //what will EVERY sigle item have in common, weather it is equipment, material, weapon, consumable

    [Header("Infomation")]
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    [TextArea] public string itemDescription;
    public int price;
}
