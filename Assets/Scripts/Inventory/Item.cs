using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public Sprite itemImage;
    public int itemHeld;

    [TextArea]
    public string itemInfo;
    public bool equip;
    public float HP;
    public float ATK;
    public float DEF;
    public float SPEED;
    public int price;
}
