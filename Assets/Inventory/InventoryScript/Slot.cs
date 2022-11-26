using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public GameObject itemDescription;
    public Inventory playerInventory;

    public void ItemOnClicked()
    {   
        InventoryManager.ShowDes();
        InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        InventoryManager.ChooseItem(slotItem);
    }

    public void BuyItem()
    {
        InventoryManager.AddNewItem();
    }
}
