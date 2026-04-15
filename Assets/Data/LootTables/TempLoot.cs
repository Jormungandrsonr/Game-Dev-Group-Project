using UnityEngine;

public class TempLoot : MonoBehaviour
{
    public int amount = 2;
    public short health = 3;
    //go into EnumItemSet and get the correspnding number for each tile
    public int type = 0;
    //THIS IS TEMPORARY AND IS NOT MEANT TO BE KEPT
    //WE'RE USING FOR THE FINAL PROJECT THOUGH
    //THIS SHOULD HONESTLY BE A TYPE OF JSON FILE
    public int GetAmount()
    {
        return amount;
    }
    public short GetHealth()
    {
        return health;
    }
    public int GetResourceType()
    {
        return type;
    }
}
