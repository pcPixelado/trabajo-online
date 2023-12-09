using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Puntero : MonoBehaviour
{
    public bool MandoConectado;

    public InventoryManager InventoryManager;

    private readonly float VelocidadDelCursor = 1;

    private void Awake()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }
    }
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {



            if (!InventoryManager.inventarioAbierto && Gamepad.all[0].rightStick.magnitude == 0)
            {
                transform.position = new(Camera.main.scaledPixelWidth, Camera.main.scaledPixelHeight);
            }
        }
        else
        {
            transform.position = Input.mousePosition; // Si no hay mando conectado
            MandoConectado = true;
        }
    }
}