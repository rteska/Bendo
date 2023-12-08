using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStateCheck : MonoBehaviour
{
    public Inventory currPickupInventory;
    //public Inventory currRoomInteractInv;
    //public List<GameObject> roomObjects;

    //private bool stopEarly;
    private List<ItemData> currPickupList;
    //private List<ItemData> currRoomInteractList;

    // Start is called before the first frame update
    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        currPickupList = currPickupInventory.getInventory();
    }

    void Start()
    {
        //currPickupList = currPickupInventory.getInventory();
        //currRoomInteractList = currRoomInteractInv.getInventory();

        

        foreach (ItemData currItem in currPickupList)
        {
            currItem.hasItem = false;
            currItem.used = false;
            currItem.selected = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        foreach (GameObject currObject in roomObjects)
        {
            foreach ()
            {

            }
        }
        */
    }

}
