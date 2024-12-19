using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private CharacterController _characterController;
    private float _moveSpeed = 5f;
    private float _jumpForce = 4f;
    [SerializeField] private float _points = 500;
    private bool _isGrounded;
    private Vector2 _lookPosition;
    private Vector3 _moveInput;
    private Vector3 _playerVelocity;


    private void Start()
    {
        _camera = Camera.main;
        _characterController = GetComponent<CharacterController>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Movement");
        _jumpAction = _playerInput.actions.FindAction("Jump");
    }

    private void Update()
    {
        Movement();
    }

    public void Movement()
    {
        Vector2 direction = _moveAction.ReadValue<Vector2>();
        transform.position += new Vector3 (direction.x, 0, direction.y) * _moveSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        Vector3 jump = _jumpAction.ReadValue<Vector3>();
        transform.position += new Vector3(0, jump.y, 0) * _jumpForce * Time.deltaTime;
    }
}
