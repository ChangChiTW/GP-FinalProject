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
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        TradeManager.ChooseItem(slotItem);
        TradeManager.ShowDes();
        TradeManager.UpdateItemInfo(
            slotItem.itemName,
            slotItem.itemInfo,
            slotItem.HP,
            slotItem.ATK,
            slotItem.DEF,
            slotItem.SPEED,
            slotItem.itemImage,
            slotItem.price
        );
    }

    public void CloseDes()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        TradeManager.CloseDes();
    }
}
