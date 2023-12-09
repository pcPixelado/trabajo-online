using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float vida, vidaRestante, walkSpeed = 11f;
    public float runSpeed = 25f; // Velocidad de carrera
    public Rigidbody2D rig;
    public bool isRunning = false; // Variable para rastrear si el jugador está corriendo
    public Image BarraDeVida;

    public float currentSpeed;
    private void Start()
    {
        vidaRestante = vida;
        rig = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        if (Gamepad.all.Count > 0)
        {
            if (Gamepad.all[0].crossButton.value > 0)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            if (Gamepad.all[0].leftShoulder.value > 0f)
            {
                rig.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed / 2, Input.GetAxis("Vertical") * currentSpeed / 2);
            }
            else rig.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, Input.GetAxis("Vertical") * currentSpeed);
        }
        else
        {
            // Si se presiona Shift, establecer isRunning en verdadero
            if (Input.GetKey(KeyCode.LeftShift))
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }

            // Mover al personaje
            if (!Input.GetKey(KeyCode.Mouse1))
            {
                rig.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, Input.GetAxis("Vertical") * currentSpeed);
            }
            else rig.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed / 2, Input.GetAxis("Vertical") * currentSpeed / 2);
        }

        // Calcular la velocidad en función de si el jugador está corriendo o caminando
        currentSpeed = isRunning ? runSpeed : walkSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vidaRestante -= collision.transform.localScale.z;

            BarraDeVida.fillAmount = vidaRestante/vida;

            Destroy(collision.gameObject);
        }
    }
}