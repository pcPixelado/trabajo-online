using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSeleccionado : MonoBehaviour
{
    public RectTransform Puntero;

    public GameObject objetoSeleccionado;
    public GameObject objetoEnElSuelo;
    private RectTransform rectTransform;
    public InventoryManager inventoryManager;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        Rect uiObjectLocalRect = new(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y, rectTransform.rect.width, rectTransform.rect.height);

        Vector2 mousePos = (Puntero.position / Camera.main.scaledPixelWidth * 1600) - new Vector3(600, 750);

        if (!uiObjectLocalRect.Contains(mousePos))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetButtonDown("xBtn"))
            {
                transform.position = new Vector3(-100, -10);
            }
        }

    }

    public void SoltarItemSeleccionado()
    {
        ItemInfo info;
        if (objetoSeleccionado.GetComponent<ItemInventory>() != null)
        {
            info = objetoSeleccionado.GetComponent<ItemInventory>().info;
        }
        else info = objetoSeleccionado.GetComponent<ArmasInventory>().info;

        Destroy(objetoSeleccionado);
        GameObject newitem = Instantiate(objetoEnElSuelo, transform.parent.parent.parent.GetChild(1).position, Quaternion.identity);
        newitem.GetComponent<ItemGround>().info = info;

        if (objetoSeleccionado.GetComponent<ItemInventory>() != null)
        {
            newitem.GetComponent<ItemGround>().Municion = objetoSeleccionado.GetComponent<ItemInventory>().Munición;
        }
        else
        {
            newitem.GetComponent<ItemGround>().Municion = objetoSeleccionado.GetComponent<ArmasInventory>().MunicionEnElCartucho;
            newitem.GetComponent<ItemGround>().cartuchoEquipado = objetoSeleccionado.GetComponent<ArmasInventory>().CartuchoEquipado;
        }

        transform.position = new Vector3(-100, -10);
    }
    public void UsarEquiparItemSeleccionado()
    {
        if (objetoSeleccionado.GetComponent<ArmasInventory>() != null)
        {
            inventoryManager.EquiparArma(objetoSeleccionado);
            print("arma");
            transform.position = new Vector3(-100, -10);
        }
        else print("Item");
    }
}
