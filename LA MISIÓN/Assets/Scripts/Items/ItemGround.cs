using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGround : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public ItemInfo info;
    void Update()
    {
        spriteRenderer.sprite = info.sprite;
    }
}
