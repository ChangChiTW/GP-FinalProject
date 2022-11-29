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
    public int hp;
    public int atk;
    public int def;
    public ItemInfo[] items;
}

public class AdventurerManager : MonoBehaviour
{
    private AdventurerInfo[] _adventurerList = new AdventurerInfo[3];

    void Start()
    {
        if (GameObject.Find("AdventurerManager") != null)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        randomAdventurerList();
    }

    private void randomAdventurerList()
    {
        for (int i = 0; i < 3; i++)
        {
            _adventurerList[i] = new AdventurerInfo();
            _adventurerList[i].name = "Adventurer " + i;
            _adventurerList[i].hp = Random.Range(1, 100);
            _adventurerList[i].atk = Random.Range(1, 100);
            _adventurerList[i].def = Random.Range(1, 100);
            _adventurerList[i].items = new ItemInfo[3];
            for (int j = 0; j < 3; j++)
            {
                _adventurerList[i].items[j] = new ItemInfo();
                _adventurerList[i].items[j].name = "Item " + j;
                _adventurerList[i].items[j].cost = Random.Range(1, 100);
                _adventurerList[i].items[j].basePrice = Random.Range(1, 100);
            }
        }
    }

    public AdventurerInfo[] GetAdventurerList()
    {
        return _adventurerList;
    }
}
