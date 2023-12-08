using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public List<ItemData> items = new();
    //public List<ItemData> interactables = new();

    //Adds an item by marking the "hasItem" as true
    public void addItem(string itemTag)
    {
        //items.Add(newItem);
        foreach (ItemData tempItem in items)
        {
            if (itemTag == tempItem.tag)
            {
                tempItem.hasItem = true;
                break;
            }
        }
    }

    //Removes an item my marking "hasItem" as false
    public void removeItem(string itemTag)
    {
        //items.Remove(loseItem);
        foreach (ItemData tempItem in items)
        {
            if (itemTag == tempItem.tag)
            {
                tempItem.hasItem = false;
                break;
            }
        }
    }

    //Display the text of an item
    public string displayText(string itemTag)
    {
        foreach (ItemData tempItem in items)
        {
            if (itemTag == tempItem.tag)
            {

                return tempItem.description;
            }
        }
        return null;
    }

    public List<ItemData> getInventory()
    {
        return items;
    }

    public ItemData GetItem(string itemTag)
    {
        foreach (ItemData tempItem in items)
        {
            if (tempItem.tag == itemTag)
            {
                return tempItem;
            }
        }

        return null;
    }

}
