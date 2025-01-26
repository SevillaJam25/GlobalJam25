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


        // UI OPENED
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

    }

}
