using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float vida, vidaRestante, walkSpeed = 11f;
    public float runSpeed = 25f; // Velocidad de carrera
    public Rigidbody2D rb;
    public bool isRunning = false; // Variable para rastrear si el jugador est� corriendo
    public Image BarraDeVida;

    public float currentSpeed;
    private void Start()
    {
        vidaRestante = vida;
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
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


            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) currentSpeed = isRunning ? runSpeed : walkSpeed;
            else currentSpeed = 0;

            if (Gamepad.all[0].leftShoulder.value > 0f)
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed / 2, Input.GetAxis("Vertical") * currentSpeed / 2);
            }
            else rb.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, Input.GetAxis("Vertical") * currentSpeed);
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

            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) currentSpeed = isRunning ? runSpeed : walkSpeed;
            else currentSpeed = 0;

            // Mover al personaje
            if (!Input.GetKey(KeyCode.Mouse1))
            {
                rb.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, Input.GetAxis("Vertical") * currentSpeed);
            }
            else rb.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed / 2, Input.GetAxis("Vertical") * currentSpeed / 2);
        }

        // Calcular la velocidad en funci�n de si el jugador est� corriendo o caminando
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            vidaRestante -= collision.transform.localScale.z * collision.relativeVelocity.magnitude / 290;

            BarraDeVida.fillAmount = vidaRestante/vida;

            Destroy(collision.gameObject);
        }
    }
}