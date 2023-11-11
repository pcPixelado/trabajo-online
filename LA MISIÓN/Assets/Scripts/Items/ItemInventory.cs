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

        RectTransform rectTransform = transform.GetChild(0).GetComponent<RectTransform>();

        rectTransform.sizeDelta = new Vector2(info.sprite.rect.width, info.sprite.rect.height);
        rectTransform.localScale = new Vector3(info.inventoryScale, info.inventoryScale);
    }
}
