using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public Image image;
    public ItemInfo info;
    public RectTransform rectTransform;
    public GameObject clickDerecho;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        clickDerecho = GameObject.FindGameObjectWithTag("ClickDerecho");
    }
    void Update()
    {
        image.sprite = info.sprite;

        RectTransform spriteRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(info.SlotsX * 120, info.SlotsY * 120);

        spriteRectTransform.sizeDelta = new Vector2(info.sprite.rect.width, info.sprite.rect.height);
        spriteRectTransform.localScale = new Vector3(info.inventoryScale, info.inventoryScale);



        Rect uiObjectLocalRect = new Rect(rectTransform.position.x, rectTransform.position.y - rectTransform.rect.height, rectTransform.rect.width, rectTransform.rect.height);


        if (uiObjectLocalRect.Contains(Input.mousePosition))
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                clickDerecho.transform.position = Input.mousePosition;

                clickDerecho.GetComponent<ItemSeleccionado>().objetoSeleccionado = gameObject;
            }
        }
    }
}