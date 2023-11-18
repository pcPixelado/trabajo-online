using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSeleccionado : MonoBehaviour
{
    public GameObject objetoSeleccionado;
    public GameObject objetoEnElSuelo;
    private RectTransform rectTransform;
    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        Rect uiObjectLocalRect = new Rect(rectTransform.position.x, rectTransform.position.y - rectTransform.rect.height, rectTransform.rect.width, rectTransform.rect.height);


        if (!uiObjectLocalRect.Contains(Input.mousePosition))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                transform.position = new Vector3(-100, -10);
            }
        }

    }

    public void SoltarItemSeleccionado()
    {
        ItemInfo info = objetoSeleccionado.GetComponent<ItemInventory>().info;
        Destroy(objetoSeleccionado);
        GameObject newitem = Instantiate(objetoEnElSuelo, transform.parent.parent.parent.GetChild(1).position, Quaternion.identity);
        newitem.GetComponent<ItemGround>().info = info;
        transform.position = new Vector3(-100, -10);
    }
    public void UsarEquiparItemSeleccionado()
    {

    }
}
