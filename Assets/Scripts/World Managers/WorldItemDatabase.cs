using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldItemDatabase : MonoBehaviour
{
    public static WorldItemDatabase Instance;

    [SerializeField] List<Item> items = new List<Item>();

    [Header("Head Equipment")]
    [SerializeField] List<HeadEquipment> headEquipment = new List<HeadEquipment>();

    [Header("Chest Equipment")]
    [SerializeField] List<ChestEquipment> chestEquipment = new List<ChestEquipment>();

    [Header("Leg Equipment")]
    [SerializeField] List<LegEquipment> legEquipment = new List<LegEquipment>();

    [Header("Hand Equipment")]
    [SerializeField] List<HandEquipment> handEquipment = new List<HandEquipment>();

    [Header("Feet Equipment")]
    [SerializeField] List<FeetEquipment> feetEquipment = new List<FeetEquipment>();

    [Header("Accessories")]
    [SerializeField] List<AccessoryEquipment> accessories = new List<AccessoryEquipment>();

    //Each type of equipment is spaced 1000 IDS apart, so if i add new helmets or hands, i dont push half of the databases item ids up by one,
    //which would complety ruin saved inventories
    [Header("ID Keys")]
    [SerializeField] int weaponItemKey = 1000;
    [SerializeField] int headItemKey = 2000;
    [SerializeField] int chestItemKey = 3000;
    [SerializeField] int legItemKey = 4000;
    [SerializeField] int handItemKey = 5000;
    [SerializeField] int feetItemKey = 6000;
    [SerializeField] int accessoryItemKey = 7000;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        //EACH ARMOUR IS ADDED TO THE GENERAL ITEM DATABASE, THEY MUST BE ADDED TO THEIR OWN MANUALLY
        for(int i = 0; i < headEquipment.Count; i++)
        {
            headEquipment[i].itemID = headItemKey + i;
            items.Add(headEquipment[i]);
        }

        for (int i = 0; i < chestEquipment.Count; i++)
        {
            chestEquipment[i].itemID = chestItemKey + i;
            items.Add(chestEquipment[i]);
        }

        for (int i = 0; i < legEquipment.Count; i++)
        {
            legEquipment[i].itemID = legItemKey + i;
            items.Add(legEquipment[i]);
        }

        for (int i = 0; i < handEquipment.Count; i++)
        {
            handEquipment[i].itemID = handItemKey + i;
            items.Add(handEquipment[i]);
        }

        for (int i = 0; i < feetEquipment.Count; i++)
        {
            feetEquipment[i].itemID = feetItemKey + i;
            items.Add(feetEquipment[i]);
        }

        for (int i = 0; i < accessories.Count; i++)
        {
            accessories[i].itemID = accessoryItemKey + i;
            items.Add(accessories[i]);
        }
    }

    //GET ARMOURS BY IDS
    //Returns the data of the head equipment you have the id for
    public HeadEquipment GetHeadEquipmentByID(int id)
    {
        return headEquipment.FirstOrDefault(equipment => equipment.itemID == id);
    }

    public ChestEquipment GetChestEquipmentByID(int id)
    {
        return chestEquipment.FirstOrDefault(equipment => equipment.itemID == id);
    }

    public LegEquipment GetLegEquipmentByID(int id)
    {
        return legEquipment.FirstOrDefault(equipment => equipment.itemID == id);
    }

    public HandEquipment GetHandEquipmentByID(int id)
    {
        return handEquipment.FirstOrDefault(equipment => equipment.itemID == id);
    }

    public FeetEquipment GetFeetEquipmentByID(int id)
    {
        return feetEquipment.FirstOrDefault(equipment => equipment.itemID == id);
    }

    public AccessoryEquipment GetAccessorytByID(int id)
    {
        return accessories.FirstOrDefault(equipment => equipment.itemID == id);
    }
}
