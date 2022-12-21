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
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        InventoryManager.ShowDes();
        InventoryManager.UpdateItemInfo(slotItem.itemName, slotItem.itemInfo, slotItem.HP, slotItem.ATK, slotItem.DEF, slotItem.itemImage, slotItem.price);
        InventoryManager.ChooseItem(slotItem);
    }

    public void StoreItemClicked()
    {
        InventoryManager.ChangeBuySell("Buy", slotItem.price);
    }

    public void BagItemClicked()
    {
        InventoryManager.ChangeBuySell("Sell", slotItem.price);
    }

    public void BuyItem()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        InventoryManager.AddNewItem();
        InventoryManager.CloseDes();
    }

    public void CloseDes()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        InventoryManager.CloseDes();
    }
}
