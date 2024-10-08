using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerManager : CharacterManager
{
    //a place to store all the componets we get
    [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
    [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
    [HideInInspector] public PlayerEquipmentManager playerEquipmentManager;
    [HideInInspector] public PlayerInventoryManager playerInventoryManager;
    [HideInInspector] public PlayerNetworkManager playerNetworkManager;
    [HideInInspector] public PlayerBodyManager playerBodyManager;
    [HideInInspector] public PlayerStatsManager playerStatsManager;

    protected override void Awake()
    {
        base.Awake();

        //getting all of the components
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
        playerInventoryManager = GetComponent<PlayerInventoryManager>();
        playerNetworkManager = GetComponent<PlayerNetworkManager>();
        playerBodyManager = GetComponent<PlayerBodyManager>();
    }

    protected override void Update()
    {
        //if you own the character, handle the movements, only you should move your character
        base.Update();
        if (!IsOwner) return;
        playerLocomotionManager.HandleAllMovements();
    }

    protected override void LateUpdate()
    {
        //same logic with camera
        if(!IsOwner) return;
        base.LateUpdate();
        PlayerCamera.Instance.HandleAllCameraMovements();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        //NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;

        if (IsOwner)
        {
            //asign the player variables in the player camera and the player input to this current clone
            //this is used A because its a clone and B because we can have multiple clones
            PlayerCamera.Instance.player = this;
            PlayerInputManager.Instance.player = this;
            PlayerSpawnManager.Instance.player = this;


            playerNetworkManager.currentHp.OnValueChanged += PlayerUIManager.Instance.playerUIHUDManager.SetNewHPValue;

            //moved when save/load
            playerNetworkManager.hp.Value = 10;
            playerNetworkManager.currentHp.Value = 10;

            PlayerUIManager.Instance.playerUIHUDManager.SetMaxHPValue(playerNetworkManager.hp.Value);

        }

        //ARMOUR
        //TODO none of this works for some reason
        playerNetworkManager.headEquipmentID.OnValueChanged += playerNetworkManager.OnHeadEquipmentChanged;
        playerNetworkManager.chestEquipmentID.OnValueChanged += playerNetworkManager.OnChestEquipmentChanged;
        playerNetworkManager.legEquipmentID.OnValueChanged += playerNetworkManager.OnLegEquipmentChanged;
        playerNetworkManager.headEquipmentID.OnValueChanged += playerNetworkManager.OnHandEquipmentChanged;
        playerNetworkManager.feetEquipmentID.OnValueChanged += playerNetworkManager.OnFeetEquipmentChanged;

        if(IsOwner && !IsServer)
        {
            //LoadOtherPlayerCharacterWhenJoiningServer(ref WorldSaveGameManager.Instance.currentCharacterData);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();

        //NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
        playerNetworkManager.headEquipmentID.OnValueChanged -= playerNetworkManager.OnHeadEquipmentChanged;
        playerNetworkManager.chestEquipmentID.OnValueChanged -= playerNetworkManager.OnChestEquipmentChanged;
        playerNetworkManager.legEquipmentID.OnValueChanged -= playerNetworkManager.OnLegEquipmentChanged;
        playerNetworkManager.headEquipmentID.OnValueChanged -= playerNetworkManager.OnHandEquipmentChanged;
        playerNetworkManager.feetEquipmentID.OnValueChanged -= playerNetworkManager.OnFeetEquipmentChanged;
    }

    public void LoadOtherPlayerCharacterWhenJoiningServer()
    {
        //Sync Armour
        playerNetworkManager.OnHeadEquipmentChanged(0, playerNetworkManager.headEquipmentID.Value);
        playerNetworkManager.OnChestEquipmentChanged(0, playerNetworkManager.chestEquipmentID.Value);
        playerNetworkManager.OnLegEquipmentChanged(0, playerNetworkManager.legEquipmentID.Value);
        playerNetworkManager.OnHandEquipmentChanged(0,playerNetworkManager.handEquipmentID.Value);
        playerNetworkManager.OnFeetEquipmentChanged(0, playerNetworkManager.feetEquipmentID.Value);
    }


    //we use ref because we specifically need the one in use right now
    public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
        currentCharacterData.xPosition = transform.position.x;
        currentCharacterData.yPosition = transform.position.y;
        currentCharacterData.zPosition = transform.position.z;

        //add scene and probs stats
    }

    public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
    {
        playerNetworkManager.characterName.Value = currentCharacterData.characterName;
        Vector3 myPosition = new Vector3 (currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
        transform.position = myPosition;
    }

}
