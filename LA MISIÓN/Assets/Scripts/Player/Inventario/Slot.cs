using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int SlotQueSoy;
    private InventoryManager inventoryManager;
    void Start()
    {
        inventoryManager = transform.parent.parent.GetComponent<InventoryManager>();
    }

    public void itemSelected()
    {
        inventoryManager.BtnPressed(SlotQueSoy);
    }
}
