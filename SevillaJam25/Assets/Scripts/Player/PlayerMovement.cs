using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller_;
    public Transform boatSpot;
    public Transform seaSpot;

    [SerializeField] private float boatSpeed_ = 2.95f;
    [SerializeField] private float gravity_ = 8.5f; // Aumentar gravedad para caída rápida
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
    public float rotationSmoothness = 10.2f;  // Cuanto más alto, más rápida la rotación
    public float movementSpeed = 3.95f;        // Velocidad de movimiento

    public float seaSpeed = 32f;         // Velocidad base en el agua
    public float sinkSpeed = 4f;        // Velocidad de caída en el agua
    public float riseSpeed = 1f;        // Velocidad al subir en el agua (más lento que bajar)
    public float movementSmoothness = 10f;

    [Range(0f, 20f)][SerializeField] float sensitivity = 2f;
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
        // Capturar entrada del mouse sin multiplicar por deltaTime
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Aplicar límites a la rotación en X (mirada vertical)
        rotationX = Mathf.Clamp(rotationX - mouseY, -90f, 90f);
        rotationY += mouseX;

        // Suavizar la rotación con Slerp, aumentando la velocidad
        Quaternion targetRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSmoothness);


        // Capturar entrada de movimiento (W/S y A/D)
        float moveVertical = Input.GetAxisRaw("Vertical"); // W/S
        float moveHorizontal = Input.GetAxisRaw("Horizontal"); // A/D

        if (PlayerTrigger.playerPosition.Equals(PlayerPosition.BOAT))
        {
            RenderSettings.fog = false;

            // Calcular dirección de movimiento normalizada
            Vector3 moveDirection = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized;
            Vector3 targetPosition = transform.position + moveDirection * boatSpeed_ * Time.deltaTime;
            targetPosition.y=startingPosY;

            // Aplicar movimiento con MoveTowards para mayor responsividad
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * movementSpeed);
        }


        else if (PlayerTrigger.playerPosition.Equals(PlayerPosition.SEA))
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = new Color(0.1f, 0.4f, 0.6f, 1f); // Azul verdoso para simular el agua
            RenderSettings.fogDensity = 0.002f; // Ajusta para mayor o menor visibilidad

            // Dirección de movimiento basada en la orientación de la cámara
            Vector3 moveDirection = transform.TransformDirection(new Vector3(moveHorizontal, 0, moveVertical)).normalized;

            // Factor de ascenso más rápido
            float ascendBonus = 1f;
            if (moveVertical > 0 && Vector3.Dot(transform.forward, Vector3.up) > 0.3f)
            {
                ascendBonus = 1.5f; // Aumentar velocidad al ascender
            }

            // Velocidad del movimiento
            Vector3 moveVelocity = moveDirection * seaSpeed * ascendBonus * Time.deltaTime;

            // Aplicar fuerza de gravedad marina
            float depthFactor = Mathf.Clamp(Vector3.Dot(transform.forward, Vector3.down), -1f, 1f);
            float gravityEffect = Mathf.Lerp(sinkSpeed, riseSpeed, (depthFactor + 1) / 2);

            Vector3 gravityForce = Vector3.down * gravityEffect * Time.deltaTime;

            // Aplicar movimiento directo sin Lerp
            transform.position += moveVelocity + gravityForce;
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
                    PlayerTrigger.resetTriggerObject();
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
