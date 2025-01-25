using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    public CharacterController controller;
    Rigidbody rb;
    public float speed = 10f;
    public float gravity = -9.8f;
    public Transform groundCheck;
    public float groundDistance = 0.5f;
    public LayerMask groundMask;
    private Vector3 velocity;
    private bool isGrounded;
    public float jumpHeight = 3f;
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (isGrounded)
        {

            //rb.position = Vector3.Lerp(transform.position, transform.up, Time.deltaTime); 
        }
        
        
        
    }
    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded /*&& velocity.y < 0*/)
        {
            //velocity.y = 0f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        }
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Sea")
        {
            gravity = 0.002f;
            speed = 2f; 
        }
        else { gravity = -9.8f; }
    }
}
