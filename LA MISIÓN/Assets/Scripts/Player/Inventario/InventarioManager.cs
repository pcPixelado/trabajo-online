using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventarioManager : MonoBehaviour
{
    public GameObject[,] slots;

    public GameObject[] slotsDetected;

    public int slotsX, slotsY;

    private bool inventarioAbierto = false;
    private void Awake()
    {
        slots = new GameObject[slotsX, slotsY];
        slotsDetected = GameObject.FindGameObjectsWithTag("Slot");

        for (int i = 0; i < slotsY; i++)
        {
            for (int j = 0; j < slotsX; j++)
            {
                slots[j, i] = slotsDetected[(i * slotsX + j)];
                slots[j, i].GetComponent<Slot>().SlotQueSoy = 53 - (i * slotsX + j);
            }
        }
    }
    void Update()
    {
        if (inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(0).GetComponent<Image>().enabled = false;
            transform.GetChild(1).GetComponent<Image>().enabled = false;
            inventarioAbierto = false;
        }
        else if (!inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(1).GetComponent<Image>().enabled = true;
            inventarioAbierto = true;
        }
    }

    public void BtnPressed(int Btn)
    {
        print(Btn);
    }
}
