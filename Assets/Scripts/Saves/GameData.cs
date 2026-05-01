using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("Timer")]
    public float currentTime = 0f;
    public bool timerRunning = false;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    private void Update()
    {
        if (timerRunning)
            currentTime += Time.deltaTime;
    }

    // ---- TIMER ----
    public void StartTimer() => timerRunning = true;
    public void StopTimer() => timerRunning = false;

    public void SubmitHighscore()
    {
        float best = PlayerPrefs.GetFloat("Highscore", float.MaxValue);
        if (currentTime < best)
        {
            PlayerPrefs.SetFloat("Highscore", currentTime);
            Debug.Log("New highscore: " + FormatTime(currentTime));
        }
        PlayerPrefs.Save();
    }

    public float GetHighscore() => PlayerPrefs.GetFloat("Highscore", float.MaxValue);

    public string FormatTime(float time)
    {
        int minutes = (int)(time / 60);
        int seconds = (int)(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SaveCompletionTime(int timeBlock, float time)
    {
        float totalTime = (timeBlock * GameTime.timeInterval) + time;

        // Load existing scores
        float[] scores = new float[10];
        for (int i = 0; i < 10; i++)
            scores[i] = PlayerPrefs.GetFloat("Highscore_" + i, float.MaxValue);

        // Insert new score in sorted order
        for (int i = 0; i < 10; i++)
        {
            if (totalTime < scores[i])
            {
                // Shift scores down
                for (int j = 9; j > i; j--)
                    scores[j] = scores[j - 1];
                scores[i] = totalTime;
                Debug.Log("New score saved at rank " + (i + 1));
                break;
            }
        }

        // Save back
        for (int i = 0; i < 10; i++)
            if (scores[i] != float.MaxValue)
                PlayerPrefs.SetFloat("Highscore_" + i, scores[i]);

        PlayerPrefs.Save();
    }

    // ---- RESOURCES (mined/chopped tracking) ----
    public void SetResourceMined(string resourceID, bool state)
    {
        PlayerPrefs.SetInt("Resource_" + resourceID, state ? 1 : 0);
    }

    public bool GetResourceMined(string resourceID)
    {
        return PlayerPrefs.GetInt("Resource_" + resourceID, 0) == 1;
    }

    // ---- INVENTORY ----
    public void SaveInventory()
    {
        int count = (int)EnumItemSet.endPoint;
        for (int i = 0; i < count; i++)
            PlayerPrefs.SetInt("Item_" + i, InventoryManager.CheckItemCount(i));
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        int count = (int)EnumItemSet.endPoint;
        for (int i = 0; i < count; i++)
        {
            int saved = PlayerPrefs.GetInt("Item_" + i, 0);
            int current = InventoryManager.CheckItemCount(i);
            if (saved > current)
                InventoryManager.AddItem(i, saved - current);
        }
    }

    // ---- TOWN LEVEL ----
    public void SaveTownLevel()
    {
        PlayerPrefs.SetInt("TownLevel", TownManager.instance.townLevel);
        PlayerPrefs.Save();
    }

    public int LoadTownLevel()
    {
        return PlayerPrefs.GetInt("TownLevel", 1);
    }

    // ---- SAVE/LOAD ALL ----
    public void Save()
    {
        SaveInventory();
        if (TownManager.instance != null) SaveTownLevel();
        PlayerPrefs.Save();
        Debug.Log("Game saved.");
    }

    public void Load()
    {
        LoadInventory();
        Debug.Log("Game loaded.");
    }

}