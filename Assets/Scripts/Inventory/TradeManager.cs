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
    public Text itemSpeed;
    public Image itemPic;
    public Text itemPrice;
    public Text originalPrice;
    public Text raiseRatio;
    private StateManager _stateManager;
    public Text OwnMoney;
    public Text AdventurerMoney;

    public GameObject itemDes;
    public Item chosenItem;

    public Button Raise;

    void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
        instance._stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
    }

    void Start()
    {
        RefreshItem();
    }

    void Update()
    {
        instance.OwnMoney.text = "$" + instance._stateManager.GetBalance();
        instance.AdventurerMoney.text = "$" + instance._stateManager.GetAdventurerBalance();
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

    public static bool AddNewItem()
    {
        if (instance.chosenItem.itemRaise == 0)
        {
            if (instance._stateManager.GetAdventurerBalance() < -1.0 * instance.chosenItem.price)
                return false;
        }
        else if (instance.chosenItem.itemRaise == 1)
        {
            if (instance._stateManager.GetAdventurerBalance() < -2.0 * instance.chosenItem.price)
                return false;
        }
        else
        {
            if (instance._stateManager.GetAdventurerBalance() < -0.5 * instance.chosenItem.price)
                return false;
        }
        if (instance.chosenItem.itemRaise == 0)
            instance._stateManager.AddBalance(
                System.Convert.ToInt32(System.Math.Floor(-1.0 * instance.chosenItem.price))
            );
        else if (instance.chosenItem.itemRaise == 1)
            instance._stateManager.AddBalance(
                System.Convert.ToInt32(System.Math.Floor(-2.0 * instance.chosenItem.price))
            );
        else
            instance._stateManager.AddBalance(
                System.Convert.ToInt32(System.Math.Floor(-0.5 * instance.chosenItem.price))
            );
        if (instance.chosenItem.itemRaise == 0)
            instance._stateManager.AddAdventurerBalance(
                System.Convert.ToInt32(System.Math.Floor(1.0 * instance.chosenItem.price))
            );
        else if (instance.chosenItem.itemRaise == 1)
            instance._stateManager.AddAdventurerBalance(
                System.Convert.ToInt32(System.Math.Floor(2.0 * instance.chosenItem.price))
            );
        else
            instance._stateManager.AddAdventurerBalance(
                System.Convert.ToInt32(System.Math.Floor(0.5 * instance.chosenItem.price))
            );
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
        return true;
    }

    public static void UpdateItemInfo(
        string itemName,
        string itemDescription,
        float HP,
        float ATK,
        float DEF,
        float SPD,
        Sprite itemImage,
        int price
    )
    {
        if (instance.chosenItem.itemRaise == 0)
            instance.Raise.gameObject.SetActive(true);
        instance.itemName.text = itemName;
        instance.itemStrength.text = "HP:       +" + HP.ToString();
        instance.itemWisdom.text = "ATK:     +" + ATK.ToString();
        instance.itemLuck.text = "DEF:     +" + DEF.ToString();
        instance.itemSpeed.text = "SPEED:  +" + SPD.ToString();
        instance.itemPic.sprite = itemImage;
        instance.originalPrice.text = (-1 * price).ToString();
        int ratio = instance._stateManager.GetRaiseRatio();
        instance.raiseRatio.text = ratio.ToString() + "%";
        //instance.itemPrice.text = "+" + (-1 * ratio * 0.01 * price).ToString();
        if (instance.chosenItem.itemRaise == 0)
            instance.itemPrice.text = "+" + (-1 * price).ToString();
        else if (instance.chosenItem.itemRaise == 1)
            instance.itemPrice.text = "+" + (-2 * price).ToString();
        else
            instance.itemPrice.text = "+" + (-0.5 * price).ToString();
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

    public static void RaisePrice()
    {
        System.Random random = new System.Random();
        int rand = random.Next(0, 99);
        int ratio = instance._stateManager.GetRaiseRatio();
        if (rand < ratio)
        {
            instance.chosenItem.itemRaise = 1;
            instance.itemPrice.text = "+" + (-1 * 2 * instance.chosenItem.price).ToString();
        }
        else
        {
            instance.chosenItem.itemRaise = 2;
            instance.itemPrice.text = "+" + (-1 * 0.5 * instance.chosenItem.price).ToString();
        }
        instance.Raise.gameObject.SetActive(false);
    }
}
