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

    public void DetectarSiOcupado(RectTransform objetoEnElInventario)
    {
        Rect uiObjectLocalRect = objetoEnElInventario.rect;

        Ocupado = uiObjectLocalRect.Contains(uiObjectRect.rect.center);
        print(uiObjectLocalRect + " y " + uiObjectRect.position);
        if (Ocupado) print(Ocupado + " es " + gameObject);
    }

    public void itemSelected()
    {
        inventoryManager.BtnPressed(SlotQueSoy);
    }
}
