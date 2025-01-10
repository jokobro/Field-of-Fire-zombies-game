using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Header("References")]
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cameraHolder;

    [Header("Movement")]
    [SerializeField] private float speed;

    [Header("Drag")]
    [SerializeField] private float drag;

    [Header("Look Settings")]
    [SerializeField] private float sensX;
    [SerializeField] private float sensY;
    private float yRotation;
    private float xRotation;
    
   
    private Vector3 moveDirection;
    private float horizontalInput;
    private float verticalInput;
    

    private GameManager gameManager;

    //variable voor pickups
    private bool isDoublePointsActive;

    public static Action shootInput;
    public static Action ReloadInput;
    [SerializeField] private KeyCode reloadKey;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start() => rb.freezeRotation = true;

    private void HandleLooking()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * 0.1f;
        float mouseY = Input.GetAxisRaw("Mouse Y") * 0.1f;

        yRotation += mouseX * sensX;
        xRotation -= mouseY * sensY;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        orientation.rotation = Quaternion.Euler(0f, yRotation, 0f);
        cameraHolder.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }


    private void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;

        rb.drag = drag;

        HandleLooking();

        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }

        if (Input.GetKeyDown(reloadKey))
        {
            ReloadInput?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDirection * speed, ForceMode.Acceleration);
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
