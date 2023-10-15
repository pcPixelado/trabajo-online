using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab, player;
    public Transform firePoint;
    public float bulletSpeed = 10f;
    public float timeBetweenShots = 0.2f;
    private float bulletLifetime = 2f;
    public float RangoDeVision;

    public LayerMask layer;

    private float nextShotTime = 0f;

    public bool JugadorEnElCampoDeVisión;

    void Update()
    {

        Vector3 vectormouse = player.transform.position - transform.position;

        float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
        if (JugadorEnElCampoDeVisión)
        {
            if (Time.time >= nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + timeBetweenShots;
            }

            print(angle);

            transform.rotation = Quaternion.Euler(0, 0, angle);

        }

        lanzarRaycast(angle);

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed * 10;

        // Destruye la bala después de 2 segundos.
        Destroy(bullet, bulletLifetime);
    }

    void lanzarRaycast(float angulo)
    {
        Vector2 direction = new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, RangoDeVision);

        if (hit)
        {
            if (hit.transform.tag == "Player")
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.green);
                JugadorEnElCampoDeVisión = true;
            }
            else
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.red);
                JugadorEnElCampoDeVisión = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction * RangoDeVision, Color.red);
            JugadorEnElCampoDeVisión = false;
        }
    }
}
