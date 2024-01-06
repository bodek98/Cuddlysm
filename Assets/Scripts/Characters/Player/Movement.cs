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
    [SerializeField] private GameObject _weaponHolder;
    [SerializeField] private float _gunHeight = 1.5f;

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
        Plane groundPlane = new Plane(Vector3.up, new Vector3(0,_gunHeight, 0));

        if (groundPlane.Raycast(cameraRay, out float rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
            _weaponHolder.transform.LookAt(new Vector3(pointToLook.x, _weaponHolder.transform.position.y, pointToLook.z));
        }
    }

    void OnMovement(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        _direction = new Vector3(moveInput.x, 0, moveInput.y);
    }
}