using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buyItem : MonoBehaviour
{
    public Item thisItem;
    public Inventory playerInventory;


    public void AddNewItem()
    {
        if(!playerInventory.itemList.Contains(thisItem))
        {
            playerInventory.itemList.Add(thisItem);
            //InventoryManager.CreateNewItem(thisItem);
        }
        else
        {
            thisItem.itemHeld += 1;
        }

        InventoryManager.RefreshItem();
    }
}
