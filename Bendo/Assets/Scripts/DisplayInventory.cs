using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public Inventory currInventory;
    public TextMeshProUGUI itemNameDisplay;
    public TextMeshProUGUI itemDescDisplay;
    public GameObject spriteDisplay;
    public List<GameObject> allButtons;

    private List<ItemData> currInvState;
    private bool active;
    
    // Start is called before the first frame update
    void Start()
    {
        active = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the canvas is active
        if (active)
        {
            UpdateInv();

            if (Input.GetKey(KeyCode.Escape))
            {
                active = false;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.I))
            {
                active = true;
            }
        }
    }

    private void UpdateInv()
    {
        //Grabs the inventory to assess its current state
        currInvState = currInventory.getInventory();
        foreach (ItemData nextItem in currInvState)
        {
            if (nextItem.hasItem == true) //If there are any items that the player has now, then update the buttons to be active
            {
                foreach (GameObject nextButton in allButtons)
                {
                    if (nextItem.tag == nextButton.tag)
                    {
                        nextButton.SetActive(true);

                        /*
                        if (nextItem.used) //Makes sure that if there is an item that is used, then disable its button
                        {
                            nextButton.GetComponent<Button>().enabled = false;
                        }
                        \*/
                        break;
                    }

                    
                }
            }
        }
    }

    

}
