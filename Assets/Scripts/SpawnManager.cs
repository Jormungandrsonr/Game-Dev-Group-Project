using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    public string spawnPointID = ""; // set before loading a scene

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static void SetSpawn(string id)
    {
        if (instance != null)
            instance.spawnPointID = id;
    }
}