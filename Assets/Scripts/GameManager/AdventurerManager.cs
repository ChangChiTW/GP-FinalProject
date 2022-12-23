using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerInfo
{
    public string name;
    public string job;
    public Sprite img;
    public float maxHp;
    public float hp;
    public float atk;
    public float def;
    public float speed;
    public List<Sprite> preferenceImgs;
    public List<Sprite> itemImgs;
}

public class AdventurerManager : MonoBehaviour
{
    private AdventurerInfo[] _adventurerList = new AdventurerInfo[3];

    void Start()
    {
        initAdventurerList();
    }

    public void initAdventurerList()
    {
        _adventurerList = new AdventurerInfo[3];
        _adventurerList[0] = NewKnight();
        _adventurerList[1] = NewArcher();
        _adventurerList[2] = NewMage();
    }

    private AdventurerInfo NewKnight()
    {
        AdventurerInfo adventurer = new AdventurerInfo();
        adventurer.name = "Knight";
        adventurer.job = "Knight";
        adventurer.img = Resources.Load<Sprite>("Adventurer/Knight");
        adventurer.maxHp = 90;
        adventurer.hp = 90;
        adventurer.atk = 13;
        adventurer.def = 3;
        adventurer.speed = 10;
        adventurer.preferenceImgs = new List<Sprite>();
        adventurer.preferenceImgs.Add(Resources.Load<Sprite>("Item/IronSword"));
        adventurer.preferenceImgs.Add(Resources.Load<Sprite>("Item/BarrelLid"));
        adventurer.itemImgs = new List<Sprite>();
        return adventurer;
    }

    private AdventurerInfo NewArcher()
    {
        AdventurerInfo adventurer = new AdventurerInfo();
        adventurer.name = "Archer";
        adventurer.job = "Archer";
        adventurer.img = Resources.Load<Sprite>("Adventurer/Archer");
        adventurer.maxHp = 70;
        adventurer.hp = 70;
        adventurer.atk = 16;
        adventurer.def = 2;
        adventurer.speed = 20;
        adventurer.preferenceImgs = new List<Sprite>();
        adventurer.preferenceImgs.Add(Resources.Load<Sprite>("Item/IronArrow"));
        adventurer.preferenceImgs.Add(Resources.Load<Sprite>("Item/CottonHood"));
        adventurer.itemImgs = new List<Sprite>();
        return adventurer;
    }

    private AdventurerInfo NewMage()
    {
        AdventurerInfo adventurer = new AdventurerInfo();
        adventurer.name = "Mage";
        adventurer.job = "Mage";
        adventurer.img = Resources.Load<Sprite>("Adventurer/Mage");
        adventurer.maxHp = 60;
        adventurer.hp = 60;
        adventurer.atk = 20;
        adventurer.def = 1;
        adventurer.speed = 15;
        adventurer.preferenceImgs = new List<Sprite>();
        adventurer.preferenceImgs.Add(Resources.Load<Sprite>("Item/WoodenCane"));
        adventurer.preferenceImgs.Add(Resources.Load<Sprite>("Item/WizardHat"));
        adventurer.itemImgs = new List<Sprite>();
        return adventurer;
    }

    public AdventurerInfo[] GetAdventurerList()
    {
        return _adventurerList;
    }

    public void SetAdventurerList(AdventurerInfo[] a)
    {
        _adventurerList = new AdventurerInfo[a.Length];
        _adventurerList = a;
    }
}
