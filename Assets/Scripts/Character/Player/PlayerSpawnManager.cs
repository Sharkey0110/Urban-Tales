using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSpawnManager : MonoBehaviour
{
    public static PlayerSpawnManager Instance;

    public PlayerManager player;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Method to initiate the scene change
    public void ChangeSceneWithSpawnPoint(int sceneToLoad, string spawnPointName)
    {
        StartCoroutine(ChangeScene(sceneToLoad, spawnPointName));
    }

    // Coroutine to handle the asynchronous scene loading and player relocation
    private IEnumerator ChangeScene(int sceneToLoad, string spawnPointName)
    {
        // Start loading the scene asynchronously
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the scene has fully loaded
        while (!loadOperation.isDone)
        {
            yield return null;
        }

        // Now that the scene is loaded, locate the spawn point and move the player
        Debug.Log("Scene loaded, locating spawn point.");
        LocateSpawnPointForNextArea(spawnPointName);
    }

    // Method to find and move the player to the specified spawn point
    public void LocateSpawnPointForNextArea(string spawnPointName)
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not assigned.");
            return;
        }

        if (string.IsNullOrEmpty(spawnPointName))
        {
            Debug.LogError("Spawn point name is empty");
            return;
        }

        GameObject spawnPoint = GameObject.Find(spawnPointName);
        if (spawnPoint == null)
        {
            Debug.LogError("Couldn't find spawn point");
            return;
        }

        // Temporarily disable movement handling
        player.playerLocomotionManager.enabled = false;
        player.characterController.enabled = false;

        Vector3 newPosition = spawnPoint.transform.position;
        player.transform.position = newPosition;

        // Re-enable movement handling
        player.playerLocomotionManager.enabled = true;
        player.characterController.enabled = true;
    }
}
