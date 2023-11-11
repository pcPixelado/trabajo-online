using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;

    private float Alejamiento = 3;

    private Vector3 posicionDeLacamara;
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Alejamiento = player.GetComponent<PlayerController>().armaEquipada.mirillaDeApuntado;

            float alejamientoReal = (Alejamiento + 2) / 3;
            posicionDeLacamara = (player.transform.position + (alejamientoReal - 1) * Camera.main.ScreenToWorldPoint(Input.mousePosition)) / alejamientoReal;
        }
        else posicionDeLacamara = player.transform.position;

        transform.position = posicionDeLacamara + new Vector3(0,0,-10);
    }
}
