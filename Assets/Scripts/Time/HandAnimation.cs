using UnityEngine;

public class HandAnimation : MonoBehaviour
{
    RectTransform rect;
    float intervals;
    int workingTimeBlock = 0;
    int tempTimeBlock = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rect = GetComponent<RectTransform>();
        intervals = GameTime.endTime/16;
        //Debug.Log("Clock Intervals in " + intervals);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        workingTimeBlock = GameTime.GetTimeBlock() - tempTimeBlock;
        //Debug.Log("Temp: " + tempTimeBlock + " Working: " + workingTimeBlock);
        if(workingTimeBlock >= intervals)
        {
            rect.Rotate(new Vector3(0,0,-22.5f));
            workingTimeBlock = 0;
            tempTimeBlock = GameTime.GetTimeBlock();
        }
    }
    public void RestartAnimation()
    {
        rect.Rotate(new Vector3(0,0,0));
    }
}
