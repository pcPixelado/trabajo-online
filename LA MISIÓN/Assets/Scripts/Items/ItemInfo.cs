using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ItemInfo", fileName = "Item")]
public class ItemInfo : ScriptableObject
{
    public float SlotsX, SlotsY, inventoryScale;
    public Sprite sprite;

}

