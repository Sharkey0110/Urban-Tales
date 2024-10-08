using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSaveGameManager : MonoBehaviour
{
    public static WorldSaveGameManager Instance;

    [SerializeField] private PlayerManager player;

    [Header("Save/Load")]
    [SerializeField] bool saveGame;
    [SerializeField] bool loadGame;

    [SerializeField] int firstScreenAfterTitleOnNewGame = 1;

    [Header("Current Character Data")]
    public CharacterSaveData currentCharacterData;
    public CharacterSlot currentCharacterSlot;
    private string fileName;

    [Header("Save Data Writer")]
    public SaveFileDataWriter saveFileDataWriter;

    [Header("Character Slots")]
    public CharacterSaveData characterSlot01;
    public CharacterSaveData characterSlot02;
    public CharacterSaveData characterSlot03;

    //make sure theres only one instance in the scene ever, if there isnt, let this instance be the only
    //if theres already another instance, destroy this gameobject
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
    }

    //when scenes change, all of the game objects are destroyed, this code makes sure world managers are not
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(saveGame)
        {
            saveGame = false;
            SaveGame();
        }
        if (loadGame)
        {
            loadGame = false;
            LoadGame();
        }
    }

    //find what character slot your using and name the file based on it, so it can be easily loaded again
    private void DecideCharacterFileNameBasedOnCharacterSlotBeingUsed()
    {
        switch (currentCharacterSlot)
        {
            case CharacterSlot.CharacterSlot01:
                fileName = "CharacterSlot01";
                break;

            case CharacterSlot.CharacterSlot02:
                fileName = "CharacterSlot02";
                break; 

            case CharacterSlot.CharacterSlot03:
                fileName = "CharacterSlot03";
                break;
        }
    }

    //on your first time loading into the world, maybe find out how to put cutscenes here
    public void CreateNewGame()
    {
        //create a new file depnding on what slot your using
        DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();

        //create new data as none exists
        currentCharacterData = new CharacterSaveData();
    }

    public void LoadGame()
    {
        //load a previous file depending on what slot your using
        DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();

        //create a new writer
        saveFileDataWriter = new SaveFileDataWriter();

        //just standard
        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = fileName;
        currentCharacterData = saveFileDataWriter.LoadSaveFile();

        StartCoroutine(LoadWorldScene());
    }

    public void SaveGame()
    {
        //Save current file under a file name depending on which slot your using
        DecideCharacterFileNameBasedOnCharacterSlotBeingUsed();

        saveFileDataWriter = new SaveFileDataWriter();

        saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
        saveFileDataWriter.saveFileName = fileName;

        //Pass the players info, from game, to save file
        player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

        //write that info onto the json file
        saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
    }

    //An IEnumerator is used when you return something that needs to happen over time, its like an async function, in unity they can pause and resume later if needed
    public IEnumerator LoadWorldScene()
    {
        //this is as simple as its gets, the syntax is just like this i think
        AsyncOperation loadOperatior = SceneManager.LoadSceneAsync(firstScreenAfterTitleOnNewGame);
        yield return null;
    }
}
