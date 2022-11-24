using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public Inventory myBag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text itemInformation;

    public Inventory myShop;
    public GameObject shopGrid;
    public Slot shopPrefab;

    //public GameObject itemDes;

    void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }


    void Start()
    {
        RefreshItem();
        ShopItem();
        instance.itemInformation.text = "";
    }

    /*private void OnEnabled()
    {
        RefreshItem();
    }*/

    public static void UpdateItemInfo(string itemDescription)
    {
        //itemDes.SetActive(true);
        instance.itemInformation.text = itemDescription;
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab,instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    public static void CreateShopItem(Item item)
    {
        Slot newItem = Instantiate(instance.shopPrefab,instance.shopGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.shopGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
    }

    public static void RefreshItem()
    {
        //Debug.Log("RefreshItem");
        for(int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if(instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }

    public static void ShopItem()
    {
        //Debug.Log("RefreshItem");
        for(int i = 0; i < instance.shopGrid.transform.childCount; i++)
        {
            if(instance.shopGrid.transform.childCount == 0)
                break;
            Destroy(instance.shopGrid.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < instance.myShop.itemList.Count; i++)
        {
            CreateShopItem(instance.myShop.itemList[i]);
        }
    }
}
