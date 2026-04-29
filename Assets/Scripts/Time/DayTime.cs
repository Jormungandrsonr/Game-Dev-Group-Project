using UnityEngine;

public class DayTime
{
    static int currentTimeBlock = 0;
    static int currentday = 0;
    static float time = 0f;

    //// Getter and Setter Methods
    public static int GetCurrentDay()
    {
        return currentday;
    }
    public static int GetCurrentTimeBlock()
    {
        return currentTimeBlock;
    }
    public static float GetCurrentTime()
    {
        return time;
    }


    public static void SetCurrentDay(int newDay)
    {
        currentday = newDay;
    }
    public static void SetCurrentTimeBlock(int newTBlock)
    {
        currentTimeBlock = newTBlock;
    }
    public static void SetCurrentTime(float newTime)
    {
        time = newTime;
    }
}
