using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentManager : MonoBehaviour
{
    public PlayerManager player;
    [SerializeField] bool equipNewItems = false;

    //we need variables to store our parent objects, that hold all potential equipment disabled

    [Header("Equipment Models")]
    public GameObject fullHelmetObject;
    [HideInInspector] public GameObject[] helmetComponents;

    public GameObject fullBackObject;
    [HideInInspector] public GameObject[] backComponents;

    public GameObject fullRightShoulderObject;
    [HideInInspector] public GameObject[] rightShoulderComponents;

    public GameObject fullLeftShoulderObject;
    [HideInInspector] public GameObject[] leftShoulderComponents;

    public GameObject fullRightUpperArmObject;
    [HideInInspector] public GameObject[] rightUpperArmComponents;

    public GameObject fullLeftUpperArmObject;
    [HideInInspector] public GameObject[] leftUpperArmComponents;

    public GameObject fullRightLowerArmObject;
    [HideInInspector] public GameObject[] rightLowerArmComponents;

    public GameObject fullLeftLowerArmObject;
    [HideInInspector] public GameObject[] leftLowerArmComponents;

    public GameObject fullRightHandObject;
    [HideInInspector] public GameObject[] rightHandComponents;

    public GameObject fullLeftHandObject;
    [HideInInspector] public GameObject[] leftHandComponents;

    public GameObject fullHipsObject;
    [HideInInspector] public GameObject[] hipsComponents;

    public GameObject fullRightLegObject;
    [HideInInspector] public GameObject[] rightLegComponents;

    public GameObject fullLeftLegObject;
    [HideInInspector] public GameObject[] leftLegComponents;

    //GENDER SPECIFICS
    //may need back and chest seperate

    [Header("Male Bodies")]
    public GameObject maleTorsoObject;
    [HideInInspector] public GameObject[] maleTorsoComponents;

    [Header("Female Bodies")]
    public GameObject femaleTorsoObject;
    [HideInInspector] public GameObject[] femaleTorsoComponents;

    private void Awake()
    {
        //we automatically take all of the game objects in each parent object and add them to the already made components varaible as an array

        player = GetComponent<PlayerManager>();

        //HELMETS
        List<GameObject> fullHelmetsList = new List<GameObject>();

        foreach(Transform child in fullHelmetObject.transform)
        {
            fullHelmetsList.Add(child.gameObject);
        }

        helmetComponents = fullHelmetsList.ToArray();

        //MALE TORSOS
        List<GameObject> fullMaleBodiesList = new List<GameObject>();

        foreach(Transform child in maleTorsoObject.transform)
        {
            fullMaleBodiesList.Add(child.gameObject);
        }

        maleTorsoComponents = fullMaleBodiesList.ToArray();

        //FEMALE TORSOS
        List<GameObject> fullFemaleBodiesList = new List<GameObject>();

        foreach (Transform child in femaleTorsoObject.transform)
        {
            fullFemaleBodiesList.Add(child.gameObject);
        }

        femaleTorsoComponents = fullFemaleBodiesList.ToArray();

        //LEFT SHOULDER
        List<GameObject> fullLeftShoulderList = new List<GameObject>();

        foreach (Transform child in fullLeftShoulderObject.transform)
        {
            fullLeftShoulderList.Add(child.gameObject);
        }

        leftShoulderComponents = fullLeftShoulderList.ToArray();

        //RIGHT SHOULDER
        List<GameObject> fullRightShoulderList = new List<GameObject>();

        foreach (Transform child in fullRightShoulderObject.transform)
        {
            fullRightShoulderList.Add(child.gameObject);
        }

        rightShoulderComponents = fullRightShoulderList.ToArray();
    }


    private void Update()
    {
        if(equipNewItems)
        {
            equipNewItems = false;
            DebugEquipItems();
        }
    }

    private void DebugEquipItems()
    {
        LoadHeadEquipment(player.playerInventoryManager.headEquipment);
        LoadTorsoEquipment(player.playerInventoryManager.chestEquipment);
        //LoadLegEquipment(player.playerInventoryManager.legEquipment);
        //LoadHandEquipment(player.playerInventoryManager.handEquipment);
        //LoadFeetEquipment(player.playerInventoryManager.feetEquipment);

        //ep 63 16 mins, for stats
    }

    //EQUIPMENT CODE


    //HELMET (UNIQUE)
    public void LoadHeadEquipment(HeadEquipment equipment)
    {
        //remove old equipment
        UnloadHeadEquipmentModel();

        //if null, equip nothing
        if(equipment == null)
        {
            if (player.IsOwner)
            {
                player.playerNetworkManager.headEquipmentID.Value = -1; //-1 is null and will unequip
            }
            player.playerInventoryManager.headEquipment = null;
            return;
        }

        //if not null, equip new helm
        player.playerInventoryManager.headEquipment = equipment;

        switch (equipment.headEquipmentType)
        {
            case HeadEquipmentType.FaceAndHairCoverHelmet:
                //disable both
                break;
            case HeadEquipmentType.FaceCoverHelmet:
                //disable face
                break;
            case HeadEquipmentType.HairCoverHelmet:
                //disable hair
                break;
            case HeadEquipmentType.Helmet:
                break;
        }

        foreach(var model in equipment.equipmentModels)
        {
            model.LoadModel(player, true);
        }

        if (player.IsOwner)
        {
            player.playerNetworkManager.headEquipmentID.Value = equipment.itemID;
        }
    }

    private void UnloadHeadEquipmentModel()
    {
        foreach (var model in helmetComponents)
        {
            model.SetActive(false);
        }

        //reenable hair and head in a body manager script
    }


    //TORSO EQUIPMENT
    public void LoadTorsoEquipment(ChestEquipment equipment)
    {
        UnloadTorsoEquipmentModel();

        if(equipment == null)
        {
            if (player.IsOwner)
            {
                player.playerNetworkManager.chestEquipmentID.Value = -1;
            }

            player.playerInventoryManager.chestEquipment = null;
            return;
        }

        player.playerInventoryManager.chestEquipment = equipment;

        foreach(var model in equipment.equipmentModels)
        {
            model.LoadModel(player, true);
        }

        //calculate
        if(player.IsOwner)
        {
            player.playerNetworkManager.chestEquipmentID.Value = equipment.itemID;
        }
    }

    public void UnloadTorsoEquipmentModel()
    {
        foreach(var model in rightShoulderComponents)
        {
            model.SetActive(false);
        }

        foreach (var model in leftShoulderComponents)
        {
            model.SetActive(false);
        }

        foreach (var model in rightUpperArmComponents)
        {
            model.SetActive(false);
        }

        foreach (var model in leftUpperArmComponents)
        {
            model.SetActive(false);
        }

        foreach(var model in backComponents)
        {
            model.SetActive(false);
        }

        //GENDER SPECIFIC

        foreach (var model in maleTorsoComponents)
        {
            model.SetActive(false);
        }

        foreach (var model in femaleTorsoComponents)
        {
            model.SetActive(false);
        }

        //enable body
    }

    //LEG EQUIPMENT
    public void LoadLegEquipment(LegEquipment equipment)
    {

    }

    //HAND EQUIPMENT
    public void LoadHandEquipment(HandEquipment equipment)
    {

    }

    //FEET EQUIPMENT
    public void LoadFeetEquipment(FeetEquipment equipment)
    {

    }
}
