using UnityEngine;
using System.Collections;

public class triggerDoor : MonoBehaviour
{

    public Vector3 endPosition = Vector3.zero;
    public bool triggered;
    public GameObject door;
    public float speed = 1;
    private float timer = 0;
    private bool buttonOn;
    private Vector3 startPosition = Vector3.zero;
    private bool closed = true;
    private bool open = false;
    // Use this for initialization
    void Start()
    {
        triggered = false;
        startPosition = door.transform.position;
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
        //buttonOn = script.isOn;
        if (triggered)
        {
            timer += Time.deltaTime * speed;
            if (closed)
            {
                door.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
                if (timer > 1)
                {
                    closed = false;
                    open = true;

                    timer = 1;
                }
            }


        }


    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Player")
        {
            triggered = true;
        }
    }

}
