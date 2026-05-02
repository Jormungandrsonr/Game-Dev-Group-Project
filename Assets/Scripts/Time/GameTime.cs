using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour
{
    bool dayEnded = false;
    public static float timeInterval = 10f;
    public static int endTime = 16;
    public bool showGUI = false;

    static float tempTime = 0;
    static int tempTimeBlock = 0;

    DayTime timeObject;
    
    void Awake()
    {
        timeObject = new DayTime();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        tempTime = DayTime.GetCurrentTime();
        tempTimeBlock = DayTime.GetCurrentTimeBlock();

        //Win Con
        if ((TownManager.instance != null && TownManager.instance.defenseValue >= 5000) || dayEnded)
            ForceFinishDay();

        if (tempTimeBlock >= endTime)
        {
            dayEnded = true;

            //do stuff regarding game time
        }  
        else if(tempTime > timeInterval)
        {
            //time = 0;
            DayTime.SetCurrentTime(0);
            //currentTimeBlock++;
            DayTime.SetCurrentTimeBlock(tempTimeBlock+1);
        }
        else
        {
            DayTime.SetCurrentTime(tempTime + Time.fixedDeltaTime);
        }
    }

    //temp gui
    
    void OnGUI()
    {   if(showGUI)
        {
            if(!dayEnded)
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 48;
                style.normal.textColor = Color.red;
                GUI.Label(new Rect(10, 10, 200 ,50), "Time is " + (int)DayTime.GetCurrentTime()+ "\nIn Time Block " + DayTime.GetCurrentTimeBlock(), style);
            }
            else
            {
                GUIStyle style = new GUIStyle();
                style.fontSize = 48;
                style.normal.textColor = Color.red;
                GUI.Label(new Rect(10, 10, 200 ,50), "Day has Ended!", style);
            }
        }
    }

    /////// Non Unity Methods ///////
    public void ForceFinishDay()
    {
        DayTime.SetCurrentTimeBlock(endTime);
        if(DayTime.GetFinalDay() <= DayTime.GetCurrentDay())
        {
            if (GameData.instance != null)
                GameData.instance.SubmitHighscore();

            SceneManager.LoadSceneAsync("Ending");
        }
        else
        {
            DayTime.SetCurrentDay(DayTime.GetCurrentDay()+ 1);
        }

    }
    public void RestartDay()
    {
        DayTime.SetCurrentTimeBlock(0);
        DayTime.SetCurrentTime(0);
        dayEnded = false;
    }

    //3 is easy, 2 is med, 1 is hard
    public void SetDifficulty(int difficulty)
    {
        DayTime.SetFinalDay(difficulty * 2);
    }
}
