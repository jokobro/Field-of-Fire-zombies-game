using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;

    [Header("Player Settings")]
    [SerializeField] private float playerHealth = 100f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpPower = 10f;
    [SerializeField] private float gravityMultiplier = 3.0f;

    [SerializeField] private int points = 500;

    [Header("Camera Settings")]
    [SerializeField] private float mouseSensitivity = 2f;
    private Camera mainCamera;

    private float gravity = -9.81f;
    private float verticalVelocity;
    private Vector3 MoveDirection;
    private Vector2 input;
    private float cameraPitch = 0f;

    //variable voor pickups
    private bool isDoublePointsActive;

    private GameManager gameManager;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        gameManager = FindObjectOfType<GameManager>();
        
        // Controleer of de camera is toegewezen
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Probeer de hoofdcamera te vinden
        }

        if (mainCamera == null)
        {
            Debug.LogError("PlayerController: No Camera found! Please assign a camera in the Inspector.");
        }

        // Cursor verbergen en vergrendelen voor FPS
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (mainCamera == null) return; // Geen camera? Stop met updaten om crashes te voorkomen

        HandleGravity();
        HandleMovement();
        HandleRotation();
        UpdateHealth();
    }

    private void HandleGravity()
    {
        if (IsGrounded() && verticalVelocity < 0)
        {
            verticalVelocity = -1f;

        }
        else
        {
            verticalVelocity += gravity * gravityMultiplier * Time.deltaTime;
        }

        MoveDirection.y = verticalVelocity;
    }

    private void HandleMovement()
    {
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        characterController.Move(move * walkSpeed * Time.deltaTime + MoveDirection * Time.deltaTime);
    }

    private void HandleRotation()
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity * Time.deltaTime;

        //rotate the player horizontally
        transform.Rotate(Vector3.up * mouseDelta.x);

        //adjust camera pitch (vertical rotation)
        cameraPitch -= mouseDelta.y;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);

        mainCamera.transform.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
    }

    public void Move(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && IsGrounded())
        {
            verticalVelocity = jumpPower;
        }
    }

    private bool IsGrounded() => characterController.isGrounded;


    private void UpdateHealth()
    {



    }




    public void ActivatePowerup(int id, float duration, GameObject powerup)
    {
        if (id == 0)
        {
            if (!isDoublePointsActive)
            {
                ActivateDoublePoints(duration);
                Destroy(powerup);
            }
        }
        else if (id == 1)
        {
            BonusPoints();
            Destroy(powerup);
            Debug.Log("bonus points opgepakt");
        }
    }

    private void ActivateDoublePoints(float duration)
    {
        Debug.Log("double points active");
        isDoublePointsActive = true;
        gameManager.scoreMultiplier = 2f;
        StartCoroutine(DoublePointsCooldown(duration));
    }

    IEnumerator DoublePointsCooldown(float duration)
    {
        yield return new WaitForSeconds(duration);
        isDoublePointsActive = false;
        gameManager.scoreMultiplier = 1f;
        Debug.Log("double points uitgezet");
    }

    private void BonusPoints()
    {
        gameManager.AddScore(500);
    }
}
