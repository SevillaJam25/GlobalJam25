using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rb_;
    [SerializeField] private float depthBeforeSubmerged = 1f;
    [SerializeField] private float displacementAmount = 3f;

    private void Start()
    {
        rb_ = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //Check if floater's y pos < 0. If it is, it's underwater, so we'll have to add buoyancy
        if (transform.position.y < 0f)
        {
            //Approximates how much the object is submerged, which directly affects buoyancy
            float displacementMultiplier = Mathf.Clamp01(-transform.position.y / depthBeforeSubmerged) * displacementAmount;
            //Add force to y axis * the displacement multiplier we calculated
            rb_.AddForce(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f),ForceMode.Acceleration);
            //Use acceleration forcemode bc buoyancy force shouldn't be affected by the object's mass
        }

    }
}
