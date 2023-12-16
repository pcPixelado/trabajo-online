using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public RectTransform Puntero;

    public GameObject[,] slots;
    public bool[,] slotsOcupados;

    public GameObject[] slotsDetected, itemsDentroDelinventario;

    [HideInInspector]public int slotsX = 9, slotsY = 6;

    [HideInInspector]public bool inventarioAbierto = false;

    public GameObject[] armasEquipadas;

    public PlayerController playerController;

    public GameObject[] SlotsDeArmas;
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

        CerrarInventario();
    }

    private void CerrarInventario()
    {
        transform.GetChild(0).GetComponent<Image>().enabled = false;
        transform.GetChild(1).GetComponent<Image>().enabled = false;
        transform.GetChild(2).GetComponent<Image>().enabled = false;

        transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new(11, -35);
        transform.GetChild(2).GetComponent<RectTransform>().localScale = new(0.5f, 0.5f, 1);

        for (int i = 0; i < itemsDentroDelinventario.Length; i++)
        {
            itemsDentroDelinventario[i].GetComponent<Image>().enabled = false;
            itemsDentroDelinventario[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
        }

        inventarioAbierto = false;
    }

    private void AbrirInventario()
    {
        transform.GetChild(0).GetComponent<Image>().enabled = true;
        transform.GetChild(1).GetComponent<Image>().enabled = true;
        transform.GetChild(2).GetComponent<Image>().enabled = true;

        transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new(31, 60);
        transform.GetChild(2).GetComponent<RectTransform>().localScale = new(1,1,1);

        inventarioAbierto = true;

        for (int i = 0; i < itemsDentroDelinventario.Length; i++)
        {
            itemsDentroDelinventario[i].GetComponent<Image>().enabled = true;
            itemsDentroDelinventario[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
        }

        RecargarSlotsOcupados();
    }

    public void RecargarSlotsOcupados()
    {
        for (int i = 0; i < slotsY; i++)
        {
            for (int j = 0; j < slotsX; j++)
            {
                slots[j, i].GetComponent<Slot>().Clear();

                for (int k = 0; k < itemsDentroDelinventario.Length; k++)
                {
                    if(itemsDentroDelinventario[k].GetComponent<ArmasInventory>() != null)
                    {
                        if (!itemsDentroDelinventario[k].GetComponent<ArmasInventory>().AgarrandoItem)
                        {
                            slots[j, i].GetComponent<Slot>().DetectarSiOcupado(itemsDentroDelinventario[k].GetComponent<RectTransform>());
                        }
                    }
                    else
                    {
                        if (!itemsDentroDelinventario[k].GetComponent<ItemInventory>().AgarrandoItem)
                        {
                            slots[j, i].GetComponent<Slot>().DetectarSiOcupado(itemsDentroDelinventario[k].GetComponent<RectTransform>());
                        }
                    }    
                }

                slotsOcupados[j, i] = slots[j, i].GetComponent<Slot>().Ocupado;
            }
        }
    }

    void Update()
    {
        itemsDentroDelinventario = GameObject.FindGameObjectsWithTag("Item");

        if (Gamepad.all.Count > 0)
        {
            if (inventarioAbierto && Input.GetButtonDown("Touchpad"))
            {
                CerrarInventario();
            }
            else if (!inventarioAbierto && Input.GetButtonDown("Touchpad"))
            {
                AbrirInventario();
            }
        }
        else
        {
            if (inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
            {
                CerrarInventario();
            }
            else if (!inventarioAbierto && Input.GetKeyDown(KeyCode.Tab))
            {
                AbrirInventario();
            }
        }


        for (int i = 0; i < armasEquipadas.Length; i++)
        {
            if (armasEquipadas[i] == null)
            {
                SlotsDeArmas[i].transform.GetChild(0).GetComponent<Image>().enabled = false;
                SlotsDeArmas[i].GetComponent<Image>().color = Color.gray;
            }
            else
            {
                SlotsDeArmas[i].transform.GetChild(0).GetComponent<Image>().enabled = true;
                SlotsDeArmas[i].transform.GetChild(0).GetComponent<Image>().sprite = armasEquipadas[i].transform.GetChild(0).GetComponent<Image>().sprite;
                SlotsDeArmas[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = armasEquipadas[i].transform.GetChild(0).GetComponent<RectTransform>().sizeDelta;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetAxis("Vertical7Axis") > 0)
        {
            if(armasEquipadas[0] != null) playerController.gameObjectArmaEquipada = armasEquipadas[0];
            for (int i = 0;i < armasEquipadas.Length; i++)
            {
                if (armasEquipadas[i] != null)
                {
                    if (i == 0)
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = true;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.cyan;
                    }
                    else
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = false;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.white;
                    }
                }
                else SlotsDeArmas[i].GetComponent<Image>().color = Color.gray;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetAxis("Horizontal6Axis") < 0)
        {
            if (armasEquipadas[1] != null) playerController.gameObjectArmaEquipada = armasEquipadas[1];
            for (int i = 0; i < armasEquipadas.Length; i++)
            {
                if (armasEquipadas[i] != null)
                {
                    if (i == 1)
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = true;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.cyan;
                    }
                    else
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = false;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.white;
                    }
                }
                else SlotsDeArmas[i].GetComponent<Image>().color = Color.gray;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetAxis("Horizontal6Axis") > 0)
        {
            if (armasEquipadas[2] != null) playerController.gameObjectArmaEquipada = armasEquipadas[2];
            for (int i = 0; i < armasEquipadas.Length; i++)
            {
                if (armasEquipadas[i] != null)
                {
                    if (i == 2)
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = true;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.cyan;
                    }
                    else
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = false;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.white;
                    }
                }
                else SlotsDeArmas[i].GetComponent<Image>().color = Color.gray;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetAxis("Vertical7Axis") < 0)
        {
            if (armasEquipadas[3] != null) playerController.gameObjectArmaEquipada = armasEquipadas[3];
            for (int i = 0; i < armasEquipadas.Length; i++)
            {
                if (armasEquipadas[i] != null)
                {
                    if (i == 3)
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = true;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.cyan;
                    }
                    else
                    {
                        armasEquipadas[i].GetComponent<ArmasInventory>().ArmaEquipada = false;
                        SlotsDeArmas[i].GetComponent<Image>().color = Color.white;
                    }
                }
                else SlotsDeArmas[i].GetComponent<Image>().color = Color.gray;
            }
        }
    }

    public void BtnPressed(int Btn)
    {
        print(Btn);
        RecargarSlotsOcupados();
    }

    public GameObject item, arma;

    public Transform ObjetosEnElInventario;

    public void NewItemOnInventory(ItemInfo info)
    {
        NewItemOnInventory(info, 0, false);
    }
    public void NewItemOnInventory(ItemInfo info, bool armaConCartucho)
    {
        NewItemOnInventory(info, 0, armaConCartucho);
    }
    public void NewItemOnInventory(ItemInfo info, int municionDelItem)
    {
        NewItemOnInventory(info, municionDelItem, false);
    }

    public void NewItemOnInventory(ItemInfo info, int municionDelItem, bool armaConCartucho)
    {
        int ancho = info.SlotsX - 1;
        int alto = info.SlotsY - 1;

        RecargarSlotsOcupados();

        PosicionLibre posicionLibre = EncontrarPosicionLibre(slotsOcupados, ancho, alto);


        // Muestra el resultado
        if (posicionLibre != null)
        {
            float posicionObjetivoX = slots[posicionLibre.Fila, posicionLibre.Columna].transform.position.x;
            float posicionObjetivoY = slots[posicionLibre.Fila, posicionLibre.Columna].transform.position.y;

            GameObject newItem;

            if (info.ItemID == 0)
            {
                newItem = Instantiate(arma, new Vector3(posicionObjetivoX - 1, posicionObjetivoY + 1), Quaternion.identity, ObjetosEnElInventario);

                newItem.GetComponent<ArmasInventory>().Puntero = Puntero;

                newItem.GetComponent<ArmasInventory>().inventoryManager = this;
                newItem.GetComponent<ArmasInventory>().info = info;
                newItem.GetComponent<ArmasInventory>().CartuchoEquipado = armaConCartucho;
                newItem.GetComponent<ArmasInventory>().slotsDeArmas = SlotsDeArmas;

                if (armaConCartucho) newItem.GetComponent<ArmasInventory>().MunicionEnElCartucho = municionDelItem;
            }
            else
            {
                newItem = Instantiate(item, new Vector3(posicionObjetivoX - 1, posicionObjetivoY + 1), Quaternion.identity, ObjetosEnElInventario);

                newItem.GetComponent<ItemInventory>().Puntero = Puntero;

                newItem.GetComponent<ItemInventory>().inventoryManager = this;
                newItem.GetComponent<ItemInventory>().info = info;
                newItem.GetComponent<ItemInventory>().Munición = municionDelItem;

                newItem.GetComponent<ItemInventory>().ItemID = info.ItemID;
            }

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

        RecargarSlotsOcupados();

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

    public bool ConfirmarUnaPosicion(int fila, int columna, int anchoObjeto, int altoObjeto)
    {
        RecargarSlotsOcupados();

        if (columna < slotsOcupados.GetLength(1) - altoObjeto + 1 && fila < slotsOcupados.GetLength(0) - anchoObjeto + 1)
        {
            bool espacioLibre = true;
            for (int i = fila; i <= fila + anchoObjeto-1; i++)
            {
                for (int j = columna; j <= columna + altoObjeto -1; j++)
                {
                    if (slotsOcupados[i, j] == true)
                    {
                        espacioLibre = false;
                        break;
                    }

                }
                if (!espacioLibre) break;
            }
            return espacioLibre;
        }
        return false;
    }

    public void EquiparArma(GameObject armaAEquipar)
    {
        EquiparArma(armaAEquipar, 0);
    }

    public void EquiparArma(GameObject armaAEquipar, int slot)
    {
        for (int i = 0; i < armasEquipadas.Length; i++)
        {
            if(armasEquipadas[i] = armaAEquipar)
            {
                armasEquipadas[i] = null;
            }
        }

        armasEquipadas[slot] = armaAEquipar;
        SlotsDeArmas[slot].GetComponent<Image>().color = Color.white;
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