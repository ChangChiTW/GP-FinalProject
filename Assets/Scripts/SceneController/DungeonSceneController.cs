using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DungeonSceneController : MonoBehaviour
{
    [SerializeField]
    private RawImage _backgroundImage;

    [SerializeField]
    private List<Texture> _backgroundImages = new List<Texture>();

    [SerializeField]
    private GameObject[] _avatars = new GameObject[3];

    [SerializeField]
    private Transform[] _team = new Transform[3];
    private AudioManager _audioManager;
    private StateManager _stateManager;
    private EventController _eventController;
    private AdventurerManager _adventurerManager;
    private AdventurerInfo[] _adventurerList;
    private Vector3 _targetPosition = new Vector3(-5, 0, 0);
    private float _scale = 0.009259259f;
    private float _speed = 4.0f;
    private bool _isArrived = true;
    private int _level = -1;

    void Awake()
    {
        _audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        _eventController = GameObject.Find("GameController").GetComponent<EventController>();
        _adventurerManager = GameObject.Find("GameManager").GetComponent<AdventurerManager>();
    }

    void Update()
    {
        _team[0].position = Vector3.MoveTowards(
            _team[0].position,
            _targetPosition,
            _speed * Time.deltaTime
        );

        // change _team image
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].hp > 0)
            {
                _team[i].GetComponent<SpriteRenderer>().sprite = _adventurerList[i].img;
            }
            else
            {
                _team[i].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
    }

    void Start()
    {
        _audioManager.PlayDungeonBGM();
        int layer = _stateManager.GetLayer() - 1;
        _backgroundImage.texture = _backgroundImages[layer];
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
                _avatars[i].GetComponent<AvatarController>().SetAdventurer(adventurer);
            }
            else
            {
                _avatars[i].SetActive(false);
            }
        }
    }

    public Vector3 GetTeamPosition()
    {
        return _team[0].position;
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
            Vector2 team = new Vector2(_team[0].position.x, _team[0].position.y);
            if (Vector2.Distance(target, team) < 6f && target.x > team.x)
            {
                _audioManager.PlayBtnClick();
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
        yield return new WaitUntil(() => _team[0].position == _targetPosition);
        float[] adventurerHp = new float[_adventurerList.Length];
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            adventurerHp[i] = _adventurerList[i].hp;
        }
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
                        }
                    }
                    else
                    {
                        _adventurerList[i].hp -= monster.atk - _adventurerList[i].def;
                        if (_adventurerList[i].hp <= 0)
                        {
                            _adventurerList[i].hp = 0;
                        }
                        monsterHp -= _adventurerList[i].atk - monster.def;
                        if (monsterHp <= 0)
                        {
                            break;
                        }
                    }
                    _avatars[i]
                        .GetComponent<AvatarController>()
                        .UpdateAdventurer(_adventurerList[i]);
                }
            }
            CheckAnyAdventurerAlive();
        }
        yield return new WaitUntil(() => _eventController.IsEventFinished());
        _stateManager.AddAdventurerBalance(monster.gold);
        _eventController.ShowBattleResult(monster.gold, adventurerHp, _adventurerList);
        yield return new WaitUntil(() => _eventController.IsEventFinished());
        _level++;
        _isArrived = true;
        CheckLastLevel();
    }

    private IEnumerator StartTreasure()
    {
        yield return new WaitUntil(() => _team[0].position == _targetPosition);
        _eventController.ShowEventInfo();
        yield return new WaitUntil(() => _eventController.IsEventFinished());
        _level++;
        _isArrived = true;
        CheckLastLevel();
    }

    private void CheckAnyAdventurerAlive()
    {
        bool isAnyAlive = false;
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].hp > 0)
            {
                isAnyAlive = true;
                break;
            }
        }
        if (!isAnyAlive)
        {
            SceneManager.LoadScene("SettlementScene");
        }
    }

    private void CheckLastLevel()
    {
        if (_level == 9)
        {
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

    public AdventurerInfo GetRandomAdventurer()
    {
        List<AdventurerInfo> list = new List<AdventurerInfo>();
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].hp > 0)
            {
                list.Add(_adventurerList[i]);
            }
        }
        if (list.Count > 0)
        {
            return list[Random.Range(0, list.Count)];
        }
        else
        {
            return null;
        }
    }

    public void DashForward(string adventurer)
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].name == adventurer)
            {
                _adventurerList[i].speed -= 3;
                if (_adventurerList[i].speed < 0)
                {
                    _adventurerList[i].speed = 0;
                }
                _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
                break;
            }
        }
    }

    public void RollBack()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp -= 5;
            if (_adventurerList[i].hp < 0)
            {
                _adventurerList[i].hp = 0;
            }
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void OpenChestWithMonster(Monster monster)
    {
        StartCoroutine(StartBattle(monster));
    }

    public void EatDirectly()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp -= 10;
            _adventurerList[i].atk += 3;
            if (_adventurerList[i].hp < 0)
            {
                _adventurerList[i].hp = 0;
            }
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void MakeIntoDish()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp += 5;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void DontAskForAdvice()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp += 5;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void BuyFireworks()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].atk += 1;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void UseBlackMagicDeckSlaughtered()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp -= 10;
            if (_adventurerList[i].hp < 0)
            {
                _adventurerList[i].hp = 0;
            }
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void UseBlackMagicDeckDefeated()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp -= 5;
            if (_adventurerList[i].hp < 0)
            {
                _adventurerList[i].hp = 0;
            }
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void UseBlackMagicDeckVictory()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].atk += 1;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void AnotherOrder()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].atk += 1;
            _adventurerList[i].speed += 1;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void IdiotGame()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].atk += 1;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void UseEightArtifacts(string adventurer)
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].name == adventurer)
            {
                _adventurerList[i].hp += 10;
                _adventurerList[i].atk += 10;
                _adventurerList[i].speed += 10;
                _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
                break;
            }
        }
    }

    public void TrashEightArtifacts()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].speed += 1;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }

    public void OolongCoveredWithMulletRoe(string adventurer)
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].name == adventurer)
            {
                _adventurerList[i].hp += 3;
                _adventurerList[i].speed += 1;
                _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
                break;
            }
        }
    }

    public void BubbleMilkTea(string adventurer)
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            if (_adventurerList[i].name == adventurer)
            {
                _adventurerList[i].atk += 2;
                _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
                break;
            }
        }
    }

    public void UndergroundHotSprings()
    {
        for (int i = 0; i < _adventurerList.Length; i++)
        {
            _adventurerList[i].hp += 15;
            _avatars[i].GetComponent<AvatarController>().UpdateAdventurer(_adventurerList[i]);
        }
    }
}
