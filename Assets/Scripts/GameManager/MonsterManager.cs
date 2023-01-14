using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster
{
    public string name;
    public float hp;
    public float atk;
    public float def;
    public int speed;
    public int gold;
    public int exp;
    public Sprite img;

    public Monster(
        string name,
        float hp,
        float atk,
        float def,
        int speed,
        int gold,
        int exp,
        Sprite img
    )
    {
        this.name = name;
        this.hp = hp;
        this.atk = atk;
        this.def = def;
        this.speed = speed;
        this.gold = gold;
        this.exp = exp;
        this.img = img;
    }
}

public class MonsterManager : MonoBehaviour
{
    private StateManager _stateManager;
    private Monster[] _monsterList = new Monster[4];
    private float _ratio;

    void Awake()
    {
        _ratio =
            GameObject.Find("GameManager").GetComponent<StateManager>().GetMonsterRatio() * 0.01f;
    }

    void Start()
    {
        initMonsterList();
    }

    public void initMonsterList()
    {
        _monsterList = new Monster[4];
        _monsterList[0] = NewSlime();
        _monsterList[1] = NewReptilian();
        _monsterList[2] = NewGoblin();
        _monsterList[3] = NewMinotaur();
    }

    public Monster NewSlime()
    {
        return new Monster(
            "Slime",
            150 * _ratio / 2,
            5 * _ratio,
            0,
            12,
            100,
            1,
            Resources.Load<Sprite>("Monster/Slime")
        );
    }

    public Monster NewReptilian()
    {
        return new Monster(
            "Reptilian",
            200 * _ratio / 2,
            9 * _ratio,
            1,
            12,
            150,
            2,
            Resources.Load<Sprite>("Monster/Reptilian")
        );
    }

    public Monster NewGoblin()
    {
        return new Monster(
            "Goblin",
            100 * _ratio / 2,
            7 * _ratio,
            0,
            18,
            125,
            2,
            Resources.Load<Sprite>("Monster/Goblin")
        );
    }

    public Monster NewMinotaur()
    {
        return new Monster(
            "Minotaur",
            500 * _ratio / 2,
            10 * _ratio,
            1,
            9,
            500,
            3,
            Resources.Load<Sprite>("Monster/Minotaur")
        );
    }

    public Monster GetMonster(int index)
    {
        return _monsterList[index];
    }

    public Monster[] GetMonsterList()
    {
        return _monsterList;
    }

    public Monster GetRandomMonster()
    {
        int random = Random.Range(0, 4);
        return _monsterList[random];
    }
}
