using System.Collections;
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
    /*[SerializeField] private float fireRate = 0.6f;
    private float nextFire;*/

    [SerializeField] private bool addBulletSpread = true;
    [SerializeField] private Vector3 bulletSpreadVariance = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private ParticleSystem shootingSystem;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private ParticleSystem impactParticleSystem;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private float shootDelay = 0.5f;
    [SerializeField] private LayerMask layerMask;

    private Animator animator;
    private float lastShootTime;


    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fire;
    [SerializeField] private GameObject hitPoint;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        RaycastHit hit;


        if (Physics.Raycast(firePoint.position, transform.TransformDirection(Vector3.forward), out hit, 20))
        {
            Debug.Log("hit");
            Debug.DrawRay(firePoint.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);


            GameObject a = Instantiate(fire, firePoint.position, Quaternion.identity);
            GameObject b = Instantiate(hitPoint, hit.point, Quaternion.identity);


            Destroy(a, 1);
            Destroy(b, 1);

            BaseEnemy enemy = hit.transform.GetComponent<BaseEnemy>();

            if (enemy != null)
            {
                enemy.HandleDamage(20);
            
            }
        }
        else
        {
            Debug.Log("hit nothing");
        }
    }

   /* private void HandleShooting(InputAction.CallbackContext context)
    {
        *//*if (context.performed && currentClip > 0)*//*
        if (lastShootTime + shootDelay < Time.time)
        {
            animator.SetBool("IsShooting", true);
            shootingSystem.Play();
            Vector3 direction = GetDirection();

            if (Physics.Raycast(bulletSpawnPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask))
            {
                TrailRenderer trail = Instantiate(bulletTrail, bulletSpawnPoint.position, Quaternion.identity);


                StartCoroutine(SpawnTrail(trail, hit));

                lastShootTime = Time.time;
            }

        }
        currentClip--;

    }*/

    private Vector3 GetDirection()
    {
        Vector3 direction = transform.forward;

        if (addBulletSpread)
        {
            direction += new Vector3(
               Random.Range(-bulletSpreadVariance.x, bulletSpreadVariance.x),
               Random.Range(-bulletSpreadVariance.y, bulletSpreadVariance.y),
               Random.Range(-bulletSpreadVariance.z, bulletSpreadVariance.z)
            );

            direction.Normalize();
        }
        return direction;
    }


    private IEnumerator SpawnTrail(TrailRenderer trail,RaycastHit hit)
    {
        float time = 0;

        Vector3 startPosition  = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }
        animator.SetBool("IsShooting", false);
        trail.transform.position = hit.point;
        Instantiate(impactParticleSystem, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
    }
}
