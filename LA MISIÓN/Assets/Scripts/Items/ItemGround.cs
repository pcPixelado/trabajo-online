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
    private void Awake()
    {
        inventoryManager = GameObject.FindGameObjectWithTag("InventoryManager").GetComponent<InventoryManager>();
    }
    void Update()
    {
        spriteRenderer.sprite = info.sprite;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            indicador.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryManager.NewItemOnInventory(info);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            indicador.SetActive(false);
        }
    }
}
