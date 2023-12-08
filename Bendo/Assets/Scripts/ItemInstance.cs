using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemData itemType;
    public string usedOn; //Passes through the tag of the object
    /*
    public ItemInstance(ItemData itemData)
    {
        itemType = itemData;

    }
    */
}
