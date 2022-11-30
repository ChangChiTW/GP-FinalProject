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
    public Text itemName;
    public Text itemStrength;
    public Text itemWisdom;
    public Text itemLuck;
    public Image itemPic;
    public Text itemPrice;

    public Inventory myShop;
    public GameObject shopGrid;
    public Slot shopPrefab;

    //private PlayerManager _playerManager;
    private int currentMoney;
    private int currentDebt;
    public Text OwnDebt;
    public Text OwnMoney;

    public GameObject itemDes;
    public Item chosenItem;

    void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
        //instance._playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }


    void Start()
    {
        RefreshItem();
        ShopItem();
        instance.itemInformation.text = "";
        instance.currentMoney = 1000;
        instance.currentDebt = 2000;
    }

    void Update()
    {
        instance.OwnMoney.text = "$" + instance.currentMoney;
        instance.OwnDebt.text = "Debt: " + instance.currentDebt;
    }

    /*private void OnEnabled()
    {
        RefreshItem();
    }*/

    public static void ShowDes()
    {
        instance.itemDes.SetActive(true);
    }

    public static void CloseDes()
    {
        instance.itemDes.SetActive(false);
    }

    public static void ChooseItem(Item pointedItem)
    {
        instance.chosenItem = pointedItem;
    }

    public static void AddNewItem()
    {
        if(instance.currentMoney + instance.chosenItem.price<0)
            return;
        if(!instance.myBag.itemList.Contains(instance.chosenItem))
        {
            instance.myBag.itemList.Add(instance.chosenItem);
            //InventoryManager.CreateNewItem(chosenItem);
        }
        else
        {
            instance.chosenItem.itemHeld += 1;
        }

        //instance._playerManager.AddBalance(-1*price);
        instance.currentMoney += instance.chosenItem.price;

        RefreshItem();
    }

    public static int GetCurrentMoney()
    {
        return instance.currentMoney;
    }

    public static void AddDebt(int newDebt)
    {
        instance.currentDebt+=newDebt;
    }

    public static int GetCurrentDebt()
    {
        return instance.currentDebt;
    }

    public static void UpdateItemInfo(string itemName, string itemDescription, string strength, string wisdom, string luck, Sprite itemImage,int price)
    {
        //itemDes.SetActive(true);
        instance.itemName.text = itemName;
        instance.itemInformation.text = itemDescription;
        instance.itemStrength.text = strength;
        instance.itemWisdom.text = wisdom;
        instance.itemLuck.text = luck;
        instance.itemPic.sprite = itemImage;
        instance.itemPrice.text = price.ToString();
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
