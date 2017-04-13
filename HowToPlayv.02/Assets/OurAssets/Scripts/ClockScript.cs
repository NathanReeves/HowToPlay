using UnityEngine;
using System;

public class ClockScript : MonoBehaviour
{

    private const float
        hoursToDegrees = 360f / 12f,
        minutesToDegrees = 360f / 60f,
        secondsToDegrees = 360f / 35f;

    public Transform seconds;
    public GameObject button;
    public GameObject[] lasers;
    int start = 0;
    int delta = 0;
    void Update()
    {
        Debug.Log(delta);
        
        if (button.GetComponent<buttonTouched>().isOn)
        {
            delta = DateTime.Now.Second - start;
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            if(start ==0)
                start = DateTime.Now.Second;
            seconds.localRotation =
                Quaternion.Euler(0f, 0f, (float)delta * secondsToDegrees);
        }
        else
        {
            delta = 0;
            start = 0;
        }
        Debug.Log(delta);
        if (delta > 3)
        {
            Debug.Log(delta);
           // button.GetComponent<buttonTouched>().turnoff();
        }

    }
}