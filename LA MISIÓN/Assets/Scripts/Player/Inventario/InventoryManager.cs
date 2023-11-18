using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject[,] slots;
    public bool[,] slotsOcupados;

    public GameObject[] slotsDetected, itemsDentroDelinventario;

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
                slots[j, i].GetComponent<Slot>().SlotQueSoy = i * slotsX + j;
            }
        }
    }

    public void recargarSlotsOcupados()
    {
        for (int i = 0; i < slotsY; i++)
        {
            for (int j = 0; j < slotsX; j++)
            {
                slots[j, i].GetComponent<Slot>().clear();

                for (int k = 0; k < itemsDentroDelinventario.Length; k++)
                {
                    slots[j, i].GetComponent<Slot>().DetectarSiOcupado(itemsDentroDelinventario[k].GetComponent<RectTransform>());
                }

                slotsOcupados[j, i] = slots[j, i].GetComponent<Slot>().Ocupado;
            }
        }
    }

    void Update()
    {
        itemsDentroDelinventario = GameObject.FindGameObjectsWithTag("Item");

        if (inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(0).GetComponent<Image>().enabled = false;
            transform.GetChild(1).GetComponent<Image>().enabled = false;
            transform.GetChild(2).gameObject.SetActive(false);

            for (int i = 0; i < itemsDentroDelinventario.Length; i++)
            {
                itemsDentroDelinventario[i].GetComponent<Image>().enabled = false;
                itemsDentroDelinventario[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
            }

            inventarioAbierto = false;
        }
        else if (!inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
        {
            transform.GetChild(0).GetComponent<Image>().enabled = true;
            transform.GetChild(1).GetComponent<Image>().enabled = true;
            transform.GetChild(2).gameObject.SetActive(true);
            inventarioAbierto = true;

            for (int i = 0; i < itemsDentroDelinventario.Length; i++)
            {
                itemsDentroDelinventario[i].GetComponent<Image>().enabled = true;
                itemsDentroDelinventario[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
            }

            recargarSlotsOcupados();
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
        recargarSlotsOcupados();
    }

    public GameObject item;

    public Transform ObjetosEnElInventario;

    public void NewItemOnInventory(ItemInfo info)
    {
        int ancho = info.SlotsX - 1;
        int alto = info.SlotsY - 1;

        recargarSlotsOcupados();

        PosicionLibre posicionLibre = EncontrarPosicionLibre(slotsOcupados, ancho, alto);


        // Muestra el resultado
        if (posicionLibre != null)
        {
            float posicionObjetivoX = slots[posicionLibre.Fila, posicionLibre.Columna].transform.position.x;
            float posicionObjetivoY = slots[posicionLibre.Fila, posicionLibre.Columna].transform.position.y;

            GameObject newItem = Instantiate(item, new Vector3(posicionObjetivoX - 18, posicionObjetivoY + 18), Quaternion.identity, ObjetosEnElInventario);
            // esos numeros "18" no son fijos y en caso de que no salga el item bien en la cuadricula hay que ajustarlos

            newItem.GetComponent<ItemInventory>().info = info;

            if (inventarioAbierto)
            {
                newItem.GetComponent<Image>().enabled = true;
                newItem.transform.GetChild(0).GetComponent<Image>().enabled = true;
            }
            else
            {
                newItem.GetComponent<Image>().enabled = false;
                newItem.transform.GetChild(0).GetComponent<Image>().enabled = false;
            }

        }
        else
        {
            print("No se encontró espacio libre para el objeto.");
        }

        recargarSlotsOcupados();

    }
    // Función para encontrar la primera posición libre para un objeto en la matriz
    static PosicionLibre EncontrarPosicionLibre(bool[,] matriz, int anchoObjeto, int altoObjeto)
    {
        int filas = matriz.GetLength(0);
        int columnas = matriz.GetLength(1);

        for (int columna = 0; columna < columnas - altoObjeto; columna++)
        { 
            for (int fila = 0; fila < filas - anchoObjeto; fila++)
            {
                // Verifica si hay suficiente espacio libre para el objeto en esta posición
                bool espacioLibre = true;
                for (int i = fila; i <= fila + anchoObjeto; i++)
                {
                    for (int j = columna; j <= columna + altoObjeto; j++)
                    {
                        if (matriz[i, j] == true)
                        {
                            espacioLibre = false;
                            break;
                        }
                    }
                    if (!espacioLibre) break;
                }


                print(matriz[fila,columna] + " + " + columnas + " y " + filas);

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
