﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slowbuttondoor : MonoBehaviour
{

    public Vector3 endPosition = Vector3.zero;
    public GameObject button;
    public float speed = 1;
    private float timer = 0;
    private bool buttonOn;
    private Vector3 startPosition = Vector3.zero;
    private bool closed = true;
    private bool open = false;
    private buttonTouchedPlatformer script;
    // Use this for initialization
    void Start()
    {
        script = button.GetComponent<buttonTouchedPlatformer>();
        startPosition = this.gameObject.transform.position;
        endPosition = endPosition + startPosition;

        float distance = Vector3.Distance(startPosition, endPosition);
        if (distance != 0)
        {
            speed = speed / distance;
        }
    }

    // Update is called once per frame
    void Update()
    {
        buttonOn = script.isOn;
        if (buttonOn)
        {
            timer += Time.deltaTime * speed;
            if (closed)
            {
                this.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
                if (timer > 1)
                {
                    closed = false;
                    open = true;

                    timer = 0;
                }
            }


        }



    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
    }
}