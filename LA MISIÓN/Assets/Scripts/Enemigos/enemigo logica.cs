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

    private float nextShotTime = 0f;

    public bool JugadorEnElCampoDeVisi�n;

    public Vector3 posicionesEstrategicas;

    void Update()
    {

        Vector3 vectormouse = player.transform.position - transform.position;

        float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
        if (JugadorEnElCampoDeVisi�n)
        {
            if (Time.time >= nextShotTime)
            {
                Shoot();
                nextShotTime = Time.time + timeBetweenShots;
            }

            //print(angle);

            transform.rotation = Quaternion.Euler(0, 0, angle);









        }

        lanzarRaycast(angle);

    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.up * bulletSpeed;

        // Destruye la bala despu�s de 2 segundos.
        Destroy(bullet, bulletLifetime);
    }

    void lanzarRaycast(float angulo)
    {
        Vector3 direction = new Vector2(Mathf.Cos(angulo * Mathf.Deg2Rad), Mathf.Sin(angulo * Mathf.Deg2Rad));

        RaycastHit2D hit = Physics2D.Raycast(transform.position + direction * 5, direction, RangoDeVision);

        if (hit)
        {
            if (hit.transform.tag == "Player" /*|| hit == player*/)
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.green);
                JugadorEnElCampoDeVisi�n = true;
            }
            else if (hit.transform.tag == "Bala")
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, direction * RangoDeVision, Color.red);
                JugadorEnElCampoDeVisi�n = false;
            }
        }
        else
        {
            Debug.DrawRay(transform.position, direction * RangoDeVision, Color.red);
            JugadorEnElCampoDeVisi�n = false;
        }
    }
}
