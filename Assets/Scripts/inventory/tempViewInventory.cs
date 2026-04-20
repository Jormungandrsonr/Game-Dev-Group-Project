using UnityEngine;

public class tempViewInventory : MonoBehaviour
{
    //temp GUI
    void OnGUI()
    {
        GUIStyle style = new GUIStyle();
            style.fontSize = 32;
            style.normal.textColor = Color.red;
            GUI.Label(new Rect(10, Screen.height - 40, 200 ,20), InventoryManager.CheckItemCount(1) + "Wood", style);
            GUI.Label(new Rect(10, Screen.height - 80, 200 ,20), InventoryManager.CheckItemCount(2) + "Stone", style);
            GUI.Label(new Rect(10, Screen.height - 120, 200 ,20),InventoryManager.CheckItemCount(3) + "Fish1", style);
            GUI.Label(new Rect(10, Screen.height - 160, 200 ,20), InventoryManager.CheckItemCount(4) + "Fish2", style);
            GUI.Label(new Rect(10, Screen.height - 200, 200 ,20), InventoryManager.CheckItemCount(5) + "Fish3", style);
            GUI.Label(new Rect(10, Screen.height - 240, 200 ,20), InventoryManager.CheckItemCount(6) + "Gold", style);
        
    }
}
