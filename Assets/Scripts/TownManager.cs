using UnityEngine;
using UnityEngine.SceneManagement;

public class TownManager : MonoBehaviour
{
    public static TownManager instance;

    [Header("Town Status")]
    public int townLevel = 1;
    public int maxTownLevel = 3;
    public int defenseValue = 0;

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

        if (PlayerPrefs.HasKey("TownLevel"))
            townLevel = PlayerPrefs.GetInt("TownLevel", 1);
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

        if (GameData.instance != null) GameData.instance.SaveTownLevel();
    }

    private void UpgradeDefense()
    {
        defenseValue += 1000;
    }

    //Town
    public static void TryUpgrade()
    {
        if (instance != null)
            instance.UpgradeTown();
    }

    //Defense
    public static void TryUpgradeDefense()
    {
        if (instance != null)
            instance.UpgradeDefense();
    }

    private void SetTownVisual()
    {
        Debug.Log("SetTownVisual called, level: " + townLevel);
        Debug.Log("townStart null: " + (townStart == null));
        townStart?.SetActive(townLevel == 1);
        townUpgrade1?.SetActive(townLevel == 2);
        townUpgrade2?.SetActive(townLevel == 3);

        foreach (TownLevelObject obj in FindObjectsByType<TownLevelObject>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            obj.UpdateVisibility();
    }
}