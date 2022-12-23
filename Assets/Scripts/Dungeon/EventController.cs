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
    private List<GameObject> _eventButtons = new List<GameObject>();

    [SerializeField]
    private GameObject _eventInfoPanel;
    private StateManager _stateManager;
    private DungeonSceneController _dungeonSceneController;

    void Awake()
    {
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
        _dungeonSceneController = GameObject
            .Find("GameController")
            .GetComponent<DungeonSceneController>();
    }

    public void ShowEventInfo()
    {
        _eventInfo.SetActive(true);
        if (Random.Range(0, 100) < 25)
        {
            int eventIndex = Random.Range(0, 3);
            switch (eventIndex)
            {
                case 0:
                    MonsterEvent();
                    break;
                case 1:
                    TrapEvent();
                    break;
                case 2:
                    PoisonEvent();
                    break;
                default:
                    MonsterEvent();
                    break;
            }
        }
        else
        {
            List<int> eventList = Enumerable.Range(0, 4).ToList();
            for (int i = 0; i < _eventButtons.Count; i++)
            {
                int eventIndex = Random.Range(0, eventList.Count);
                switch (eventList[eventIndex])
                {
                    case 0:
                        HotSpringEvent(_eventButtons[i]);
                        break;
                    case 1:
                        TreasureEvent(_eventButtons[i]);
                        break;
                    case 2:
                        SpinachEvent(_eventButtons[i]);
                        break;
                    case 3:
                        PotionEvent(_eventButtons[i]);
                        break;
                    default:
                        HotSpringEvent(_eventButtons[i]);
                        break;
                }
                eventList.RemoveAt(eventIndex);
            }
        }
    }

    public void CloseEventInfo()
    {
        _eventInfo.SetActive(false);
        _eventInfoPanel.SetActive(false);
        foreach (GameObject btn in _eventButtons)
        {
            btn.SetActive(false);
        }
    }

    public bool IsEventFinished()
    {
        return !_eventInfo.activeSelf;
    }

    // Event to be called when the player click on the event button (good event)
    private void HotSpringEvent(GameObject btn)
    {
        btn.SetActive(true);
        btn.transform.Find("Name").GetComponent<TMP_Text>().text = "Hot Spring";
        btn.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a hot spring. Everyone recover 20 HP.";
        btn.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                _dungeonSceneController.AdventurerAddHp(20);
                CloseEventInfo();
            });
    }

    private void TreasureEvent(GameObject btn)
    {
        btn.SetActive(true);
        btn.transform.Find("Name").GetComponent<TMP_Text>().text = "Treasure";
        btn.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a treasure. Everyone gain 30 gold.";
        // btn.GetComponent<Button>().onClick.AddListener(() =>
        // {
        //     _dungeonSceneController.TreasureEffect();
        //     CloseEventInfo();
        // });
    }

    private void SpinachEvent(GameObject btn)
    {
        btn.SetActive(true);
        btn.transform.Find("Name").GetComponent<TMP_Text>().text = "Spinach";
        btn.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a spinach. Everyone gain 5 ATK.";
        btn.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                _dungeonSceneController.AdventurerAddAtk(5);
                CloseEventInfo();
            });
    }

    private void PotionEvent(GameObject btn)
    {
        btn.SetActive(true);
        btn.transform.Find("Name").GetComponent<TMP_Text>().text = "Potion";
        btn.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a potion. Everyone gain 5 DEF.";
        btn.GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                _dungeonSceneController.AdventurerAddDef(5);
                CloseEventInfo();
            });
    }

    // Event to be called when the player on the treasure room (bad event)
    private void MonsterEvent()
    {
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Name").GetComponent<TMP_Text>().text = "Monster";
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a monster. Everyone lose 10 HP.";
        _dungeonSceneController.AdventurerAddHp(-10);
    }

    private void TrapEvent()
    {
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Name").GetComponent<TMP_Text>().text = "Trap";
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a trap. Everyone lose 15 gold.";
        // _dungeonSceneController.TrapEffect();
    }

    private void PoisonEvent()
    {
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Name").GetComponent<TMP_Text>().text = "Poison";
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Adventurer found a poison. Everyone lose 2 ATK.";
        _dungeonSceneController.AdventurerAddAtk(-2);
    }
}
