using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;
    private float timeSinceLastShot;

    private void Start()
    {
        PlayerController.shootInput += HandleShooting;
        PlayerController.ReloadInput += HandleReload;
    }

    private void HandleReload()
    {
        if (!gunData.reloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60);

    private void HandleShooting()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                    Debug.Log("Is shooting");
                }
            }
        }

        gunData.currentAmmo--;
        timeSinceLastShot = 0f;
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawLine(muzzle.position, muzzle.forward);
    }
}
