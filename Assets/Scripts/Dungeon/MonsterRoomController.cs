using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster
{
    public string name;
    public int hp;
    public int atk;
    public int def;
    public int speed;
    public int gold;
    public Sprite img;

    public Monster(string name, int hp, int atk, int def, int speed, int gold, Sprite img)
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

public class MonsterRoomController : MonoBehaviour
{
    [SerializeField]
    private GameObject _monsterImg;
    private DungeonSceneController _dungeonSceneController;
    private Monster _monster;

    void Awake()
    {
        _dungeonSceneController = GameObject
            .Find("GameController")
            .GetComponent<DungeonSceneController>();
        int random = Random.Range(0, 4);
        switch (random)
        {
            case 0:
                _monster = NewSlime();
                break;
            case 1:
                _monster = NewReptilian();
                break;
            case 2:
                _monster = NewGoblin();
                break;
            case 3:
                _monster = NewMinotaur();
                break;
        }
        _monsterImg.GetComponent<Image>().sprite = _monster.img;
    }

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
    }

    private Monster NewSlime()
    {
        return new Monster("Slime", 20, 5, 0, 12, 200, Resources.Load<Sprite>("Monster/Slime"));
    }

    private Monster NewReptilian()
    {
        return new Monster(
            "Reptilian",
            30,
            9,
            1,
            12,
            300,
            Resources.Load<Sprite>("Monster/Reptilian")
        );
    }

    private Monster NewGoblin()
    {
        return new Monster("Goblin", 15, 7, 0, 18, 250, Resources.Load<Sprite>("Monster/Goblin"));
    }

    private Monster NewMinotaur()
    {
        return new Monster(
            "Minotaur",
            50,
            11,
            2,
            9,
            400,
            Resources.Load<Sprite>("Monster/Minotaur")
        );
    }

    public void OnClick()
    {
        _dungeonSceneController.OnMoveNextTarget(
            this.GetComponent<RectTransform>().localPosition,
            _monster
        );
    }
}
