using UnityEngine;
using System.Collections;

public class ButtonDoorScript : MonoBehaviour
{

    public Vector3 endPosition = Vector3.zero;
    public GameObject button;
    public float speed = 1;
    private float timer = 0;
    private bool buttonOn;
    private Vector3 startPosition = Vector3.zero;
    private bool closed = true;
    private bool open = false;
    // Use this for initialization
    void Start()
    {
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
        buttonOn = button.GetComponent<buttonTouched>().isSinking;
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

                    timer = 1;
                }
            }
            
            
        }
        else
        {
            if(open)
            {
                this.transform.position = Vector3.Lerp(endPosition, startPosition, timer);
                if (timer > 1)
                {
                    closed = true;
                    open = false;
                    timer = 1;
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
