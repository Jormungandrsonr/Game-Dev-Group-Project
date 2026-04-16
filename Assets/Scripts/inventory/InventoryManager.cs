using UnityEngine;

/*
Public Class InventoryManager

This class is to store information regarding the Player's inventory.
The main storage part of the inventory is based on EnumItemSet.cs, 
an enumeration for the different items in the game.

How the Awake method currently works is to fill the array with 0's, 
however this is planned to be changed later on to read from a save file.

The number for itemType found in the functions is based on EnumItemSet.cs,
Wood is 1, Stone is 2, and others can be found in that file.
*/
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
    /*
        Function takes in an itemType, and how many of that item to insert into the array.
    */
    public static void AddItem(int itemType, int itemCount)
    {
        //Debug.Log(itemCount + " " + items[itemType]);
        items[itemType] += itemCount;
        //Debug.Log(items[itemType]+ " from add");

        //code to add item
    }

    /*
        Method takes in an itemType, and how many of that item to remove.
        Returns a boolean on whether or not that amount of item exists within the inventory.
    */
    public static bool RemoveItem(int itemType, int itemCount)
    {
        if(items[itemType] < itemCount || itemCount < 0){return false;}
        //code to remove item
        items[itemType] -= itemCount;
        return true;
    }
    /*
        Method checks and returns how many of a given itemType is in the player's inventory.
    */
    public static int CheckItemCount(int itemType)
    {
        Debug.Log(items[itemType] + " from check");
        return items[itemType];
    }
}
