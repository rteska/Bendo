using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private float timer = 0;
    private float finalTime = 0;
    private bool inEnding = false;
    private int numOfTimers;
    //private GameObject endingText;
    private TextMeshProUGUI endingText;

    private static Timer instance;

    
    // Start is called before the first frame update
    private void Start()
    {
        

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            //DestroyObject(gameObject);
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);



        /*numOfTimers = FindObjectsOfType<Timer>().Length;

        if (numOfTimers > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        */

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        
    }

    private void FixedUpdate()
    {
        if (inEnding)
        {
            //endingText = GameObject.FindWithTag("FinalText");
            endingText = GameObject.FindWithTag("FinalText").GetComponent<TextMeshProUGUI>();
            endingText.text += " " + finalTime.ToString("F2") + " minutes";
            inEnding = false;
        }
    }

    public void RecordTime()
    {
        finalTime = timer;
        finalTime /= 60; //Get time in minutes
        inEnding = true;
    }

    public float GetTime()
    {
        return finalTime;
    }

    public void Reset()
    {
        timer = 0;
    }
}
