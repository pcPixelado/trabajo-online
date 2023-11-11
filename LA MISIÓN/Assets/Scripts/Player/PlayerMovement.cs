using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 11f;
    public float runSpeed = 25f; // Velocidad de carrera
    public Rigidbody2D rig;
    private bool isRunning = false; // Variable para rastrear si el jugador está corriendo

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
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

        // Calcular la velocidad en función de si el jugador está corriendo o caminando
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        // Mover al personaje

        if (!Input.GetKey(KeyCode.Mouse1))
        {
            rig.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed, Input.GetAxis("Vertical") * currentSpeed);
        }
        else rig.velocity = new Vector2(Input.GetAxis("Horizontal") * currentSpeed / 2, Input.GetAxis("Vertical") * currentSpeed / 2);
    }
}