using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller_; //Moves game object in given direction
    private Vector3 playerVelocity_;
    private bool isGrounded_;
    public Transform boatSpot;
    [SerializeField] private float speed_ = 2.0f;
    [SerializeField] private float jumpHeight_ = 1.0f;
    [SerializeField] private float gravity_ = -9.81f;
    //[SerializeField] private Camera camera_;
    bool isOnBoat;

    void Start()
    {//Init char controller
        controller_ = gameObject.AddComponent<CharacterController>();
        isOnBoat = true;
        //camera_ = gameObject.GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded_ = controller_.isGrounded;
        if (isGrounded_ && playerVelocity_.y < 0) playerVelocity_.y = 0;

        //Movement in two axis
        float xMov = Input.GetAxis("Horizontal");
        float yMov = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(xMov, 0, yMov);
        //Quaternion camRot = Quaternion.Euler(0, camera_.transform.eulerAngles.y, 0);
        controller_.Move(/*camRot **/ move * Time.deltaTime * speed_);
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move; //Probably will change this for backwards movement
        //}
        if (Input.GetButtonDown("Jump") && !isGrounded_)
        {
            playerVelocity_.y += Mathf.Sqrt(jumpHeight_ * -2.0f * gravity_);
        }
        playerVelocity_.y += gravity_ * Time.deltaTime;
        controller_.Move(playerVelocity_ * Time.deltaTime);
        HandleInput(); 
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
            transform.SetPositionAndRotation(boatSpot.position, transform.rotation);

    }

}
