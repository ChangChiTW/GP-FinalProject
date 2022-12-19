using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInfo
{
    public string name;
    public int cost;
    public int basePrice;
}

public class AdventurerInfo
{
    public string name;
    public string job;
    public Sprite img;
    public float hp;
    public int atk;
    public int def;
    public int speed;
    public ItemInfo[] items;
    public List<Sprite> itemImgs;
}

public class AdventurerManager : MonoBehaviour
{
    private AdventurerInfo[] _adventurerList = new AdventurerInfo[3];

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        initAdventurerList();
    }

    private void initAdventurerList()
    {
        _adventurerList[0].name = "Knight";
        _adventurerList[0].job = "Knight";
        _adventurerList[0].img = Resources.Load<Sprite>("Adventurer/Knight");
        _adventurerList[0].hp = 90;
        _adventurerList[0].atk = 13;
        _adventurerList[0].def = 3;
        _adventurerList[0].speed = 10;
        _adventurerList[0].items = new ItemInfo[3];
        _adventurerList[0].items[0].name = "Sword";
        _adventurerList[0].items[0].cost = 100;
        _adventurerList[0].items[0].basePrice = 100;
        _adventurerList[0].items[1].name = "Shield";
        _adventurerList[0].items[1].cost = 100;
        _adventurerList[0].items[1].basePrice = 100;
        _adventurerList[0].items[2].name = "Armor";
        _adventurerList[0].items[2].cost = 100;
        _adventurerList[0].items[2].basePrice = 100;
        _adventurerList[0].itemImgs = new List<Sprite>();
        _adventurerList[1].name = "Archer";
        _adventurerList[1].job = "Archer";
        _adventurerList[1].img = Resources.Load<Sprite>("Adventurer/Archer");
        _adventurerList[1].hp = 70;
        _adventurerList[1].atk = 16;
        _adventurerList[1].def = 2;
        _adventurerList[1].speed = 20;
        _adventurerList[1].items = new ItemInfo[3];
        _adventurerList[1].items[0].name = "Bow";
        _adventurerList[1].items[0].cost = 100;
        _adventurerList[1].items[0].basePrice = 100;
        _adventurerList[1].items[1].name = "Arrow";
        _adventurerList[1].items[1].cost = 100;
        _adventurerList[1].items[1].basePrice = 100;
        _adventurerList[1].items[2].name = "Armor";
        _adventurerList[1].items[2].cost = 100;
        _adventurerList[1].items[2].basePrice = 100;
        _adventurerList[1].itemImgs = new List<Sprite>();
        _adventurerList[2].name = "Mage";
        _adventurerList[2].job = "Mage";
        _adventurerList[2].img = Resources.Load<Sprite>("Adventurer/Mage");
        _adventurerList[2].hp = 60;
        _adventurerList[2].atk = 20;
        _adventurerList[2].def = 1;
        _adventurerList[2].speed = 15;
        _adventurerList[2].items = new ItemInfo[3];
        _adventurerList[2].items[0].name = "Staff";
        _adventurerList[2].items[0].cost = 100;
        _adventurerList[2].items[0].basePrice = 100;
        _adventurerList[2].items[1].name = "Book";
        _adventurerList[2].items[1].cost = 100;
        _adventurerList[2].items[1].basePrice = 100;
        _adventurerList[2].items[2].name = "Armor";
        _adventurerList[2].items[2].cost = 100;
        _adventurerList[2].items[2].basePrice = 100;
        _adventurerList[2].itemImgs = new List<Sprite>();
    }

    public AdventurerInfo[] GetAdventurerList()
    {
        return _adventurerList;
    }

    public void SetAdventurerList(AdventurerInfo[] a)
    {
        _adventurerList = a;
    }
}
