using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private int keyCount;
    private Vector3 movement;
    private Vector3 lastLook;
    private Vector3 Last;
    private Rigidbody playerRigidBody;
    private int moveZone = 1;
    private bool isGrounded;
    bool turn;
    bool forward;
    Ray camRay;
    private float camRayLength = 100f;
    private int floorMask;
    public Text keys;

    void Awake()
    {
        keyCount = 0;
        keys.text = "Keys: " + keyCount.ToString();
        floorMask = LayerMask.GetMask("Floor");
        playerRigidBody = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -25f, 0);
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
        keys.gameObject.SetActive(false);
    }

    void FixedUpdate()
    {
        //Debug.Log("Player pos: " + this.transform.position);

        // Use platformer controls when in platformer zone
        if (moveZone == 1 || moveZone == 2)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            if (moveHorizontal < 0)
            {
                GetComponent<Animator>().SetBool("IsWalking", true);
                if (forward)
                    turn = true;
                forward = false;

            }
            else if (moveHorizontal > 0)
            {
                GetComponent<Animator>().SetBool("IsWalking", true);
                if (!forward)
                    turn = true;
                forward = true;

            }
            else
            {
                GetComponent<Animator>().SetBool("IsWalking", false);
            }
            MovePlatformer(moveHorizontal);
        }
        // Use twin stick club controls when in twin stick club zone
        else if (moveZone == 3)
        {
            //gameObject.transform.localScale = new Vector3(4,4,4);
            moveSpeed = 30;
            jumpSpeed = 500;
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            MoveTwinStickClub(moveHorizontal, moveVertical);
            TurnTwinStickClub();
            if (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0)
            {
                GetComponent<Animator>().SetBool("IsWalking", true);
                if (forward)
                    turn = true;
                forward = false;

            }
            else
            {
                GetComponent<Animator>().SetBool("IsWalking", false);
            }

        }
        // Use twin stick shooting controls when in twin stick shoot zone
        else if (moveZone == 4)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            //twinCam.GetComponent<Camera>().transform.forward.y  
            MoveTwinStickShoot(moveHorizontal, moveVertical);
            TurnTwinStickShoot();
        }
        // Use 3rd/1st person controls when in 3rd/1st person zone
        else if (moveZone == 5 | moveZone == 6)
        {
            moveSpeed = 40;
            jumpSpeed = 800;
            // Move player forward, back, left, right
            float moveForwardBack = Input.GetAxis("Vertical");
            float strafeLeftRight = Input.GetAxis("Horizontal");
            if (Mathf.Abs(strafeLeftRight) > 0 || Mathf.Abs(moveForwardBack) > 0)
            {
                GetComponent<Animator>().SetBool("IsWalking", true);
                if (forward)
                    turn = true;
                forward = false;

            }
            else
            {
                GetComponent<Animator>().SetBool("IsWalking", false);
            }
            Move3rd(strafeLeftRight, moveForwardBack);
            Turn3rd(strafeLeftRight, moveForwardBack);
        }

        // Check for jumping
        if (isGrounded && (int)playerRigidBody.velocity.y == 0 && moveZone > 1 && (Input.GetKey("joystick button 0") || Input.GetKey("space")))
        {
            Jump();
        }


        //Check for turning
        if (turn)
        {
            Vector3 rot = transform.rotation.eulerAngles;
            rot = new Vector3(rot.x, rot.y + 180, rot.z);
            transform.rotation = Quaternion.Euler(rot);
            turn = false;
        }
    }



    private void MovePlatformer(float horiz)
    {
        
        // Set movement/rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

        // Set movement according to input
        movement.Set(0f, playerRigidBody.velocity.y, horiz*moveSpeed);

        // Normalize movement
        //movement = movement * moveSpeed * Time.deltaTime;
        playerRigidBody.velocity = movement;
        // Move player
        //playerRigidBody.MovePosition(transform.position + movement);
    }
    private void TurnTwinStickClub()
    {
        //playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //Vector3 target = Input.GetAxis("Fire2")*Vector3.forward + Input.GetAxis("Fire1") * Vector3.right ;
        //target = target.normalized * moveSpeed * Time.deltaTime;
        //lastLook = target;
        //playerRigidBody.transform.Rotate(Input.GetAxis("Fire1"), Input.GetAxis("Fire2"), 0);
        //playerRigidBody.MoveRotation(Quaternion.LookRotation(target));
        Vector3 cameraForward = twinCam.GetComponent<Camera>().transform.TransformDirection(Vector3.forward);
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);

        Vector3 target = Input.GetAxis("Fire2") * cameraRight + Input.GetAxis("Fire1") * cameraForward * -1;
        if (target.x == 0 && target.z == 0)
        {
            if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
                target = Last;
            else
            {
                target = Input.GetAxis("Horizontal") * cameraRight + Input.GetAxis("Vertical") * cameraForward;
                Last = target;
            }
        }
        else
        {
            
            Last = target;
        }
        //target = target.normalized * moveSpeed * Time.deltaTime;

        playerRigidBody.MoveRotation(Quaternion.LookRotation(target));
        
    }
    private void MoveTwinStickClub(float horiz, float vert)
    {

        Transform cameraTransform = twinCam.GetComponent<Camera>().transform;
        // forward of the camera on the x-z plane
        Vector3 cameraForward = twinCam.GetComponent<Camera>().transform.TransformDirection(Vector3.forward);
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);


        // Set rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Set movement according to input
        movement.Set(-vert, 0f, horiz);

        // Normalize movement

        var axis = transform.position;
        Vector3 target = horiz * cameraRight + vert * cameraForward;
        target = target.normalized * moveSpeed * Time.deltaTime;
        lastLook = target;
        // Move player
        playerRigidBody.MovePosition(transform.position + target);
        //playerRigidBody.MoveRotation(Quaternion.LookRotation(target));
    }
    private void Move3rd(float horiz, float vert)
    {

        Transform cameraTransform = thirdCam.GetComponent<Camera>().transform;
        // forward of the camera on the x-z plane
        Vector3 cameraForward = thirdCam.GetComponent<Camera>().transform.TransformDirection(Vector3.forward);
        cameraForward.y = 0f;
        cameraForward = cameraForward.normalized;

        Vector3 cameraRight = new Vector3(cameraForward.z, 0.0f, -cameraForward.x);


        // Set rotation constraints
        playerRigidBody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

        // Set movement according to input
        movement.Set(-vert, 0f, horiz);

        // Normalize movement

        var axis = transform.position;

        Vector3 target = horiz * cameraRight + vert * cameraForward;
        //Vector3 target2 = horiz * Vector3.right + vert * Vector3.forward;

        target = target.normalized * moveSpeed * Time.deltaTime;
        lastLook = target;
        // Move player
        playerRigidBody.MovePosition(transform.position + target);

    }
    private void Turn3rd(float horizontal, float vertical)
    {


        // Get camera forward direction, without vertical component.

        Vector3 forward = thirdCam.transform.forward;

        // Player is moving on ground, Y component of camera facing is not relevant.
        forward.y = 0.0f;
        forward = forward.normalized;

        // Calculate target direction based on camera forward and direction key.
        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        Vector3 targetDirection;
        targetDirection = forward * vertical + right * horizontal;
        if (targetDirection.x == 0 && targetDirection.z == 0)
        {
            targetDirection = Last;
        }
        else
        {
            Last = targetDirection;
        }

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        playerRigidBody.MoveRotation(targetRotation);

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
        playerRigidBody.MovePosition(movement);
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
        // GENRE SWITCHING

        if (other.gameObject.CompareTag("Enter2D"))
        {
            // Switch to 2D platformer controls (add jump)
            moveZone = 2;
        }
        if (other.gameObject.CompareTag("EnterTwinStickClub"))
        {
            // Switch to twin stick controls (only weapon swinging)
            moveZone = 3;

            // Show sword and enable melee attack
            this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(3).gameObject.SetActive(true);

            // Switch to twin stick camera
            platCam.enabled = false;
            twinCam.enabled = true;
            thirdCam.enabled = false;
            firstCam.enabled = false;
            keys.gameObject.SetActive(true);
        }
        if (other.gameObject.CompareTag("EnterTwinStickShoot"))
        {
            // Switch to twin stick shooter controls (add shooting)
            moveZone = 4;

            // Hide melee weapon and disable melee attack
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(1).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(2).gameObject.SetActive(false);
            this.gameObject.transform.GetChild(3).gameObject.SetActive(false);

            // Show gun and enable shooting attack
            this.gameObject.transform.GetChild(4).gameObject.SetActive(true);
            this.gameObject.transform.GetChild(5).gameObject.SetActive(true);
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
        if (other.gameObject.CompareTag("Key"))
        {
            other.gameObject.SetActive(false);
            keyCount++;
            keys.text = "Keys: " + keyCount.ToString();
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerRigidBody.velocity = new Vector3(playerRigidBody.velocity.x, 0, playerRigidBody.velocity.z);
                if (!isGrounded)
                    isGrounded = true;                  
            GetComponent<Animator>().SetBool("Grounded", true);
            GetComponent<Animator>().SetBool("Jump", false);
            //playerRigidBody.transform.position = collision.transform.position;
        }
       
            if (collision.gameObject.CompareTag("MovingGround"))
        {
            isGrounded = true;
            if (!isGrounded)
                isGrounded = true;
            GetComponent<Animator>().SetBool("Grounded", true);
            GetComponent<Animator>().SetBool("Jump", false);
            playerRigidBody.transform.parent = collision.transform;
        }
        if (collision.gameObject.CompareTag("Door") && keyCount > 0)
        {
            collision.gameObject.SetActive(false);
            keyCount--;
            keys.text = "Keys: " + keyCount.ToString();

        }
    }

    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            
                if (!isGrounded)
                    isGrounded = true;
            
            GetComponent<Animator>().SetBool("Grounded", true);
            GetComponent<Animator>().SetBool("Jump", false);
            Debug.Log(playerRigidBody.velocity.x + " "+ playerRigidBody.velocity.y+ " "+ playerRigidBody.velocity.z);
        }
        if (collision.gameObject.CompareTag("MovingGround"))
        {
            isGrounded = true;
            if (!isGrounded)
                isGrounded = true;
            GetComponent<Animator>().SetBool("Grounded", true);
            GetComponent<Animator>().SetBool("Jump", false);
            playerRigidBody.transform.parent = collision.transform;
        }
        /*
		if (collision.gameObject.CompareTag ("Door") && keyCount > 0 && Input.GetKey("o")) 
		{
			collision.gameObject.SetActive (false);
			keyCount--;
			keys.text = "Keys: " + keyCount.ToString ();

		}
		*/
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {

            isGrounded = false;
            GetComponent<Animator>().SetBool("Grounded", false);
        }
        if (collision.gameObject.CompareTag("MovingGround"))
        {

            isGrounded = false;
            GetComponent<Animator>().SetBool("Grounded", false);
            playerRigidBody.transform.parent = null;
        }
    }

    private void Jump()
    {
        GetComponent<Animator>().SetBool("Jump", true);
        GetComponent<Animator>().SetBool("Grounded", false);
        //isGrounded = false;
        playerRigidBody.AddForce(Vector3.up * jumpSpeed);
    }
}
