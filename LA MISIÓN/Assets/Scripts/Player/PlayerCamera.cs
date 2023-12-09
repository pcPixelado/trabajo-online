using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public RectTransform Puntero;

    public GameObject player;

    private float Alejamiento = 3;

    private Vector3 posicionDeLacamara;

    public InventoryManager inventory;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1) && !inventory.inventarioAbierto)
        {
            Alejamiento = player.GetComponent<PlayerController>().armaEquipada.mirillaDeApuntado;

            float alejamientoReal = (Alejamiento + 2) / 3;
            posicionDeLacamara = (player.transform.position + (alejamientoReal - 1) * Camera.main.ScreenToWorldPoint(Puntero.position)) / alejamientoReal;
        }
        else posicionDeLacamara = player.transform.position;

        Vector3 posicionObjetivo = posicionDeLacamara + new Vector3(0,0,-10);

        transform.position = posicionObjetivo;
    }
}
