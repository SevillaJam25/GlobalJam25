using PhysicsCharacterController;
using UnityEngine;

public class DivingScript : MonoBehaviour
{
    private bool isDiving;
    Rigidbody rb; 
    CharacterManager characterManager;

    private void Start()
    {
        isDiving = false;
        rb = GetComponent<Rigidbody>();
        characterManager = GetComponent<CharacterManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (characterManager.GetGrounded())
        //    Debug.Log("CAN JUMP"); 
        if (isDiving)
        {
            characterManager.SetGrounded(false);
            characterManager.fallMultiplier = 0.0f;
            if(Input.GetKey(KeyCode.Space)) {
                Debug.Log("Space pressed"); 
                rb.AddForce(Vector3.up * 200, ForceMode.Acceleration);
            }
        }
        else
        {
            characterManager.fallMultiplier = 1.7f;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Sea")
            isDiving = true;   
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Sea")
            isDiving = false;
    }
}
