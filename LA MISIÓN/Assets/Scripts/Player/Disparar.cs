using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform firePoint;

    public Armas armaEquipada;

    public Armas[] armasExistentes;

    public float timer;
    void Update()
    {
        if (armaEquipada.Automatica)
        {
            if (Input.GetKey(KeyCode.Mouse0) && timer > armaEquipada.CadenciaDeTiro)
            {
                Shoot();
            }
            else if (timer <= armaEquipada.CadenciaDeTiro)
            {
                timer = timer + Time.deltaTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && timer > armaEquipada.CadenciaDeTiro)
        {
            Shoot();
        }
        else if (timer <= armaEquipada.CadenciaDeTiro)
        {
            timer = timer + Time.deltaTime;
        }


        Vector3 vectormouse = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

        float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
        //print(angle);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        timer = 0;
        for (int i = 0; i < armaEquipada.NumeroDeBalasPorDisparo; i++)
        {
            GameObject bullet = Instantiate(armaEquipada.TipoDeMunicíon, firePoint.position, Quaternion.Euler(0,0,transform.rotation.z + Random.Range(-armaEquipada.Dispersión * 2, armaEquipada.Dispersión * 2)));
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = firePoint.up * armaEquipada.VelocidadDeLasBalas;

            // Destruye la bala después de 1 segundos.
            Destroy(bullet, armaEquipada.AlcanceSegundos); // 1f representa 1 segundos.
        }

    }
}
