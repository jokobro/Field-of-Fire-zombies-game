using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Magazine Settings")]
    [SerializeField] private int Maxammo = 65;
    [SerializeField] private int currentClip = 10;
    [SerializeField] private int maxclipSize = 20;
    [SerializeField] private int currentammo = 10;
    [SerializeField] private int refillAmmo = 35;
    [Header("FireRate Settings")]
    [SerializeField] private float fireRate = 0.6f;
    private float nextFire;


    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void HandleShooting(InputAction.CallbackContext context)
    {
        if(context.performed && currentClip > 0)
        {
            nextFire = Time.time + fireRate;

            currentClip--;
        }
    }
}
