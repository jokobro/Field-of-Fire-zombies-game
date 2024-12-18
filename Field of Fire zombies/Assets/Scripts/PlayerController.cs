using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private InputActionAsset _playerControls;


    private PlayerInput _playerInput;
    private InputAction _moveAction;
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
       
    }

    private void Update()
    {
        Movement();
    }

    

    public void Movement()
    {
        
    }

    public void Jump()
    {
       
    }
}
