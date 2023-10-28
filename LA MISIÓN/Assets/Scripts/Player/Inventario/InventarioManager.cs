using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    public GameObject[,] slots;

    public GameObject[] slotsDetected;

    public int slotsX, slotsY;

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
        
    }

    public void BtnPressed(int Btn)
    {
        print(Btn);
    }
}
