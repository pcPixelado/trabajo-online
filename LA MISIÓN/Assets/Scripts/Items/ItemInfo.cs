using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemInfo", fileName = "Item")]
public class ItemInfo : ScriptableObject
{
    public int SlotsX, SlotsY;
    public float inventoryScale;
    public Sprite sprite;

}

