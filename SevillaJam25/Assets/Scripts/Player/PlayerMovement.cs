using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller_;
    public Transform boatSpot;
    public Transform seaSpot;

    [SerializeField] private float boatSpeed_= 4.0f;
    [SerializeField] private float gravity_ = -50f; // Aumentar gravedad para caída rápida
    private float verticalVelocity = 0f; // Control de velocidad en el eje Y
    private Inventory inventory;
    [SerializeField] public GameObject boat;
    private float rotationX = 0f;
    private float rotationY = 0f;
    public float Sensitivity
    {
        get { return sensitivity; }
        set { sensitivity = value; }
    }
    private float startingPosY;
    [Range(0f, 10f)][SerializeField] float sensitivity = 0.15f;
    [Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]

    void Start()
    {
        startingPosY = transform.position.y;
        inventory = GetComponent<Inventory>();
        controller_ = gameObject.AddComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (PlayerTrigger.playerPosition.Equals(PlayerPosition.BOAT))
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

            rotationX -= mouseY;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f); // Limita la rotación en el eje X

            rotationY += mouseX;

            transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

            // CALCULO DE POSICION Y -- SOBRE EL BARCO
            Vector3 position = transform.position;
            position.y = startingPosY + boat.transform.localPosition.y; // Mantiene la Y en relación al barco

            // LEEMOS ENTRADA WASD
            float xMov = Input.GetAxisRaw("Horizontal");
            float zMov = Input.GetAxisRaw("Vertical");

            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            forward.Normalize();
            right.Normalize();

            Vector3 move = (right * xMov + forward * zMov).normalized * boatSpeed_ * Time.deltaTime;

            // APLICAMOS MOVIMIENTO EN X/Z Y MANTENEMOS Y
            position += new Vector3(move.x, 0, move.z); // Solo afecta X y Z
            transform.position = position;
        }

        else if (PlayerTrigger.playerPosition.Equals(PlayerPosition.SEA))
        {

        }

        // UI OPENED

        // Vector3 forward = transform.forward;
        // Vector3 right = transform.right;

        // forward.Normalize();
        // right.Normalize();

        // Vector3 move = (right * xMov + forward * yMov).normalized * boatSpeed_;
        // controller_.Move(move * Time.deltaTime);
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerTrigger.ladderTriggered)
            {
                if (PlayerTrigger.playerPosition == PlayerPosition.SEA)
                {

                    transform.SetPositionAndRotation(boatSpot.position, transform.rotation);
                }
                else
                {
                    transform.SetPositionAndRotation(seaSpot.position, transform.rotation);
                }
            }

            if (PlayerTrigger.objectTriggered)
            {
                if (this.inventory.selectObject(PlayerTrigger.objectTriggered))
                {
                    PlayerTrigger.objectTriggered = null;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            this.inventory.dropItem();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            this.inventory.changeInventoryIndex(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            this.inventory.changeInventoryIndex(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            this.inventory.useObject();
        }
    }
}
