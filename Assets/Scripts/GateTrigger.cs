using UnityEngine;

public class GateTrigger : MonoBehaviour
{
    [SerializeField] private int sceneToLoad;
    [SerializeField] private string destinationSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Pass the responsibility of changing the scene to a persistent object
            PlayerSpawnManager.Instance.ChangeSceneWithSpawnPoint(sceneToLoad, destinationSpawnPoint);
        }
    }
}
