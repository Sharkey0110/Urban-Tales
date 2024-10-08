using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

public class PlayerNetworkManager : CharacterNetworkManager
{
    //things that all players need to see, but specific to players, so customisation/armour
    PlayerManager player;

    public NetworkVariable<FixedString64Bytes> characterName = new NetworkVariable<FixedString64Bytes>("Character", NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [Header("Armour")]
    public NetworkVariable<bool> isMale = new NetworkVariable<bool>(true, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> headEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> chestEquipmentID = new NetworkVariable<int>(0,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> legEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> handEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    public NetworkVariable<int> feetEquipmentID = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);



    public void OnHeadEquipmentChanged(int oldValue, int newValue)
    {
        //we already run on client side
        if (IsOwner) return;

        HeadEquipment headEquipment = WorldItemDatabase.Instance.GetHeadEquipmentByID(headEquipmentID.Value);

        if (headEquipment != null)
        {
            player.playerEquipmentManager.LoadHeadEquipment(Instantiate(headEquipment));
        }
        else
        {
            player.playerEquipmentManager.LoadHeadEquipment(null);
        }
    }

    public void OnChestEquipmentChanged(int oldValue, int newValue)
    {
        //we already run on client side
        if (IsOwner) return;

        ChestEquipment chestEquipment = WorldItemDatabase.Instance.GetChestEquipmentByID(chestEquipmentID.Value);

        if (chestEquipment != null)
        {
            player.playerEquipmentManager.LoadTorsoEquipment(Instantiate(chestEquipment));
        }
        else
        {
            player.playerEquipmentManager.LoadTorsoEquipment(null);
        }
    }

    public void OnLegEquipmentChanged(int oldValue, int newValue)
    {
        //we already run on client side
        if (IsOwner) return;

        LegEquipment legEquipment = WorldItemDatabase.Instance.GetLegEquipmentByID(legEquipmentID.Value);

        if (legEquipment != null)
        {
            player.playerEquipmentManager.LoadLegEquipment(Instantiate(legEquipment));
        }
        else
        {
            player.playerEquipmentManager.LoadLegEquipment(null);
        }
    }

    public void OnHandEquipmentChanged(int oldValue, int newValue)
    {
        //we already run on client side
        if (IsOwner) return;

        HandEquipment handEquipment = WorldItemDatabase.Instance.GetHandEquipmentByID(handEquipmentID.Value);

        if (handEquipment != null)
        {
            player.playerEquipmentManager.LoadHandEquipment(Instantiate(handEquipment));
        }
        else
        {
            player.playerEquipmentManager.LoadHandEquipment(null);
        }
    }

    public void OnFeetEquipmentChanged(int oldValue, int newValue)
    {
        //we already run on client side
        if (IsOwner) return;

        FeetEquipment feetEquipment = WorldItemDatabase.Instance.GetFeetEquipmentByID(feetEquipmentID.Value);

        if (feetEquipment != null)
        {
            player.playerEquipmentManager.LoadFeetEquipment(Instantiate(feetEquipment));
        }
        else
        {
            player.playerEquipmentManager.LoadFeetEquipment(null);
        }
    }

}
