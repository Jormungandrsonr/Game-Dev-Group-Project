using UnityEngine;

public class GameTime : MonoBehaviour
{
    bool dayEnded = false;
    static int currentTimeBlock = 0;
    static float time = 0f;
    public static float timeInterval = 10f;
    public static int endTime = 16;
    public bool showGUI = false;
    
    void Awake()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(currentTimeBlock >= endTime)
        {
            dayEnded = true;

            //do stuff regarding game time
        }  
        else if(time > timeInterval)
        {
            time = 0;
            currentTimeBlock++;
        }
        else{time+= Time.fixedDeltaTime;}
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
                GUI.Label(new Rect(10, 10, 200 ,50), "Time is " + (int)time+ "\nIn Time Block " + currentTimeBlock, style);
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
        //stuff regarding end of game day
    }
    public void RestartDay()
    {
        currentTimeBlock = 0;
        time = 0;
        dayEnded = false;
    }

    public static float GetTime()
    {
        return time;
    }
    public static int GetTimeBlock()
    {
        return currentTimeBlock;
    }
}
