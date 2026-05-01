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
        rect.SetLocalPositionAndRotation(rect.transform.localPosition, Quaternion.Euler(0,0, -22.5f * tempTimeBlock));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //rect.Rotate(new Vector3(0,0,-22.5f * tempTimeBlock));
        rect.SetLocalPositionAndRotation(rect.transform.localPosition, Quaternion.Euler(0,0, -22.5f * tempTimeBlock));
        workingTimeBlock = DayTime.GetCurrentTimeBlock() - tempTimeBlock;
        //Debug.Log("Temp: " + tempTimeBlock + " Working: " + workingTimeBlock);
        if(workingTimeBlock >= intervals)
        {
            workingTimeBlock = 0;
            tempTimeBlock = DayTime.GetCurrentTimeBlock();
        }
    }
    public void RestartAnimation()
    {
        rect.Rotate(new Vector3(0,0,0));
    }
}
