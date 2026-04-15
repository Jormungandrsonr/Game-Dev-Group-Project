using UnityEngine;

public class InventoryManager
{
    static int[]items = new int[(int)EnumItemSet.endPoint];

    //add more fish
    //maybe add different tool types? future proofing, but may need to be in another file
    void Awake()
    {
        for(int i = 0; i < items.Length; i++)
        {
            items[i] = 0;
        }
        //code to read from save file
    }
    public static void AddItem(int itemType, int itemCount)
    {
        Debug.Log(itemCount + " " + items[itemType]);
        items[itemType] += itemCount;
        Debug.Log(items[itemType]+ " from add");

        //code to add item
    }
    public static bool RemoveItem(int itemType, int itemCount)
    {
        if(items[itemType] > itemCount){return false;}
        //code to remove item
        return true;
    }
    public static int CheckItemCount(int itemType)
    {
        Debug.Log(items[itemType] + " from dis one");
        return items[itemType];
    }
}
