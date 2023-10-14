using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab, player;
    public Transform firePoint;
    private float bulletSpeed = 10f;
    public float timeBetweenShots = 0.2f;
    private float bulletLifetime = 2f;

    private float nextShotTime = 0f;

    public bool JugadorEnElCampoDeVisión;

    void Update()
    {
        if (JugadorEnElCampoDeVisión)
        {
            if (Time.time >= nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + timeBetweenShots;
            }

            Vector3 vectormouse = -player.transform.position + transform.position;

            float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
            print(angle);

            transform.rotation = Quaternion.Euler(0, 0, angle);

        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;

        // Destruye la bala después de 2 segundos.
        Destroy(bullet, bulletLifetime);
    }
}
