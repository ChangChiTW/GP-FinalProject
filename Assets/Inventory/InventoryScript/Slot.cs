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
        //itemDescription.SetActive(false);
        //InventoryManager.UpdateItemInfo(slotItem.itemInfo);
        if(!playerInventory.itemList.Contains(slotItem))
        {
            playerInventory.itemList.Add(slotItem);
            //InventoryManager.CreateNewItem(slotItem);
        }
        else
        {
            slotItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }

    public void AddNewItem()
    {
        
    }
}
