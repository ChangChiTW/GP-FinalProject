using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
    private MonsterManager _monsterManager;
    private DungeonSceneController _dungeonSceneController;
    private Monster _monster;

    void Awake()
    {
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        _monsterManager = GameObject.Find("GameManager").GetComponent<MonsterManager>();
        _dungeonSceneController = GameObject
            .Find("GameController")
            .GetComponent<DungeonSceneController>();
        int random = Random.Range(0, 4);
        Monster[] monsterList = _monsterManager.GetMonsterList();
        _monster = monsterList[random];
        _monsterImg.GetComponent<Image>().sprite = _monster.img;
    }

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => OnClick());
        _nameText.text = _monster.name;
        _hpText.text = _monster.hp.ToString("0");
        _atkText.text = _monster.atk.ToString();
        _defText.text = _monster.def.ToString();
        _speedText.text = _monster.speed.ToString();
        _goldText.text = _monster.gold.ToString();
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
