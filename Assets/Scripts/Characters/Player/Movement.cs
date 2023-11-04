using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Movement : MonoBehaviour
{
    private Vector3 direction;
    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;

    [SerializeField] private float speed = 5.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Movement");
    }

    private void Update()
    {
        Vector2 moveInput = moveAction.ReadValue<Vector2>();
        direction = new Vector3(moveInput.x, 0, moveInput.y);
    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
    }
}