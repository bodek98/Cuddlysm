using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    private Vector3 _direction;
    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private Camera _mainCamera;

    [SerializeField] private float speed = 5.0f;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Movement");
        _mainCamera = FindObjectOfType<Camera>();
    }

    void FixedUpdate()
    {
        _rb.MovePosition(transform.position + _direction * speed * Time.deltaTime);

        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.yellow);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    void OnMovement(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        _direction = new Vector3(moveInput.x, 0, moveInput.y);
    }
}