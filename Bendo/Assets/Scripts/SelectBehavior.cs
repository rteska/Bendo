using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class SelectBehavior : MonoBehaviour
{
    public ItemData associatedItem;
    public TextMeshProUGUI selectedText;
    public Inventory mainInventory;
    public Camera mainCamera;
    public SelectTracker overallTracker;

    private List<ItemData> currInventory;
    private bool otherItemSelected;
    //public bool selectButton; //true, is select. False is unselect
    
    // Start is called before the first frame update
    void Start()
    {
        otherItemSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Select()
    {

        /*
        associatedItem.selected = true;

        //Check to make sure that no other item is selected
        currInventory = mainInventory.getInventory();
        foreach (ItemData currItem in currInventory)
        {
            if (currItem.selected && (associatedItem.tag != currItem.tag)) //Tells player to unselect before continuing
            {               
                selectedText.text = "Please un-select " + currItem.name + " before selecting another.";
                associatedItem.selected = false;
                otherItemSelected = true;
                break;
            }
        }

        if (!otherItemSelected)//Marks selected
        {
            Debug.Log("It is selecting");
            
            //associatedItem.selected = true;
            mainCamera.GetComponent<CameraMovement>().ItemHeld(associatedItem);
            selectedText.text = "Selected: " + associatedItem.itemName;
            otherItemSelected = false;
        }
        */

        if (overallTracker.isItemSelected) //There is an item already selected
        {
            selectedText.text = "Please un-select all selected items before choosing another.";
            associatedItem.selected = false;
        }
        else //Mark that item as selected
        {
            overallTracker.isItemSelected = true;
            overallTracker.selectedItem = associatedItem;
            mainCamera.GetComponent<CameraMovement>().ItemHeld(associatedItem);
            selectedText.text = "Selected: " + associatedItem.itemName;
        }


    }

    public void UnSelect()
    {

        /*
        associatedItem.selected = false;
        otherItemSelected = false;
        selectedText.text = null;
        mainCamera.GetComponent<CameraMovement>().ItemUnheld(associatedItem);
        */
        overallTracker.isItemSelected = false;
        overallTracker.selectedItem = null;
        associatedItem.selected = false;
        selectedText.text = null;
        mainCamera.GetComponent<CameraMovement>().ItemUnheld(associatedItem);
    }
}
