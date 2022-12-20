using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeManager : MonoBehaviour
{
    static TradeManager instance;
    public Inventory myBag;
    public GameObject slotGrid;
    public TradeSlot slotPrefab;
    public Text itemName;
    public Text itemStrength;
    public Text itemWisdom;
    public Text itemLuck;
    public Image itemPic;
    public Text itemPrice;
    public Text originalPrice;
    public Text goldRatio;
    private StateManager _stateManager;
    public Text OwnMoney;

    public GameObject itemDes;
    public Item chosenItem;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        instance._stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    void Start()
    {
        RefreshItem();
    }

    void Update()
    {
        instance.OwnMoney.text = "$" + instance._stateManager.GetBalance();
    }

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
        instance._stateManager.AddBalance(-1 * instance.chosenItem.price);
        if (instance.chosenItem.itemHeld - 1 == 0)
        {
            instance.itemDes.SetActive(false);
            instance.myBag.itemList.Remove(instance.chosenItem);
        }
        else
        {
            instance.chosenItem.itemHeld -= 1;
        }
        RefreshItem();
    }

    public static void UpdateItemInfo(
        string itemName,
        string itemDescription,
        float HP,
        float ATK,
        float DEF,
        Sprite itemImage,
        int price
    )
    {
        instance.itemName.text = itemName;
        instance.itemStrength.text = "HP:    " + HP.ToString();
        instance.itemWisdom.text = "ATK:  " + ATK.ToString();
        instance.itemLuck.text = "DEF:   " + DEF.ToString();
        instance.itemPic.sprite = itemImage;
        instance.originalPrice.text = (-1 * price).ToString();
        int ratio = instance._stateManager.GetGoldRatio();
        instance.goldRatio.text = ratio.ToString() + "%";
        instance.itemPrice.text = "+" + (-1 * ratio * 0.01 * price).ToString();
    }

    public static void CreateNewItem(Item item)
    {
        TradeSlot newItem = Instantiate(
            instance.slotPrefab,
            instance.slotGrid.transform.position,
            Quaternion.identity
        );
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.itemImage;
        newItem.slotNum.text = item.itemHeld.ToString();
    }

    public static void RefreshItem()
    {
        //Debug.Log("RefreshItem");
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            if (instance.slotGrid.transform.childCount == 0)
                break;
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < instance.myBag.itemList.Count; i++)
        {
            CreateNewItem(instance.myBag.itemList[i]);
        }
    }

    public static Item GetChosenItem()
    {
        return instance.chosenItem;
    }
}
