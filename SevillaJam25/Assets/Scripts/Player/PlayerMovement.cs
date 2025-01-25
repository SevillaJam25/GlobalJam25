using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CameraController cameraController;
    private CharacterController controller_; //Moves game object in given direction
    private Vector3 playerVelocity_;
    private bool isGrounded_;
    public Transform boatSpot;
    [SerializeField] private float speed_ = 2.0f;
    [SerializeField] private float gravity_ = -9.81f;
    private Inventory inventory;

    void Start()
    {
        cameraController = GetComponent<CameraController>();
        inventory = GetComponent<Inventory>();
        controller_ = gameObject.AddComponent<CharacterController>();
    }
    void Update()
    {
        // Usamos GetAxisRaw para obtener una respuesta instantánea
        float xMov = Input.GetAxisRaw("Horizontal");
        float yMov = Input.GetAxisRaw("Vertical");

        // Obtener la dirección de la cámara
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        // Asegurarse de que el movimiento sea horizontal y no afecte al eje Y
        //forward.y = 0f;
        //right.y = 0f;

        // Normalizar las direcciones para evitar movimientos más rápidos en diagonales
        forward.Normalize();
        right.Normalize();

        // Calcular el movimiento en base a la orientación de la cámara
        Vector3 move = (right * xMov + forward * yMov).normalized;

        // Mover al personaje
        controller_.Move(move * Time.deltaTime * speed_);
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (PlayerTrigger.ladderTriggered)
            {
                transform.SetPositionAndRotation(boatSpot.position, transform.rotation);
            }

            if (PlayerTrigger.objectTriggered)
            {
                this.inventory.selectObject(PlayerTrigger.objectTriggered);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            this.inventory.dropItem();
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            this.inventory.changeInventoryIndex(true);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            this.inventory.changeInventoryIndex(false);
        }

    }

}
