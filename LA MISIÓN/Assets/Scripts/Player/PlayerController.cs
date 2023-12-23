using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public RectTransform Puntero;

    public Transform firePoint;

    public Armas armaEquipada;
    public GameObject gameObjectArmaEquipada;

    public float timer;

    public InventoryManager inventoryManager;

    public AudioSource[] Audio;
    public PlayerMovement playerMovement;
    private float dispersónPorMovimiento;
    void FixedUpdate() // Xbutton run, RigthShoulder fire, circleButton recargar, LeftShoulder Apuntar, squareButton CojerItems, Cruz Armas
    {
        if (Gamepad.all.Count > 0)
        {
            if (Gamepad.all[0].crossButton.value > 0)
            {
                dispersónPorMovimiento = (playerMovement.currentSpeed / 15);
            }
            else dispersónPorMovimiento = playerMovement.currentSpeed / 11;

            if (Gamepad.all[0].circleButton.value > 0 && armaEquipada != null)
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
                        if (Gamepad.all[0].rightTrigger.value > 0 && timer >= armaEquipada.CadenciaDeTiro)
                        {
                            Shoot();
                        }
                        else if (timer <= armaEquipada.CadenciaDeTiro)
                        {
                            timer += Time.deltaTime;
                        }
                    }
                    else if (Gamepad.all[0].rightShoulder.value > 0 && timer >= armaEquipada.CadenciaDeTiro)
                    {
                        Shoot();
                    }
                    else if (timer <= armaEquipada.CadenciaDeTiro + 1)
                    {
                        timer += Time.deltaTime;
                    }
                }
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse1))
            {
                dispersónPorMovimiento = (playerMovement.currentSpeed / 15);
            }
            else dispersónPorMovimiento = playerMovement.currentSpeed / 11;

            if (Input.GetKeyDown(KeyCode.R) && armaEquipada != null)
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
                        if (Input.GetKey(KeyCode.Mouse0) && timer >= armaEquipada.CadenciaDeTiro)
                        {
                            Shoot();
                        }
                        else if (timer <= armaEquipada.CadenciaDeTiro)
                        {
                            timer += Time.deltaTime;
                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Mouse0) && timer >= armaEquipada.CadenciaDeTiro)
                    {
                        Shoot();
                    }
                    else if (Input.GetKeyUp(KeyCode.Mouse0) && timer >= armaEquipada.CadenciaDeTiro)
                    {
                        Shoot();
                    }
                    else if (timer <= armaEquipada.CadenciaDeTiro + 1)
                    {
                        timer += Time.deltaTime;
                    }
                }
            }

        }

        Vector3 vectormouse = Camera.main.ScreenToWorldPoint(Puntero.position) - transform.position;

        float angle = Mathf.Atan2(vectormouse.y, vectormouse.x) * Mathf.Rad2Deg;
        //print(angle);

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public bool clampSound;

    void Shoot()
    {
        if (gameObjectArmaEquipada.GetComponent<ArmasInventory>().MunicionEnElCartucho > 0)
        {
            gameObjectArmaEquipada.GetComponent<ArmasInventory>().MunicionEnElCartucho--;
            timer = 0;

            if (clampSound == true)
            {
                Audio[1].Stop();
                Audio[0].Play();
                clampSound = false;
            }
            else 
            {
                Audio[0].Stop();
                Audio[1].Play();
                clampSound = true;
            }

            for (int i = 0; i < armaEquipada.NumeroDeBalasPorDisparo; i++)
            {
                float dispersion = Random.Range(-(armaEquipada.Dispersión + dispersónPorMovimiento) * 2, (armaEquipada.Dispersión + dispersónPorMovimiento) * 2);

                GameObject bullet = Instantiate(armaEquipada.TipoDeMunicíon, firePoint.position, Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + dispersion));
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.velocity = bullet.transform.right * armaEquipada.VelocidadDeLasBalas;

                // Destruye la bala después de 1 segundos.
                Destroy(bullet, armaEquipada.AlcanceSegundos); // 1f representa 1 segundos.
            }
        }
        else
        {
            print("recargar");
        }


    }
}