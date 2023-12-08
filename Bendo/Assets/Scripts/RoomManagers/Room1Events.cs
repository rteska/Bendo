using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Events : MonoBehaviour
{
    /*
     * This manager takes care of tracking all of the puzzles and causing the necessary changes for all rooms.
     * 
     */
    public Camera mainCamera;

    public Animator boxLidOpen;
    public Animator dresserDoorLeft;
    public Animator dresserDoorRight;
    public Animator carTrunkAnimator;
    public GameObject vines;
    public GameObject wires;
    public GameObject bars;
    public GameObject padlock;

    public GameObject potLamb;
    public GameObject potCarrot;
    public GameObject potBroth;
    public ItemData itemLamb;
    public ItemData itemCarrot;
    public ItemData itemBroth;
    public GameObject normalState;
    public GameObject destroyedState;

    private int potTracker;

    public GameObject elevenBDCard;
    public GameObject twelveBDCard;
    public GameObject thirteenBDCard;
    public GameObject bendo;
    public GameObject flapBlocker;
    public Animator flapTurner;

    //CarToySection, DollToySection, MiscToySection
    private int correctCardTracker;
    
    // Start is called before the first frame update
    void Start()
    {
        potTracker = 0;
        correctCardTracker = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (correctCardTracker == 3)
        {
            flapTurner.SetBool("OpenFlap", true);
            flapBlocker.SetActive(false);
            correctCardTracker = 0;
        }
        
    }

    public void DoChange(string itemToChange)
    {
        switch (itemToChange)
        {
            case "ToyBox": //Toy Key to Toy Box
                boxLidOpen.SetBool("OpenBox", true);
                break;
            case "Dresser": //Pet Collar to Dresser
                dresserDoorLeft.SetBool("DoorOpen", true);
                dresserDoorRight.SetBool("DoorOpen", true);
                break;
            case "Vines": //Safety Scissors to Vines
                vines.SetActive(false);
                break;
            case "Trunk": //Car Keys to Trunk
                carTrunkAnimator.SetBool("TrunkOpen", true);
                break;
            case "Wires": //Wire Cutters to Wires
                wires.SetActive(false);
                break;
            case "Pot": //Check the three ingredients to see which one was used and then activate the proper one
                if (itemLamb.used == true) //Lamb was used
                {
                    potLamb.SetActive(true);
                }
                if (itemCarrot.used == true) //Carrot was used
                {
                    potCarrot.SetActive(true);
                }
                if (itemBroth.used == true) //Broth was used
                {
                    potBroth.SetActive(true);
                }

                potTracker++;

                if (potTracker == 3) //If all ingredients are in there, then carry out the changes
                {
                    normalState.SetActive(false);
                    destroyedState.SetActive(true);
                }
                break;
            case "Bars": //Vial of Acid to Bars
                bars.SetActive(false);
                break;
            case "CarToySection": //Put 12th birthday card in spot
                twelveBDCard.SetActive(true);
                correctCardTracker++;
                break;
            case "DollToySection": //Put 11th birthday card in spot
                elevenBDCard.SetActive(true);
                correctCardTracker++;
                break;
            case "MiscToySection": //Put 13th birthday card in spot
                thirteenBDCard.SetActive(true);
                correctCardTracker++;
                break;
            case "BendoSpot": //The player put the Bendo back
                bendo.SetActive(true);
                mainCamera.GetComponent<CameraMovement>().ChangeEnding();
                break;
            case "Padlock": //Padlock Key to Padlock
                padlock.SetActive(false);
                break;
            default:
                break;
        }
    }

    
}
