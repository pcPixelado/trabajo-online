using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puntero : MonoBehaviour
{
    public bool MandoConectado;

    public InventoryManager InventoryManager;

    private readonly float VelocidadDelCursor = 2;

    private void Awake()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }
    }
    private Vector2 posicionObjetivo;
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            if (!InventoryManager.inventarioAbierto && Gamepad.all[0].rightStick.magnitude == 0)
            {
                posicionObjetivo = new(Camera.main.scaledPixelWidth / 2, Camera.main.scaledPixelHeight / 2);
            }
            else posicionObjetivo += Gamepad.all[0].rightStick.value * VelocidadDelCursor;

            transform.position = posicionObjetivo;

            MandoConectado = true;
        }
        else
        {
            transform.position = Input.mousePosition; // Si no hay mando conectado
            MandoConectado = false;
        }
    }
}