using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    public GameObject itemHolder;
    public Vector3 aimDirection;
    public Vector3 targetPosition;

    private Vector3 _moveDirection;
    private Vector2 _aimInput;
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private Camera _mainCamera;

    [SerializeField] private float _buildRange = 1.0f;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private float _controllerDeadzone = 0.1f;
    [SerializeField] private float _gamepadRotateSmoothing = 1000f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _moveDirection * _speed * Time.deltaTime);
        HandleRotation();
    }

    void HandleRotation()
    {
        string currentControlScheme = _playerInput.currentControlScheme;

        if (currentControlScheme == "Gamepad" || currentControlScheme == "Mobile")
        {
            targetPosition = transform.position + new Vector3(_aimInput.x, 0, _aimInput.y) * _buildRange;

            if (Mathf.Abs(_aimInput.x) > _controllerDeadzone || Mathf.Abs(_aimInput.y) > _controllerDeadzone)
            {
                aimDirection = Vector3.right * _aimInput.x + Vector3.forward * _aimInput.y;

                if (aimDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(aimDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            Ray cameraRay = _mainCamera.ScreenPointToRay(_aimInput);
            Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

            if (groundPlane.Raycast(cameraRay, out float rayLength))
            {
                aimDirection = cameraRay.GetPoint(rayLength);
                targetPosition = new Vector3(aimDirection.x, transform.position.y, aimDirection.z);
                targetPosition = Vector3.ClampMagnitude(targetPosition - transform.position, _buildRange) + transform.position;

                transform.LookAt(targetPosition);
                itemHolder.transform.LookAt(new Vector3(aimDirection.x, itemHolder.transform.position.y, aimDirection.z));
            }
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _aimInput = context.ReadValue<Vector2>();
        /*Debug.Log("Aim: " + context.ReadValue<Vector2>());*/
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        Debug.Log(_moveDirection);
    }
}