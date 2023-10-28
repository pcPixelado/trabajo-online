using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int SlotQueSoy;
    private InventarioManager inventarioManager;
    void Start()
    {
        inventarioManager = transform.parent.parent.GetComponent<InventarioManager>();
    }

    public void itemSelected()
    {
        inventarioManager.BtnPressed(SlotQueSoy);
    }
}
