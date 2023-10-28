using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventarioManager : MonoBehaviour
{
    public GameObject[,] slots;

    public GameObject[] slotsDetected;

    public int slotsX, slotsY;

    private void Start()
    {
        slots = new GameObject[slotsX, slotsY];
        slotsDetected = GameObject.FindGameObjectsWithTag("Slot");

        for (int i = 0; i < slotsY; i++)
        {
            for (int j = 0; j < slotsX; j++)
            {
                slots[j, i] = slotsDetected[i * slotsX + j];
            }
        }
    }
    void Update()
    {
        if (slots[slotsX,slotsY] == slotsDetected[slotsDetected.Length])
        {
            print("que locura");
        }
        else
        {
            print(slots[slotsX, slotsY] + " " + slotsDetected[slotsDetected.Length]);
        }
    }
}
