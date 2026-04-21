using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    public static TownManager instance;

    [Header("Town Status")]
    public int townLevel = 1;
    public int maxTownLevel = 3;

    [Header("Town Tilemaps")]
    public GameObject townStart;
    public GameObject townUpgrade1;
    public GameObject townUpgrade2;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-fetch tilemaps by name when town scene loads
        if (scene.name == "OfficialTown") // change to your exact scene name
        {
            townStart = GameObject.Find("TownStart");
            townUpgrade1 = GameObject.Find("TownUpgrade1");
            townUpgrade2 = GameObject.Find("TownUpgrade2");
            Debug.Log("Tilemaps refetched - townStart null: " + (townStart == null));
            SetTownVisual();
        }
    }

    public void UpgradeTown()
    {
        if (townLevel >= maxTownLevel)
        {
            Debug.Log("Town is already at max level.");
            return;
        }
        townLevel++;
        Debug.Log("Town upgraded to level " + townLevel);
        SetTownVisual();
    }

    public static void TryUpgrade()
    {
        if (instance != null)
            instance.UpgradeTown();
    }

    private void SetTownVisual()
    {
        Debug.Log("SetTownVisual called, level: " + townLevel);
        Debug.Log("townStart null: " + (townStart == null));
        townStart?.SetActive(townLevel == 1);
        townUpgrade1?.SetActive(townLevel == 2);
        townUpgrade2?.SetActive(townLevel == 3);
    }
}