using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeSlot : MonoBehaviour
{
    public Item slotItem;
    public Image slotImage;
    public Text slotNum;
    public GameObject itemDescription;
    public Inventory playerInventory;

    public void ItemOnClicked()
    {   
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayBtnClick();
        TradeManager.ShowDes();
        TradeManager.UpdateItemInfo(slotItem.itemName, slotItem.itemInfo, slotItem.HP, slotItem.ATK, slotItem.DEF, slotItem.itemImage, slotItem.price);
        TradeManager.ChooseItem(slotItem);
    }

    public void CloseDes()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayBtnClick();
        TradeManager.CloseDes();
    }
}
