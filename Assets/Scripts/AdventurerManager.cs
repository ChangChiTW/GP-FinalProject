using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerInfo
{
    public string name;
    public string job;
    public Sprite img;
    public float hp;
    public int atk;
    public int def;
    public int speed;
    public List<Sprite> preferenceImgs;
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

    public void initAdventurerList()
    {
        _adventurerList = new AdventurerInfo[3];
        _adventurerList[0] = new AdventurerInfo();
        _adventurerList[0].name = "Knight";
        _adventurerList[0].job = "Knight";
        _adventurerList[0].img = Resources.Load<Sprite>("Adventurer/Knight");
        _adventurerList[0].hp = 90;
        _adventurerList[0].atk = 13;
        _adventurerList[0].def = 3;
        _adventurerList[0].speed = 10;
        _adventurerList[0].preferenceImgs = new List<Sprite>();
        _adventurerList[0].preferenceImgs.Add(Resources.Load<Sprite>("Item/IronSword"));
        _adventurerList[0].preferenceImgs.Add(Resources.Load<Sprite>("Item/GoldenSword"));
        _adventurerList[0].preferenceImgs.Add(Resources.Load<Sprite>("Item/BarrelLid"));
        _adventurerList[0].itemImgs = new List<Sprite>();
        _adventurerList[1] = new AdventurerInfo();
        _adventurerList[1].name = "Archer";
        _adventurerList[1].job = "Archer";
        _adventurerList[1].img = Resources.Load<Sprite>("Adventurer/Archer");
        _adventurerList[1].hp = 70;
        _adventurerList[1].atk = 16;
        _adventurerList[1].def = 2;
        _adventurerList[1].speed = 20;
        _adventurerList[1].preferenceImgs = new List<Sprite>();
        _adventurerList[1].preferenceImgs.Add(Resources.Load<Sprite>("Item/GoldenBow"));
        _adventurerList[1].preferenceImgs.Add(Resources.Load<Sprite>("Item/IronArrow"));
        _adventurerList[1].preferenceImgs.Add(Resources.Load<Sprite>("Item/CottonHood"));
        _adventurerList[1].itemImgs = new List<Sprite>();
        _adventurerList[2] = new AdventurerInfo();
        _adventurerList[2].name = "Mage";
        _adventurerList[2].job = "Mage";
        _adventurerList[2].img = Resources.Load<Sprite>("Adventurer/Mage");
        _adventurerList[2].hp = 60;
        _adventurerList[2].atk = 20;
        _adventurerList[2].def = 1;
        _adventurerList[2].speed = 15;
        _adventurerList[2].preferenceImgs = new List<Sprite>();
        _adventurerList[2].preferenceImgs.Add(Resources.Load<Sprite>("Item/WoodenCane"));
        _adventurerList[2].preferenceImgs.Add(Resources.Load<Sprite>("Item/WizardHat"));
        _adventurerList[2].itemImgs = new List<Sprite>();
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
