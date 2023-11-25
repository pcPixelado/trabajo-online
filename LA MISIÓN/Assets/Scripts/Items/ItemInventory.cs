using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public Image image;
    public ItemInfo info;
    private RectTransform rectTransform, spriteRectTransform;
    public GameObject clickDerecho;
    public InventoryManager inventoryManager;

    public int Munición;

    public TMP_Text AmmoIndicator;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        clickDerecho = GameObject.FindGameObjectWithTag("ClickDerecho");

        spriteRectTransform = transform.GetChild(0).GetComponent<RectTransform>();

        image.enabled = false;
    }
    public bool AgarrandoItem;
    private Vector2 PosicionInicial, distanciaAlCentro, NuevaPosiblePosicion;
    void Update()
    {
        image.sprite = info.sprite[0];

        rectTransform.sizeDelta = new Vector2(info.SlotsX * 120, info.SlotsY * 120);

        spriteRectTransform.sizeDelta = new Vector2(info.sprite[0].rect.width, info.sprite[0].rect.height);
        spriteRectTransform.localScale = new Vector3(info.inventoryScale, info.inventoryScale);



        Rect uiObjectLocalRect = new Rect(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y - rectTransform.rect.height + 780, rectTransform.rect.width, rectTransform.rect.height);

        Vector2 mousePos = (Input.mousePosition / Camera.main.scaledPixelWidth * 1600) - new Vector3(460, 60);

        if (uiObjectLocalRect.Contains(mousePos) && image.enabled)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                clickDerecho.transform.position = Input.mousePosition;

                clickDerecho.GetComponent<ItemSeleccionado>().objetoSeleccionado = gameObject;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                AgarrandoItem = true;
                PosicionInicial = rectTransform.anchoredPosition;
                distanciaAlCentro = rectTransform.anchoredPosition - mousePos;
            }

            if (info.municionMaxima > 0)
            {
                AmmoIndicator.text = Munición + "/" + info.municionMaxima;
            }
            else AmmoIndicator.text = "";
        }
        else AmmoIndicator.text = "";

        if (AgarrandoItem && Input.GetKey(KeyCode.Mouse0))
        {
            rectTransform.anchoredPosition = mousePos + distanciaAlCentro;

            float DistanciaMasCercana = 1000;
            Vector2 coordsMasCercanas = Vector2.zero;
            for (int i = 0; i < inventoryManager.slots.GetLength(0); i++)
            {
                for (int j = 0; j < inventoryManager.slots.GetLength(1); j++)
                {
                    if (Vector3.Distance(inventoryManager.slots[i, j].transform.position, transform.position) < DistanciaMasCercana)
                    {
                        DistanciaMasCercana = Vector3.Distance(inventoryManager.slots[i, j].transform.position, transform.position);
                        coordsMasCercanas = new Vector2(i, j);
                    }
                }
            }

            if (inventoryManager.ConfirmarUnaPosicion(Mathf.RoundToInt(coordsMasCercanas.x), Mathf.RoundToInt(coordsMasCercanas.y), info.SlotsX, info.SlotsY))
            {
                NuevaPosiblePosicion = inventoryManager.slots[Mathf.RoundToInt(coordsMasCercanas.x), Mathf.RoundToInt(coordsMasCercanas.y)].GetComponent<RectTransform>().anchoredPosition + new Vector2(-1, 1);
            }
            else NuevaPosiblePosicion = PosicionInicial;
        }
        else if (AgarrandoItem && Input.GetKeyUp(KeyCode.Mouse0))
        {
            AgarrandoItem = false;

            rectTransform.anchoredPosition = NuevaPosiblePosicion;
        }
    }
}