using UnityEngine;

public class Floater : MonoBehaviour
{
    public Rigidbody rb_;
    [SerializeField] private float depthBeforeSubmerged = 0.5f;
    [SerializeField] private float displacementAmount = 3f;
    private int floaterCount = 4;
    [SerializeField] private float waterDrag = 1.0f;
    [SerializeField] private float waterAngularDrag = 0.5f;

    private void Start()
    {
        
    }
    private void FixedUpdate()
    {
        //Apply gravity at floaters position
        rb_.AddForceAtPosition(Physics.gravity/floaterCount, transform.position, ForceMode.Acceleration);

        float waveHeight = WaveManager.Instance.GetWaveHeight(transform.position.x);
        //Check if floater's y pos < wave height. If it is, it's underwater, so we'll have to add buoyancy
        if (transform.position.y < waveHeight)
        {
            //Approximates how much the object is submerged, which directly affects buoyancy
            float displacementMultiplier = Mathf.Clamp01((waveHeight -transform.position.y) / depthBeforeSubmerged) * displacementAmount;
            //Add force to y axis * the displacement multiplier we calculated
            rb_.AddForceAtPosition(new Vector3(0f, Mathf.Abs(Physics.gravity.y) * displacementMultiplier, 0f), transform.position, ForceMode.Acceleration);
            //Use acceleration forcemode bc buoyancy force shouldn't be affected by the object's mass

            rb_.AddForce(displacementMultiplier * -rb_.linearVelocity * waterDrag * Time.deltaTime, ForceMode.VelocityChange); 
            rb_.AddTorque(displacementMultiplier * -rb_.angularVelocity * waterAngularDrag * Time.deltaTime, ForceMode.VelocityChange); 
        }
    }
}
