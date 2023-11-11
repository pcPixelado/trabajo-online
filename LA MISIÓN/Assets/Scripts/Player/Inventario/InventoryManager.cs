using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[,] slots;
    public bool[,] slotsOcupados;

    public GameObject[] slotsDetected;

    public int slotsX, slotsY;

    private bool inventarioAbierto = false;

    public Armas[] armasEquipadas;

    public PlayerController playerController;

    private void Awake()
    {
        slots = new GameObject[slotsX, slotsY];
        slotsDetected = GameObject.FindGameObjectsWithTag("Slot");

        slotsOcupados = new bool[slotsX, slotsY];

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
            transform.GetChild(1).gameObject.SetActive(false);
            inventarioAbierto = false;
        }
        else if (!inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(1).gameObject.SetActive(true);
            inventarioAbierto = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerController.armaEquipada = armasEquipadas[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerController.armaEquipada = armasEquipadas[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerController.armaEquipada = armasEquipadas[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            playerController.armaEquipada = armasEquipadas[3];
        }
    }

    public void BtnPressed(int Btn)
    {
        print(Btn);
    }

    public GameObject item;

    public Transform ObjetosEnElInventario;

    public void NewItemOnInventory(ItemInfo info)
    {
        int ancho = info.SlotsX;
        int alto = info.SlotsY;
        


        PosicionLibre posicionLibre = EncontrarPosicionLibre(slotsOcupados, ancho, alto);

        // Muestra el resultado
        if (posicionLibre != null)
        {
           // Instantiate(item, slots[ancho-posicionLibre.Fila, alto-posicionLibre.Columna].transform.position, Quaternion.identity, ObjetosEnElInventario);------------------------------------------
        }
        else
        {
            Console.WriteLine("No se encontró espacio libre para el objeto.");
        }

    }
    // Función para encontrar la primera posición libre para un objeto en la matriz
    static PosicionLibre EncontrarPosicionLibre(bool[,] matriz, int anchoObjeto, int altoObjeto)
    {
        int filas = matriz.GetLength(0);
        int columnas = matriz.GetLength(1);

        for (int fila = 0; fila <= filas - altoObjeto; fila++)
        {
            for (int columna = 0; columna <= columnas - anchoObjeto; columna++)
            {
                // Verifica si hay suficiente espacio libre para el objeto en esta posición
                bool espacioLibre = true;
                for (int i = fila; i < fila + altoObjeto; i++)
                {
                    for (int j = columna; j < columna + anchoObjeto; j++)
                    {
                        if (matriz[i, j] == true)
                        {
                            espacioLibre = false;
                            break;
                        }
                    }
                    if (!espacioLibre) break;
                }

                // Si encontramos espacio libre, devuelve la posición
                if (espacioLibre)
                {
                    return new PosicionLibre(fila, columna);
                }
            }
        }

        // Si no se encontró espacio libre
        return null;
    }
}

// Clase para representar la posición libre en la matriz
class PosicionLibre
{
    public int Fila { get; }
    public int Columna { get; }

    public PosicionLibre(int fila, int columna)
    {
        Fila = fila;
        Columna = columna;
    }
}
