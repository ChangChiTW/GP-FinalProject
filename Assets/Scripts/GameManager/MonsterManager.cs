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
    public Sprite img;

    public Monster(string name, float hp, float atk, float def, int speed, int gold, Sprite img)
    {
        this.name = name;
        this.hp = hp;
        this.atk = atk;
        this.def = def;
        this.speed = speed;
        this.gold = gold;
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
            20 * _ratio,
            5 * _ratio,
            0,
            12,
            200,
            Resources.Load<Sprite>("Monster/Slime")
        );
    }

    public Monster NewReptilian()
    {
        return new Monster(
            "Reptilian",
            30 * _ratio,
            9 * _ratio,
            1,
            12,
            300,
            Resources.Load<Sprite>("Monster/Reptilian")
        );
    }

    public Monster NewGoblin()
    {
        return new Monster(
            "Goblin",
            15 * _ratio,
            7 * _ratio,
            0,
            18,
            250,
            Resources.Load<Sprite>("Monster/Goblin")
        );
    }

    public Monster NewMinotaur()
    {
        return new Monster(
            "Minotaur",
            50 * _ratio,
            11 * _ratio,
            2,
            9,
            400,
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
}
