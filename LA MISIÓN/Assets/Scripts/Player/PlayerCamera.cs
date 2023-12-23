using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public RectTransform Puntero;

    public GameObject player;

    private float Alejamiento = 3;

    private Vector3 posicionDeLacamara;

    public InventoryManager inventory;
    void Update()
    {
        if (Gamepad.all.Count > 0)
        {
            if (Gamepad.all[0].leftTrigger.value > 0f && !inventory.inventarioAbierto)
            {
                if (player.GetComponent<PlayerController>().armaEquipada != null)
                {
                    Alejamiento = player.GetComponent<PlayerController>().armaEquipada.mirillaDeApuntado;
                }
                else Alejamiento = 1.5f;

                float alejamientoReal = (Alejamiento + 2) / 3;
                posicionDeLacamara = (player.transform.position + (alejamientoReal - 1) * Camera.main.ScreenToWorldPoint(new(Puntero.position.x, Puntero.position.y * 1.1f))) / alejamientoReal;
            }
            else posicionDeLacamara = player.transform.position;
        }
        else
        {
            if (Input.GetKey(KeyCode.Mouse1) && !inventory.inventarioAbierto)
            {
                if (player.GetComponent<PlayerController>().armaEquipada != null)
                {
                    Alejamiento = player.GetComponent<PlayerController>().armaEquipada.mirillaDeApuntado;
                }
                else Alejamiento = 1.5f;

                float alejamientoReal = (Alejamiento + 2) / 3;
                posicionDeLacamara = (player.transform.position + (alejamientoReal - 1) * Camera.main.ScreenToWorldPoint(Puntero.position)) / alejamientoReal;
            }
            else posicionDeLacamara = player.transform.position;
        }

        Vector3 posicionObjetivo = posicionDeLacamara + new Vector3(0,0,-10);

        if (Input.GetKey(KeyCode.Mouse1) || Gamepad.all.Count > 0)
        {
            transform.position = Vector3.Slerp(transform.position, posicionObjetivo, 0.2f);
        }
        else transform.position = posicionObjetivo;


    }
}
