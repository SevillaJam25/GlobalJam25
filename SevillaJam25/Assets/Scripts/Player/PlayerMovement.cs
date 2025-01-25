using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller_; //Moves game object in given direction

    //private Vector3 playerVelocity_;
    //private bool isGrounded_;
    [Header("Movement")]
    public Transform orientation_;
    float xMov, yMov;
    [SerializeField] private float speed_ = 3.0f;
    private Vector3 moveDirection;
    Rigidbody rb_;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    bool isGrounded;
    public float drag_ = 5;

    [SerializeField] private float underwaterSpeed_ = 2.0f;
    [SerializeField] private float rotSpeed_ = 90f;
    [SerializeField] private float jumpHeight_ = 10.0f;
    [SerializeField] private float gravity_ = -9.81f;
    //[SerializeField] private Camera camera_;
    bool isOnBoat;
    bool isUnderwater;
    public Transform boatSpot;

    void Start()
    {//Init char controller
        rb_ = GetComponent<Rigidbody>();
        rb_.freezeRotation = true;
        controller_ = gameObject.AddComponent<CharacterController>();
        isOnBoat = true;
        isUnderwater = false;
        //camera_ = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ground check: Raycast que se proyecta en direccion hacia abajo (+ un poquito mas) de la dimension del player
        //isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.2f, groundLayer);

        HandleMovement();
        HandleInput();

        if (isGrounded) //Ground friction
            rb_.linearDamping = drag_;
        else rb_.linearDamping = 0;
    }

    private void HandleInput()
    {
        xMov = Input.GetAxis("Horizontal");
        yMov = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.E))
            transform.SetPositionAndRotation(boatSpot.position, transform.rotation);

    }
    private void HandleMovement()
    {
        //Calc mvt direction to always move in direction im looking at
        moveDirection = orientation_.forward * yMov + orientation_.right * xMov;
        rb_.AddForce(moveDirection.normalized * 10f, ForceMode.Force);

        //if (controller_.isGrounded)
        //{

        //    playerVelocity_ = transform.forward * speed_ * yMov;
        //    //turnVelocity_ = transform.right * rotSpeed_ * xMov;

        //    if (Input.GetButtonDown("Jump"))
        //    {
        //        playerVelocity_.y = jumpHeight_;
        //    }
        //}
        //playerVelocity_.y += gravity_ * Time.deltaTime;
        //controller_.Move(playerVelocity_ * Time.deltaTime);
        //transform.Rotate(turnVelocity_ * Time.deltaTime);

        //if (isUnderwater)
        //{
        //    //Change velocity - slower
        //    controller_.Move(move * Time.deltaTime * underwaterSpeed_);
        //    //Grounded does NOT depend on controller
        //    isGrounded_ = playerVelocity_.y > 0.0f ? true : false;

        //    if (Input.GetButtonDown("Jump") && !isGrounded_)
        //    {
        //        //Half gravity so it looks like water resistance
        //        playerVelocity_.y += Mathf.Sqrt(jumpHeight_ * -2.0f * (gravity_ * 0.05f));
        //    }
        //    playerVelocity_.y += (gravity_ * 0.05f)  * Time.deltaTime;


        //}
        //else //NOT UNDERWATER! ON BOAT
        //{
        //    //Normal speed
        //    controller_.Move(move * Time.deltaTime * speed_);
        //    //Grounded depends on controller
        //    //isGrounded_ = controller_.isGrounded;
        //    if(controller_.isGrounded)
        //    {
        //        playerVelocity_ = transform.forward * speed_ * xMov;
        //        if (Input.GetButtonDown("Jump"))
        //        {
        //            playerVelocity_.y = jumpHeight_; 
        //        }
        //    }

        //    //if (isGrounded_ && playerVelocity_.y < 0) playerVelocity_.y = 0;

        //    //if (Input.GetButtonDown("Jump") && !isGrounded_)
        //    //{
        //    //    playerVelocity_.y += Mathf.Sqrt(jumpHeight_ * -2.0f * gravity_);
        //    //}

        //    playerVelocity_.y += gravity_ * Time.deltaTime;

        //}


        //controller_.Move(playerVelocity_ * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 4) //Layer 4 is water
        {
            isUnderwater = true;
            ////Change velocity 
            //isGrounded_ = false;

            ////Movement in two axis
            //float xMov = Input.GetAxis("Horizontal");
            //float yMov = Input.GetAxis("Vertical");
            //Vector3 move = new Vector3(xMov, 0, yMov);

            //controller_.Move(move * Time.deltaTime * underwaterSpeed_);
            ////if (move != Vector3.zero)
            ////{
            ////    gameObject.transform.forward = move; //Probably will change this for backwards movement
            ////}
            //if (Input.GetButtonDown("Jump") && !isGrounded_)
            //{
            //    //Half gravity so it looks like water resistance

            //    playerVelocity_.y += Mathf.Sqrt(jumpHeight_ * -2.0f * (gravity_ * 0.5f));
            //}
            //playerVelocity_.y += gravity_ * Time.deltaTime;


            //controller_.Move(playerVelocity_ * Time.deltaTime);
        }
    }

}
