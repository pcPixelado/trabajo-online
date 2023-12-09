using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemGround : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ItemInfo info;
    public GameObject indicador;
    private InventoryManager inventoryManager;
    private bool OnTrigger;

    public int Municion;
    public bool cartuchoEquipado;
    private void Awake()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }
    void Update()
    {
        spriteRenderer.sprite = info.sprite[0];

        if (OnTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E) || Input.GetButtonDown("uBtn"))
            {
                inventoryManager.NewItemOnInventory(info, Municion, cartuchoEquipado);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            indicador.SetActive(true);
            OnTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            indicador.SetActive(false);
            OnTrigger = false;
        }
    }
}
