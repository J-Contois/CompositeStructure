using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Rigidbody rigidBody = null;
    [SerializeField] private Vector2 minMaxYaw = new(-90f, 90f);

    [SerializeField] private LayerMask interactionMask = LayerMask.NameToLayer("Default");
    [SerializeField] private Transform root = null;
    [SerializeField] private Transform head = null;

    private Vector3 input = Vector3.zero;
    private Vector2 lookInput;
    private Vector2 currentRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Reset()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        input = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            input.z += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            input.z -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            input.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            input.x += 1;
        }
    }

    private void FixedUpdate()
    {
        rigidBody.linearVelocity = root.rotation * (speed * input.normalized);
    }

    private void LateUpdate()
    {
        currentRotation.x += -lookInput.y * rotationSpeed * Time.deltaTime;
        currentRotation.y += lookInput.x * rotationSpeed * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x, minMaxYaw.x, minMaxYaw.y);

        root.localRotation = Quaternion.Euler(0f, currentRotation.y, 0f);
        head.localRotation = Quaternion.Euler(currentRotation.x, 0f, 0f);
    }

    public void Player_OnMove(CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        input.z = input.y;
        input.y = 0;
    }

    public void Player_OnLook(CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        lookInput = context.ReadValue<Vector2>();
    }

    public void Player_OnInteract(CallbackContext context)
    {
        if (context.performed)
        {
            Ray ray = new Ray(head.position, head.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10f, interactionMask))
            {
                
                Debug.DrawRay(head.position, transform.TransformDirection(head.forward) * hit.distance, Color.red);
                Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.TryGetComponent(out InteractionToggleSetter interactionToggleSetter))
                {
                    interactionToggleSetter.Interact();
                }
            }
        }
    }
}