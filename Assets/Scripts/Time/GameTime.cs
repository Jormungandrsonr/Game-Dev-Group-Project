using UnityEngine;

public class GameTime : MonoBehaviour
{
    bool dayEnded = false;
    static int currentTimeBlock = 0;
    static float time = 0f;
    public static float timeInterval = 10f;
    public int endTime = 6;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(currentTimeBlock > endTime)
        {
            dayEnded = true;
        }  
        if(time > timeInterval)
        {
            time = 0;
            currentTimeBlock++;
        }
        time+= Time.fixedDeltaTime;
    }

    //temp gui
    void OnGUI()
    {
        if(!dayEnded)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 48;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width - 320, 10, 200 ,50), "Time is " + (int)time+ "\nIn Time Block " + currentTimeBlock, style);
        }
        else
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = 48;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(Screen.width - 220, 10, 200 ,50), "Day has Ended!", style);
        }
    }

    /////// Non Unity Methods ///////
    

    public static float GetTime()
    {
        return time;
    }
    public static int GetTimeBlock()
    {
        return currentTimeBlock;
    }
}
