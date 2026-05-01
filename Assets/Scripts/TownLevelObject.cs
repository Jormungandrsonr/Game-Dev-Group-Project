using UnityEngine;

public class TownLevelObject : MonoBehaviour
{
    public int[] activeLevels; // e.g. {2, 3} = show at level 2 and 3

    private void Start()
    {
        UpdateVisibility();
    }

    public void UpdateVisibility()
    {
        if (TownManager.instance == null)
        {
            Debug.Log(gameObject.name + ": TownManager instance is null");
            return;
        }
        bool shouldBeActive = System.Array.IndexOf(activeLevels, TownManager.instance.townLevel) >= 0;
        Debug.Log(gameObject.name + ": townLevel=" + TownManager.instance.townLevel + " activeLevels=" + string.Join(",", activeLevels) + " shouldBeActive=" + shouldBeActive);
        gameObject.SetActive(shouldBeActive);
    }
}