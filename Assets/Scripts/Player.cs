using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private Rigidbody _rigidBody = null;
    [SerializeField] private Transform _root = null;
    [SerializeField] private Transform _head = null;
    [SerializeField] private Vector2 _minMaxYaw = new(-90f, 90f);

    private Vector3 _input = Vector3.zero;
    private Vector2 lookInput;
    private Vector2 currentRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Reset()
    {
        _rigidBody = GetComponent<Rigidbody>();
        Debug.LogError("je reset");
    }

    private void OnValidate()
    {
        Debug.LogError("je valide");
    }

    private void Update()
    {
        _input = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _input.z += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            _input.z -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            _input.x -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            _input.x += 1;
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.linearVelocity = _root.rotation * (_speed * _input.normalized);
    }

    private void LateUpdate()
    {
        currentRotation.x += -lookInput.y * rotationSpeed * Time.deltaTime;
        currentRotation.y += lookInput.x * rotationSpeed * Time.deltaTime;
        currentRotation.x = Mathf.Clamp(currentRotation.x, _minMaxYaw.x, _minMaxYaw.y);

        _root.localRotation = Quaternion.Euler(0f, currentRotation.y, 0f);
        _head.localRotation = Quaternion.Euler(currentRotation.x, 0f, 0f);
    }

    public void Player_OnMove(CallbackContext context)
    {
        _input = context.ReadValue<Vector2>();
        _input.z = _input.y;
        _input.y = 0;
    }

    public void Player_OnLook(CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
        lookInput = context.ReadValue<Vector2>();
    }
}
