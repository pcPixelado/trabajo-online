using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInventory : MonoBehaviour
{
    public Image image;
    public ItemInfo info;
    void Update()
    {
        image.sprite = info.sprite;

        RectTransform spriteRectTransform = transform.GetChild(0).GetComponent<RectTransform>();
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(info.SlotsX * 120, info.SlotsY * 120);

        spriteRectTransform.sizeDelta = new Vector2(info.sprite.rect.width, info.sprite.rect.height);
        spriteRectTransform.localScale = new Vector3(info.inventoryScale, info.inventoryScale);
    }
}
