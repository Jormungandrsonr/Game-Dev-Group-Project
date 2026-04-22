using UnityEngine;

public class PlayerAnim : BasicMovement
{
    static bool locked = false;
    public static bool IsLocked()
    {
        return locked;
    }
    public static void Lock()
    {
        locked = true;
    }
    public static void Unlock()
    {
        locked = false;
    }
}
