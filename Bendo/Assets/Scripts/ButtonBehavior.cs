using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ButtonBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TextMeshProUGUI itemDescription;
    public ItemData buttonItem;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (buttonItem.used)
        {
            this.GetComponent<Button>().interactable = false;
        }
    }

    //When the mouse highlights the button, then display the item's description
    public void OnPointerEnter(PointerEventData pointerEnter)
    {
        itemDescription.text = buttonItem.description;
    }

    //Once the mouse leaves, then remove the text
    public void OnPointerExit(PointerEventData pointerExit)
    {
        itemDescription.text = null;
    }
}
