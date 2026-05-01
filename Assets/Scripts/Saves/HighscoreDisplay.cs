using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighscoreDisplay : MonoBehaviour
{
    public GameObject highscorePanel;
    public TextMeshProUGUI highscoreText;
    public int maxScores = 10;

    private bool isOpen = false;

    private void Update()
    {
        if (isOpen && Input.GetKeyDown(KeyCode.Escape))
            ToggleHighscores();
    }

    public void ToggleHighscores()
    {
        isOpen = !isOpen;
        highscorePanel.SetActive(isOpen);
        if (isOpen) RefreshDisplay();
    }

    private void RefreshDisplay()
    {
        highscoreText.text = "--- Top 10 Highscores ---\n\n";

        bool anyScores = false;
        for (int i = 0; i < maxScores; i++)
        {
            string key = "Highscore_" + i;
            if (PlayerPrefs.HasKey(key))
            {
                anyScores = true;
                float time = PlayerPrefs.GetFloat(key);
                highscoreText.text += (i + 1) + ". " + FormatTime(time) + "\n";
            }
        }

        if (!anyScores)
            highscoreText.text += "No scores yet!";
    }

    private string FormatTime(float time)
    {
        int timeBlock = (int)(time / GameTime.timeInterval);
        float remainder = time % GameTime.timeInterval;
        return "Day Block " + timeBlock + " | " + remainder.ToString("F1") + "s";
    }
}