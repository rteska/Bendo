using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayTime : MonoBehaviour
{
    public GameObject timer;
    public TextMeshProUGUI mainText;

    
    // Start is called before the first frame update
    void Start()
    {
        mainText.text = mainText.text + " " + timer;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
