using UnityEngine;
using TMPro;

public class ResourceTracker : MonoBehaviour
{
    [Header("Resource Count Text")]
    public TextMeshProUGUI goldCount;
    public TextMeshProUGUI rockCount;
    public TextMeshProUGUI logCount;
    public TextMeshProUGUI smallFishCount;
    public TextMeshProUGUI mediumFishCount;
    public TextMeshProUGUI largeFishCount;

    private void Update()
    {
        goldCount.text = InventoryManager.CheckItemCount((int)EnumItemSet.Gold).ToString();
        rockCount.text = InventoryManager.CheckItemCount((int)EnumItemSet.Stone).ToString();
        logCount.text = InventoryManager.CheckItemCount((int)EnumItemSet.Wood).ToString();
        smallFishCount.text = InventoryManager.CheckItemCount((int)EnumItemSet.Fish1).ToString();
        mediumFishCount.text = InventoryManager.CheckItemCount((int)EnumItemSet.Fish2).ToString();
        largeFishCount.text = InventoryManager.CheckItemCount((int)EnumItemSet.Fish3).ToString();
    }
}