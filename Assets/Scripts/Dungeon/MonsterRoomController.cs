using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

public class MonsterRoomController : MonoBehaviour
{
    [SerializeField]
    private GameObject _monsterImg;

    [SerializeField]
    private GameObject _info;

    [SerializeField]
    private TMP_Text _nameText;

    [SerializeField]
    private TMP_Text _hpText;

    [SerializeField]
    private TMP_Text _atkText;

    [SerializeField]
    private TMP_Text _defText;

    [SerializeField]
    private TMP_Text _speedText;

    [SerializeField]
    private TMP_Text _goldText;
    private StateManager _stateManager;
    private DungeonSceneController _dungeonSceneController;
    private Monster _monster;
    private float _ratio;

    void Awake()
    {
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        _ratio = _stateManager.GetMonsterRatio() * 0.01f;
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
        _nameText.text = _monster.name;
        _hpText.text = _monster.hp.ToString();
        _atkText.text = _monster.atk.ToString();
        _defText.text = _monster.def.ToString();
        _speedText.text = _monster.speed.ToString();
        _goldText.text = _monster.gold.ToString();
    }

    private Monster NewSlime()
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

    private Monster NewReptilian()
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

    private Monster NewGoblin()
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

    private Monster NewMinotaur()
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

    public void OnClick()
    {
        _dungeonSceneController.OnMoveNextTarget(
            this.GetComponent<RectTransform>().localPosition,
            _monster
        );
    }

    public void OnCheckMonsterInfo()
    {
        _info.SetActive(true);
    }

    public void OnCloseMonsterInfo()
    {
        _info.SetActive(false);
    }
}