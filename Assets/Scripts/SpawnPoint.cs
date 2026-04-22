using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    public string spawnID; // must match the ID set in SceneChange

    // CHANGE Start() in SpawnPoint.cs:
    private void Start()
    {
        Debug.Log("SpawnPoint " + spawnID + " checking against " + (SpawnManager.instance != null ? SpawnManager.instance.spawnPointID : "NO MANAGER"));

        if (SpawnManager.instance == null)
        {
            Debug.Log("SpawnManager instance is null");
            return;
        }
        if (SpawnManager.instance.spawnPointID == spawnID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Debug.Log("Player found: " + (player != null));
            if (player != null)
            {
                player.transform.position = transform.position;
                Debug.Log("Player moved to " + transform.position);
            }
        }
    }
}