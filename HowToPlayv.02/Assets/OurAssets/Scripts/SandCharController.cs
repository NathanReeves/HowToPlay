using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandCharController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private Camera platCam;
    [SerializeField]
    private Camera twinCam;
    [SerializeField]
    private Camera thirdCam;
    [SerializeField]
    private Camera firstCam;

    private Vector3 movement;
    private Rigidbody playerRigidBody;
    private int moveZone = 1;
    private bool isGrounded;
	bool turn;
	bool forward;
    Ray camRay;
    private float camRayLength = 100f;
    private int floorMask;

    void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidBody = GetComponent<Rigidbody>();

        platCam = platCam.GetComponent<Camera>();
        twinCam = twinCam.GetComponent<Camera>();
        thirdCam = thirdCam.GetComponent<Camera>();
        firstCam = firstCam.GetComponent<Camera>();

        platCam.enabled = true;
        twinCam.enabled = false;
        thirdCam.enabled = false;
        firstCam.enabled = false;
		turn = false;
		forward = true;
        moveZone = 2;
    }

    void FixedUpdate()
    {
        // Use platformer controls when in platformer zone
        if (moveZone == 1 || moveZone == 2)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");

            MovePlatformer(moveHorizontal);
        }
        // Use twin stick club controls when in twin stick club zone
        else if (moveZone == 3)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVerical = Input.GetAxis("Vertical");

            MoveTwinStickClub(moveHorizontal, moveVerical);
        }
        // Use twin stick shooting controls when in twin stick shoot zone
        else if (moveZone == 4)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVerical = Input.GetAxis("Vertical");

            MoveTwinStickShoot(moveHorizontal, moveVerical);
            TurnTwinStickShoot();
        }
        // Use 3rd/1st person controls when in 3rd/1st person zone
        else if (moveZone == 5 | moveZone == 6)
        {
            // Move player forward, back, left, right
            float moveForwardBack = Input.GetAxis("Vertical");
            float strafeLeftRight = Input.GetAxis("Horizontal");

            MoveThirdFirst(moveForwardBack, strafeLeftRight);
        }

        // Check for jumping
        if (isGrounded && moveZone > 1 &&(Input.GetKey("joystick button 0") || Input.GetKey("space")))
        {
            Jump();
        }

		if (Input.GetAxis ("Horizontal") < 0) {
			GetComponent<Animator> ().SetBool ("IsWalking", true);
			if (forward)
				turn = true;
			forward = false;

		} else if (Input.GetAxis ("Horizontal") > 0) {
			GetComponent<Animator> ().SetBool ("IsWalking", true);
			if (!forward)
				turn = true;
			forward = true;

		} else {
			GetComponent<Animator> ().SetBool ("IsWalking", false);
		}
		//Check for turning
		if (turn) {
			Vector3 rot = transform.rotation.eulerAngles;
			rot = new Vector3(rot.x,rot.y+180,rot.z);
			transform.rotation = Quaternion.Euler(rot);
			//playerRigidbody.MoveRotation (transform.rota);
			turn = false;
		}
    }

    private void MovePlatformer(float horiz)
    {
        Physics.gravity = new Vector3(0, -25f, 0);
        // Set movement/rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

        // Set movement according to input
        movement.Set(0f, 0f, horiz);

        // Normalize movement
        movement = movement * moveSpeed * Time.deltaTime;

        // Move player
        playerRigidBody.MovePosition(transform.position + movement);
    }

    private void MoveTwinStickClub(float horiz, float vert)
    {     
        // Set rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Set movement according to input
        movement.Set(-vert, 0f, horiz);

        // Normalize movement
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Move player
        playerRigidBody.MovePosition(transform.position + movement);
    }

    private void MoveTwinStickShoot(float horiz, float vert)
    {
        // Set rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Set movement according to input
        movement.Set(-vert, 0f, horiz);

        // Normalize movement
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Move player
        playerRigidBody.MovePosition(transform.position + movement);
    }

    private void TurnTwinStickShoot()
    {
        // Create a ray
        camRay = twinCam.ScreenPointToRay(Input.mousePosition);

        // Setup RaycastHit to get info from ray
        RaycastHit floorHit;

        // If raycast hits anything...
        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
        {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0; // Make sure player isn't leaning in y axis

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidBody.MoveRotation(newRotation);
        }
    }

    private void MoveThirdFirst(float forBack, float strafe)
    {
        // Lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;

        // Set rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Set movement according to input
        //movement.Set(forBack, 0f, -strafe);

        // Normalize movement
        movement = movement.normalized * moveSpeed * Time.deltaTime;

        // Move player
        //playerRigidBody.MovePosition(transform.position + movement);
        transform.Translate(forBack, 0f, -strafe);
    }

    // Triggers for camera and move changes
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enter2D"))
        {
            // Switch to 2D platformer controls (add jump)
            moveZone = 2; 
        }
        if (other.gameObject.CompareTag("EnterTwinStickClub"))
        {
            // Switch to twin stick controls (only weapon swinging)
            moveZone = 3;
            // Switch to twin stick camera
            platCam.enabled = false;
            twinCam.enabled = true;
            thirdCam.enabled = false;
            firstCam.enabled = false;

            // Show arm and sword
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("EnterTwinStickShoot"))
        {
            // Switch to twin stick shooter controls (add shooting)
            moveZone = 4;

            // Switch from melee to shooting
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("Enter3rdPerson"))
        {
            // Switch to 3rd person / 1st person controls
            moveZone = 5;
            // Switch to 3rd person camera
            platCam.enabled = false;
            twinCam.enabled = false;
            thirdCam.enabled = true;
            firstCam.enabled = false;
        }
        if (other.gameObject.CompareTag("Enter1stPerson"))
        {
            // Switch to 1st person camera
            platCam.enabled = false;
            twinCam.enabled = false;
            thirdCam.enabled = false;
            firstCam.enabled = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!isGrounded && playerRigidBody.velocity.y == 0)
                isGrounded = true;
            GetComponent<Animator>().SetBool("Grounded", true);
            GetComponent<Animator>().SetBool("Jump", false);
        }
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            
            isGrounded = false;
            GetComponent<Animator>().SetBool("Grounded", false);
        }
    }

    private void Jump()
    {
		GetComponent<Animator> ().SetBool ("Jump", true);
		GetComponent<Animator> ().SetBool ("Grounded", false);
        isGrounded = false;
        playerRigidBody.AddForce(Vector3.up * jumpSpeed);
    }
}
