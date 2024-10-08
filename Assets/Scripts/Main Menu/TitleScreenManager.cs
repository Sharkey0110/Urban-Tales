using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TitleScreenManager : MonoBehaviour
{

    //when you enter the game, connect to the network as a host, call this from the press start button
    //though maybe i dont even need to do this if most of the game will be single player, idk, ill look into it
    //i might be able to activate this when you talk to an NPC
    public void StartNetworkAsHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    //start coroutine is needed to run IEnumorators
    //this starts a new game, it is connected to the new game button

    public void StartNewGame()
    {
        WorldSaveGameManager.Instance.CreateNewGame();
        StartCoroutine(WorldSaveGameManager.Instance.LoadWorldScene());
    }
}
