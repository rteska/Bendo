using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float mouseSensitivity;
    [SerializeField] private Material highwaySkybox;
    [SerializeField] private Material volcanoSkybox;

    public Inventory mainInventory;
    public Inventory interactableInv;
    public Canvas mainUI;
    public Canvas inventoryUI;
    public TextMeshProUGUI pickupText;
    public TextMeshProUGUI invSelectedText;
    public GameObject room1Manager;
    public SelectTracker overallTracker;

    private GameObject timerManager;
    private bool itemSelected;
    private bool inInv;
    private ItemData selectedCameraItem;
    private int currentRoom;
    private float cameraVerticalRotation = 0f;
    private RaycastHit hit;
    private LayerMask pickups;
    private LayerMask interactables;
    private LayerMask portal;
    private string selectedItem;

    private bool goodEnding;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pickups = LayerMask.GetMask("Pickup");
        interactables = LayerMask.GetMask("Interactable");
        portal = LayerMask.GetMask("Portal");
        itemSelected = false;
        inInv = false;
        currentRoom = 0; //0 is bedroom, 1 is highway, 2 is volcano, 3 is toy store
        goodEnding = false;
        timerManager = GameObject.FindGameObjectWithTag("Timer");
        timerManager.GetComponent<Timer>().Reset();
    }

    // Update is called once per frame
    void Update()
    {

        if (!inInv) //If not in inventory, have normal camera and player movement
        {
            CameraMove();
            if (Input.GetMouseButtonDown(0)) //If player left clicks
            {
                //If pickupable, then goes into inventory. Pickup items have tag "Pickup"
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20f, pickups)) //Raycast hits a pickupable item
                {
                    Pickup(hit);
                    Destroy(hit.transform.gameObject);
                }
                else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20f, interactables)) //Raycast hits an interactable object
                {
                    Interact(hit);
                }
                else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 20f, portal)) //Raycast hits a portal
                {
                    GoThroughPortal(hit);
                }
            }
        }
        else //Player is in inventory
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                inInv = false;
                mainUI.enabled = true;
                inventoryUI.enabled = false;
                pickupText.text = null;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }

        //Cheatcodes
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.G)) //Shortcut to good freedom
        {
            timerManager.GetComponent<Timer>().RecordTime();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("GoodFreedom");
        }else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKey(KeyCode.E)) //Shortcut to bad freedom
        {
            timerManager.GetComponent<Timer>().RecordTime();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("BadFreedom");
        }

    }

    //This moves the camera based on where the player points the mouse
    public void CameraMove()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraVerticalRotation -= mouseY;

        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;

        playerTransform.Rotate(Vector3.up * mouseX);

        if (Input.GetKey(KeyCode.I)) //Player goes into inventory
        {
            inInv = true;
            mainUI.enabled = false;
            inventoryUI.enabled = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void Pickup(RaycastHit item)
    {
        //item.collider.tag == "ToyKey"
        mainInventory.addItem(item.collider.tag);

        //Display "Picked up (Item name)"
        pickupText.text = "Picked up: " + item.collider.name;
        Debug.Log(mainInventory.displayText(item.collider.tag));   
        
    }
    
    public void Interact(RaycastHit whatHit)
    {

        //If item is not selected, then display some quick text
        if (!itemSelected)
        {
            Debug.Log(interactableInv.displayText(whatHit.collider.tag));
            pickupText.text = interactableInv.displayText(whatHit.collider.tag);
        }
        else
        {
            //If item is compatable, then do the proper interaction
            if (selectedCameraItem.usedFor == whatHit.collider.tag)
            {
                Debug.Log("Woo!");
                selectedCameraItem.used = true;
                selectedCameraItem.selected = false;
                selectedItem = null;
                invSelectedText.text = null;
                itemSelected = false;
                room1Manager.GetComponent<Room1Events>().DoChange(whatHit.collider.tag);
                overallTracker.isItemSelected = false;
                overallTracker.selectedItem = null;
            }
            else //If not, then display not compatable
            {
                Debug.Log("I can't use this item here");
                pickupText.text = "I can't use this item here";
            }
        }

    }

    public void GoThroughPortal(RaycastHit portalHit)
    {
        string portalTag = portalHit.collider.tag;

        switch (portalTag)
        {
            case "Vines": //Move Transform to Highway scene
                //playerTransform.position = new Vector3(-600f, 5.21f, 35.38f);
                playerTransform.position = new Vector3(-3112f, 5.21f, 35.38f);
                RenderSettings.skybox = highwaySkybox;
                break;
            case "Wires": //Go to Volcano
                playerTransform.position = new Vector3(3441.45f, 186.5682f, 119.68f);
                RenderSettings.skybox = volcanoSkybox;
                break;
            case "Bars": //Go to Toy Store
                playerTransform.position = new Vector3(-66.91f, 4.28f, -1514.4f);
                break;
            case "Padlock": //Freedom
                timerManager.GetComponent<Timer>().RecordTime();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                if (goodEnding) //The player put the Bendo back
                {
                    SceneManager.LoadScene("GoodFreedom");
                }
                else //The player didn't put the Bendo back
                {
                    SceneManager.LoadScene("BadFreedom");
                }
                break;
            case "Bedroom":
                playerTransform.position = new Vector3(6.09f, 4.28f, 35.38f);
                break;
            default:
                break;
        }
    }

    public void ItemHeld(ItemData selectedItem)
    {
        itemSelected = true;
        selectedCameraItem = selectedItem;

    }

    public void ItemUnheld(ItemData unselectedItem)
    {
        itemSelected = false;
        selectedCameraItem = null;
    }

    public void ChangeEnding()
    {
        goodEnding = true;
    }
}
