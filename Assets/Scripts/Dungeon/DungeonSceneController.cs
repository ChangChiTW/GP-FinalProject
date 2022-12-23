using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _avatars = new GameObject[3];

    [SerializeField]
    private Transform _team;
    private StateManager _stateManager;
    private AdventurerManager _adventurerManager;
    private AdventurerInfo[] _adventurerList;
    private Vector3 _targetPosition = new Vector3(-5, 0, 0);
    private float _scale = 0.009259259f;
    private float _speed = 4.0f;
    private bool _isArrived = true;
    private int _level = -1;

    void Awake()
    {
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        _adventurerManager = GameObject.Find("GameManager").GetComponent<AdventurerManager>();
    }

    void Update()
    {
        _team.position = Vector3.MoveTowards(
            _team.position,
            _targetPosition,
            _speed * Time.deltaTime
        );
    }

    void Start()
    {
        _adventurerList = _adventurerManager.GetAdventurerList();
        // sort adventurer list by speed
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            for (int j = i + 1; j < _adventurerList.Length; j++)
            {
                if (_adventurerList[i].speed < _adventurerList[j].speed)
                {
                    AdventurerInfo temp = _adventurerList[i];
                    _adventurerList[i] = _adventurerList[j];
                    _adventurerList[j] = temp;
                }
            }
        }
        for (int i = 0; i < _avatars.Length; i++)
        {
            if (i < _adventurerList.Length)
            {
                AdventurerInfo adventurer = _adventurerList[i];
                _avatars[i].SetActive(true);
                _avatars[i].transform.Find("Img").GetComponent<Image>().sprite = adventurer.img;
                _avatars[i].transform.Find("HP").GetComponent<Image>().fillAmount =
                    adventurer.hp / adventurer.maxHp;
            }
            else
            {
                _avatars[i].SetActive(false);
            }
        }
    }

    public Vector3 GetTeamPosition()
    {
        return _team.position;
    }

    public bool IsArrived()
    {
        return _isArrived;
    }

    public float GetScale()
    {
        return _scale;
    }

    public int GetLevel()
    {
        return _level;
    }

    public void OnMoveNextTarget(Vector3 pos, Monster monster)
    {
        if (_isArrived)
        {
            Vector2 target = new Vector2(pos.x * _scale, pos.y * _scale);
            Vector2 team = new Vector2(_team.position.x, _team.position.y);
            if (Vector2.Distance(target, team) < 6f && target.x > team.x)
            {
                _isArrived = false;
                GameObject
                    .Find("Main Camera")
                    .GetComponent<DungeonCameraController>()
                    .FreeCamera(false);
                _targetPosition = new Vector3(target.x, target.y, 0);
                if (monster != null)
                {
                    StartCoroutine(StartBattle(monster));
                }
                else
                {
                    StartCoroutine(StartTreasure());
                }
            }
        }
    }

    private IEnumerator StartBattle(Monster monster)
    {
        yield return new WaitUntil(() => _team.position == _targetPosition);
        float monsterHp = monster.hp;
        while (monsterHp > 0)
        {
            for (int i = 0; i < _adventurerList.Length; i++)
            {
                if (_adventurerList[i].hp > 0)
                {
                    if (_adventurerList[i].speed > monster.speed)
                    {
                        monsterHp -= _adventurerList[i].atk - monster.def;
                        if (monsterHp <= 0)
                        {
                            break;
                        }
                        _adventurerList[i].hp -= monster.atk - _adventurerList[i].def;
                        if (_adventurerList[i].hp <= 0)
                        {
                            _adventurerList[i].hp = 0;
                            _avatars[i].transform.Find("Img").GetComponent<Image>().sprite =
                                Resources.Load<Sprite>("Adventurer/Dead");
                        }
                    }
                    else
                    {
                        _adventurerList[i].hp -= monster.atk - _adventurerList[i].def;
                        if (_adventurerList[i].hp <= 0)
                        {
                            _adventurerList[i].hp = 0;
                            _avatars[i].transform.Find("Img").GetComponent<Image>().sprite =
                                Resources.Load<Sprite>("Adventurer/Dead");
                        }
                        monsterHp -= _adventurerList[i].atk - monster.def;
                        if (monsterHp <= 0)
                        {
                            break;
                        }
                    }
                    _avatars[i].transform.Find("HP").GetComponent<Image>().fillAmount =
                        _adventurerList[i].hp / _adventurerList[i].maxHp;
                }
            }
        }
        _level++;
        _isArrived = true;
        CheckLastLevel();
    }

    private IEnumerator StartTreasure()
    {
        yield return new WaitUntil(() => _team.position == _targetPosition);
        _level++;
        _isArrived = true;
        CheckLastLevel();
    }

    private void CheckLastLevel()
    {
        if (_level == 9)
        {
            Debug.Log("Game Clear");
            List<AdventurerInfo> newList = new List<AdventurerInfo>();
            for (int i = 0; i < _adventurerList.Length; i++)
            {
                if (_adventurerList[i].hp > 0)
                {
                    newList.Add(_adventurerList[i]);
                }
            }
            _adventurerManager.SetAdventurerList(newList.ToArray());
            if (newList.Count > 0)
            {
                SceneManager.LoadScene("TradeScene");
            }
            else
            {
                SceneManager.LoadScene("SettlementScene");
            }
        }
    }
}
