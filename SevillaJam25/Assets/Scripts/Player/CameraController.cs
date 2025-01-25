using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    [Range(500f, 2500f)][SerializeField] float sensitivity = 2f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
    // [Range(0f, 90f)][SerializeField] float yRotationLimit = 55f;

    // Vector2 rotation = Vector2.zero;
    // const string xAxis = "Mouse X"; //Strings in direct code generate garbage, storing and re-using them creates no garbage
    // const string yAxis = "Mouse Y";

    public Transform playerBody;    
    private float xRotation = 0f;


    void Start()
    {
        playerBody = transform.parent;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita la rotación en el eje X

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Rotación de la cámara
        playerBody.Rotate(Vector3.up * mouseX); // Rotación del personaje


        // rotation.x += Input.GetAxis(xAxis) * sensitivity;
        // rotation.y += Input.GetAxis(yAxis) * sensitivity;
        // rotation.y = Mathf.Clamp(rotation.y, -yRotationLimit, yRotationLimit);
        // if (Input.GetMouseButton(0))
        // {
        //     var xQuat = Quaternion.AngleAxis(rotation.x, Vector3.up);
        //     var yQuat = Quaternion.AngleAxis(rotation.y, Vector3.left);

        //     transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);

        // }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }
}
