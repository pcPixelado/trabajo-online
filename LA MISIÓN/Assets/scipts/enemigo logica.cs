using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 0.1f;
    public float bulletLifetime = 2f;

    private float nextShotTime = 0f;

    void Update()
    {
        if (Time.time >= nextShotTime)
        {
            Shoot();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;

        // Destruye la bala después de 2 segundos.
        Destroy(bullet, bulletLifetime);
    }
}
