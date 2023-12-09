using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public RectTransform Puntero;

    public Transform firePoint;

    public Armas armaEquipada;
    public GameObject gameObjectArmaEquipada;

    public float timer;

    public InventoryManager inventoryManager;

    public PlayerMovement playerMovement;
    private float dispers�nPorMovimiento;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            dispers�nPorMovimiento = (playerMovement.currentSpeed / 11) - 4;
        }
        else dispers�nPorMovimiento = playerMovement.currentSpeed / 11;



        if (Input.GetKeyDown(KeyCode.R))
        {
            timer = -armaEquipada.TiempoDeRecarga;
        }
        if (gameObjectArmaEquipada != null)
        {
            armaEquipada = gameObjectArmaEquipada.GetComponent<ArmasInventory>().info.infoArma;

            if (!inventoryManager.inventarioAbierto)
            {
                if (armaEquipada.Automatica)
                {
                    if (Input.GetKey(KeyCode.Mouse0) && timer > armaEquipada.CadenciaDeTiro)
                    {
                        Shoot();
                    }
                    else if (timer <= armaEquipada.CadenciaDeTiro)
                    {
                        timer += Time.deltaTime;
                    }
                }
                else if (Input.GetKeyDown(KeyCode.Mouse0) && timer > armaEquipada.CadenciaDeTiro)
                {
                    Shoot();
                }
                else if (timer <= armaEquipada.CadenciaDeTiro)
                {
                    timer += Time.deltaTime;
                }
            }
        }
        Vector3 vectormouse = Camera.main.ScreenToWorldPoint(Puntero.position) - transform.position;

        float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
        //print(angle);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Shoot()
    {
        if (gameObjectArmaEquipada.GetComponent<ArmasInventory>().MunicionEnElCartucho > 0)
        {
            gameObjectArmaEquipada.GetComponent<ArmasInventory>().MunicionEnElCartucho--;
            timer = 0;

            for (int i = 0; i < armaEquipada.NumeroDeBalasPorDisparo; i++)
            {
                GameObject bullet = Instantiate(armaEquipada.TipoDeMunic�on, firePoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + Random.Range(-(armaEquipada.Dispersi�n + dispers�nPorMovimiento) * 2, (armaEquipada.Dispersi�n + dispers�nPorMovimiento) * 2)));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bullet.transform.right * armaEquipada.VelocidadDeLasBalas;

                // Destruye la bala despu�s de 1 segundos.
                Destroy(bullet, armaEquipada.AlcanceSegundos); // 1f representa 1 segundos.
            }
        }
        else
        {
            print("recargar");
        }


    }
}