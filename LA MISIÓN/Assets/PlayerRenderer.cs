using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public PlayerController playerController;

    public SpriteRenderer Piernas, Cuerpo;
    public Transform PiernasTransform;

    private Rigidbody2D rb;

    public Sprite[] SpritesPiernas, SpritesCuerpo;
    private void Awake()
    {
        rb = playerMovement.GetComponent<Rigidbody2D>();
    }
    private float timer;//correr
    private int Cambio;

    private float timerShot;

    public bool shot;
    void Update()
    {
        if (!Input.GetKey(KeyCode.Mouse0))
        {
            shot = false;
        }
        

        if (playerController.armaEquipada != null)
        {
            if (shot)
            {
                if (timerShot > playerController.armaEquipada.CadenciaDeTiro / 2 && timerShot < playerController.armaEquipada.CadenciaDeTiro)
                {
                    timerShot += Time.deltaTime;
                    Cuerpo.sprite = SpritesCuerpo[playerController.gameObjectArmaEquipada.GetComponent<ArmasInventory>().info.CalibreDelArma * 2];
                }
                else if (timerShot > playerController.armaEquipada.CadenciaDeTiro)
                {
                    timerShot = 0;
                    Cuerpo.sprite = SpritesCuerpo[(playerController.gameObjectArmaEquipada.GetComponent<ArmasInventory>().info.CalibreDelArma * 2) -1];
                }
                else
                {
                    timerShot += Time.deltaTime;
                }
            }
            else Cuerpo.sprite = SpritesCuerpo[playerController.gameObjectArmaEquipada.GetComponent<ArmasInventory>().info.CalibreDelArma * 2];
        }
        else
        {
            Cuerpo.sprite = SpritesCuerpo[0];
        }


        if (rb.velocity.magnitude == 0)
        {
            Piernas.sprite = SpritesPiernas[0];
            timer = 25f;
        }
        else
        {
            PiernasTransform.rotation = Quaternion.Euler(0,0,Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg + 90);

            if (timer > 1/playerMovement.currentSpeed)
            {
                timer = 0;

                if (Cambio == 0)
                {
                    Cambio = 1;

                    Piernas.sprite = SpritesPiernas[1];
                }
                else if (Cambio == 1)
                {
                    Cambio = 2;

                    Piernas.sprite = SpritesPiernas[0];
                }
                else if (Cambio == 2)
                {
                    Cambio = 3;

                    Piernas.sprite = SpritesPiernas[2];
                }
                else
                {
                    Cambio = 0;

                    Piernas.sprite = SpritesPiernas[0];
                }
            }
            else
            {
                timer += Time.deltaTime / 4.7f;
            }
        }
    }
}
