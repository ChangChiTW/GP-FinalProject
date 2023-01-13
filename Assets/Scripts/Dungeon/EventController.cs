using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    [SerializeField]
    private GameObject _eventInfo;

    [SerializeField]
    private GameObject _eventName;

    [SerializeField]
    private GameObject _description;

    [SerializeField]
    private GameObject _optionA;

    [SerializeField]
    private GameObject _optionB;

    [SerializeField]
    private GameObject _eventInfoPanel;

    [SerializeField]
    private GameObject _battleResultPanel;
    private StateManager _stateManager;
    private DungeonSceneController _dungeonSceneController;
    private MonsterManager _monsterManager;

    void Awake()
    {
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        _dungeonSceneController = GameObject
            .Find("GameController")
            .GetComponent<DungeonSceneController>();
        _monsterManager = GameObject.Find("GameManager").GetComponent<MonsterManager>();
    }

    void Start() { }

    public bool IsEventFinished()
    {
        return !_eventInfo.activeSelf;
    }

    public void ShowBattleResult(float[] beforeHp, AdventurerInfo[] after)
    {
        _eventInfo.SetActive(true);
        _battleResultPanel.SetActive(true);
        for (int i = 0; i < after.Length; i++)
        {
            _battleResultPanel.transform
                .Find("Adventurer" + i)
                .Find("Name")
                .GetComponent<TMP_Text>()
                .text = after[i].name;
            _battleResultPanel.transform
                .Find("Adventurer" + i)
                .Find("Img")
                .GetComponent<Image>()
                .sprite = after[i].img;
            _battleResultPanel.transform
                .Find("Adventurer" + i)
                .Find("Before")
                .GetComponent<TMP_Text>()
                .text = beforeHp[i].ToString();
            _battleResultPanel.transform
                .Find("Adventurer" + i)
                .Find("After")
                .GetComponent<TMP_Text>()
                .text = after[i].hp.ToString();
        }
    }

    public void ShowEventInfo()
    {
        _eventInfo.SetActive(true);
        _eventName.SetActive(true);
        _description.SetActive(true);
        _optionA.SetActive(true);
        _optionB.SetActive(true);
        int eventIndex = Random.Range(0, 3);
        switch (eventIndex)
        {
            case 0:
                BoulderTrapEvent();
                break;
            case 1:
                ObtrusiveTreasureChestEvent();
                break;
            case 2:
                CannedSpinachEvent();
                break;
        }
    }

    public void CloseEventInfo()
    {
        _eventInfo.SetActive(false);
        CloseDescription();
        _eventInfoPanel.SetActive(false);
        _battleResultPanel.SetActive(false);
    }

    private void CloseDescription()
    {
        _eventName.SetActive(false);
        _description.SetActive(false);
        _optionA.SetActive(false);
        _optionB.SetActive(false);
    }

    // Events
    private void BoulderTrapEvent()
    {
        AdventurerInfo adventurer = _dungeonSceneController.GetRandomAdventurer();
        _eventName.GetComponent<TMP_Text>().text = "Boulder Trap";
        _description.GetComponent<TMP_Text>().text =
            adventurer.name
            + " stepped on a trap on the long and narrow road, and after the stone door on the right opened, a huge boulder rolled towards the "
            + adventurer.name
            + " at high speed";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Dash forward";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                DashForwardResult(adventurer.name);
            });

        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Roll back";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                RollBackResult(adventurer.name);
            });
    }

    private void ObtrusiveTreasureChestEvent()
    {
        _eventName.GetComponent<TMP_Text>().text = "Obtrusive Treasure Chest";
        _description.GetComponent<TMP_Text>().text =
            "In an empty room, a treasure chest out of place with the surroundings is conspicuously placed in the center of the room";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Open the chest";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                OpenChestResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Ignore the chest";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                IgnoreChestResult();
            });
    }

    private void CannedSpinachEvent()
    {
        _eventName.GetComponent<TMP_Text>().text = "Canned Spinach";
        _description.GetComponent<TMP_Text>().text =
            "Picked up a can of spinach with Bu Pai's face printed on it, how to eat it?";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Eat directly";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                EatDirectlyResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Make it into a dish";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                MakeIntoDishResult();
            });
    }

    // Results
    private void DashForwardResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Although dodged the boulder, but sprained his ankle while sprinting, "
            + adventurer
            + "'s speed is permanently -3";
        _dungeonSceneController.DashForward(adventurer);
    }

    private void RollBackResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(false);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            adventurer
            + " collides with his team after narrowly dodging the boulder, and the team takes 5 damage";
        _dungeonSceneController.RollBack();
    }

    private void OpenChestResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        if (Random.Range(0, 100) < 25)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
                "The chest is filled with gold coins, and the team gains 500 gold coins";
            _dungeonSceneController.OpenChestWithCoins();
        }
        else
        {
            Monster monster = _monsterManager.GetRandomMonster();
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
                "The chest is filled with a monster, and the team encounters a " + monster.name;
            _dungeonSceneController.OpenChestWithMonster(monster);
        }
    }

    private void IgnoreChestResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "Nothing happened";
    }

    private void EatDirectlyResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "A bit expired, causing everyone in the team to have diarrhea, but also received the blessing of Bu Pai, the team's hp -10 attack power +3";
        _dungeonSceneController.EatDirectly();
    }

    private void MakeIntoDishResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Didn't take a big gulp of spinach, one less taste, the whole team recovered 5 points of HP";
        _dungeonSceneController.MakeIntoDish();
    }
}
