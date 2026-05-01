using UnityEngine;

public class MinableResource : MonoBehaviour
{
    public string resourceID; // unique name e.g. "Rock_Forest_01"

    private void Awake()
    {
        if (GameData.instance == null)
        {
            Debug.Log("GameData instance is null on " + resourceID);
            return;
        }

        Debug.Log(resourceID + " mined state: " + GameData.instance.GetResourceMined(resourceID));

        if (GameData.instance.GetResourceMined(resourceID))
            Destroy(gameObject);
    }

    public void OnMined()
    {
        if (GameData.instance != null)
            GameData.instance.SetResourceMined(resourceID, true);
        Destroy(gameObject);
    }
}