using UnityEngine;

public class DayTime
{
    static int currentTimeBlock = 0;
    static int currentday = 0;
    static int finalDay = 3;
    static float time = 0f;
    

    //// Getter and Setter Methods
    public static int GetCurrentDay()
    {
        return currentday;
    }
    public static int GetFinalDay()
    {
        return finalDay;
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
    public static void SetFinalDay(int newFinal)
    {
        finalDay = newFinal;
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
