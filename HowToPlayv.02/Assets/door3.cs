using UnityEngine;
using System.Collections;

public class door3 : MonoBehaviour
{

    public Vector3 endPosition = Vector3.zero;
    public GameObject button;
    public GameObject button2;
    public GameObject combo;
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
        if (button.GetComponent<buttonTouched>().isOn && button2.GetComponent<buttonTouched>().isOn && combo.GetComponent<WireOnCombination>().unlocked)
        {
            timer += Time.deltaTime * speed;
            this.transform.position = Vector3.Lerp(startPosition, endPosition, timer);
            if (timer > 1)
            {
                closed = false;
                open = true;

                timer = 1;
            }
        }



    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(this.transform.position, endPosition + this.transform.position);
    }
}
