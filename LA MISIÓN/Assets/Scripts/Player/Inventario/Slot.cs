using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int SlotQueSoy;
    public bool Ocupado;
    private InventoryManager inventoryManager;
    private RectTransform uiObjectRect;
    void Awake()
    {
        inventoryManager = transform.parent.parent.GetComponent<InventoryManager>();
        uiObjectRect = GetComponent<RectTransform>();
    }

    public void Clear()
    {
        Ocupado = false;
    }

    public void DetectarSiOcupado(RectTransform objetoEnElInventario)
    {
        Rect uiObjectLocalRect = new Rect(objetoEnElInventario.anchoredPosition.x, objetoEnElInventario.anchoredPosition.y - objetoEnElInventario.rect.height, objetoEnElInventario.rect.width, objetoEnElInventario.rect.height);

        if(!Ocupado)Ocupado = uiObjectLocalRect.Contains(uiObjectRect.anchoredPosition);

        //if (Ocupado) print(Ocupado + " es " + gameObject);
    }

    public void ItemSelected()
    {
        inventoryManager.BtnPressed(SlotQueSoy);
    }
}
