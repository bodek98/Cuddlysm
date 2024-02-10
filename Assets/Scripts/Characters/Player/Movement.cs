using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    private Vector3 _moveDirection;
    private Vector2 _aimInput;
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private Camera _mainCamera;

    [SerializeField] private float speed = 5.0f;
    [SerializeField] private GameObject _weaponHolder;
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
        _rb.MovePosition(transform.position + _moveDirection * speed * Time.deltaTime);

        HandleRotation();
    }

    void HandleRotation()
    {
        string currentControlScheme = _playerInput.currentControlScheme;

        if (currentControlScheme == "Gamepad")
        {
            if (Mathf.Abs(_aimInput.x) > _controllerDeadzone || Mathf.Abs(_aimInput.y) > _controllerDeadzone)
            {
                Vector3 aimDirection = Vector3.right * _aimInput.x + Vector3.forward * _aimInput.y;
                Debug.Log(aimDirection);
                if (aimDirection.sqrMagnitude > 0.0f)
                {
                    Quaternion newRotation = Quaternion.LookRotation(aimDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _gamepadRotateSmoothing * Time.deltaTime);
                }
            }
        }
        else
        {
            // Todo: check how to convert screenPoint (left-down corner) to point of player (worldPoint?) and use in ScreenPointToRay.
            Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, new Vector3(0, transform.position.y, 0));

            if (groundPlane.Raycast(cameraRay, out float rayLength))
            {
                Vector3 pointToLook = cameraRay.GetPoint(rayLength);

                transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
                _weaponHolder.transform.LookAt(new Vector3(pointToLook.x, _weaponHolder.transform.position.y, pointToLook.z));
            }
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _aimInput = context.ReadValue<Vector2>();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        _moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
    }
}