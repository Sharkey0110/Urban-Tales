using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerUIManager : MonoBehaviour
{
    public static PlayerUIManager Instance;

    [Header("Network Join")]
    [SerializeField] bool startGameAsClient;

    [HideInInspector] public PlayerUIHUDManager playerUIHUDManager;
    [HideInInspector] public PlayerUICharacterMenuManager playerMenuManager;

    public bool isMenuWindowOpen = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerUIHUDManager = GetComponentInChildren<PlayerUIHUDManager>();
        playerMenuManager = GetComponentInChildren<PlayerUICharacterMenuManager>();
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (startGameAsClient)
        {
            startGameAsClient = false;
            StartCoroutine(WaitThenJoinGameAsClient());

        }
    }

    private IEnumerator WaitThenJoinGameAsClient()
    {
        //everyone who plays the game starts the network as a host, to start as client you must first destroy your own connection as a host
        NetworkManager.Singleton.Shutdown();

        yield return new WaitForSeconds(3);

        //after shutdown, start network as client
        NetworkManager.Singleton.StartClient();
    }
}
