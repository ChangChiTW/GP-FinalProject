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

    public void ShowBattleResult(int gold, float[] beforeHp, AdventurerInfo[] after)
    {
        _eventInfo.SetActive(true);
        _battleResultPanel.SetActive(true);
        _battleResultPanel.transform.Find("Gold").GetComponent<TMP_Text>().text =
            "Gold +" + gold.ToString();
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
        int eventIndex = Random.Range(0, 12);
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
            case 3:
                MagicConchEvent();
                break;
            case 4:
                ReceiveSoaringSharesEvent();
                break;
            case 5:
                HappyNewYearEvent();
                break;
            case 6:
                DuelEvent();
                break;
            case 7:
                AfricanChiefEvent();
                break;
            case 8:
                EightArtifactsEvent();
                break;
            case 9:
                MiniDrinkEvent();
                break;
            case 10:
                UndergroundHotSpringsEvent();
                break;
            case 11:
                GoldCoinsFromTheSkyEvent();
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

    private void ShowDescription()
    {
        _eventName.SetActive(true);
        _description.SetActive(true);
        _optionA.SetActive(true);
        _optionB.SetActive(true);
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
        ShowDescription();
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
        ShowDescription();
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
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Canned Spinach";
        _description.GetComponent<TMP_Text>().text =
            "Adventurers picked up a can of spinach with Bu Pai's face printed on it, how should they eat it?";
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

    private void MagicConchEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Magic Conch";
        _description.GetComponent<TMP_Text>().text = "Adventurers picked up a conch";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Ask for advice";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                AskForAdviceResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Don't ask for advice";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                DontAskForAdviceResult();
            });
    }

    private void ReceiveSoaringSharesEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Receive Soaring Shares";
        _description.GetComponent<TMP_Text>().text =
            "A strange teacher appeared, claiming that as long as you buy the props specified by him, you will get rich. Do you want to take off with the teacher?";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Trust the teacher";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                TrustTeacherResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Call 165";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                Call165Result();
            });
    }

    private void HappyNewYearEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Happy New Year";
        _description.GetComponent<TMP_Text>().text =
            "The end of the year is approaching, come to the grocery store for fun.";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Buy scratch cards";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                BuyScratchCardsResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Buy fireworks";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                BuyFireworksResult();
            });
    }

    private void DuelEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Duel";
        _description.GetComponent<TMP_Text>().text =
            "The adventurers came to a room, and it was Muto Yuki standing in front of him, and Muto Yuki issued a duel to the adventurers!";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text =
            "Use the Ultimate Mage deck";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                UseUltimateMageDeckResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Use the Black Magic Deck";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                UseBlackMagicDeckResult();
            });
    }

    private void AfricanChiefEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "African Chief";
        _description.GetComponent<TMP_Text>().text =
            "TThe adventurer encountered a slot machine that could be played, but he failed to win the jackpot after playing 200 times";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Another order";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                AnotherOrderResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Idiot game";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                IdiotGameResult();
            });
    }

    private void EightArtifactsEvent()
    {
        AdventurerInfo adventurer = _dungeonSceneController.GetRandomAdventurer();
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Eight Artifacts";
        _description.GetComponent<TMP_Text>().text =
            "This script gives you +10 attack speed and blood volume, which is guaranteed to be non-toxic and absolutely safe.";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "Use it";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                UseEightArtifactsResult(adventurer.name);
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Trash it";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                TrashEightArtifactsResult();
            });
    }

    private void MiniDrinkEvent()
    {
        AdventurerInfo adventurer = _dungeonSceneController.GetRandomAdventurer();
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "Mini Drink";
        _description.GetComponent<TMP_Text>().text =
            adventurer.name + "found a lovely drink shop selling different drinks";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text =
            "Oolong covered with mullet roe";
        _optionA
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                OolongCoveredWithMulletRoeResult(adventurer.name);
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "Bubble milk tea";
        _optionB
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                BubbleMilkTeaResult(adventurer.name);
            });
    }

    private void UndergroundHotSpringsEvent()
    {
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "The adventurers found an underground hot spring, took a good rest, and the whole team recovered 15HP";
        _dungeonSceneController.UndergroundHotSprings();
    }

    private void GoldCoinsFromTheSkyEvent()
    {
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Gold coins began to rain everywhere in the dungeon, and the purchasing power of adventurers increased significantly, +2000g, the desire to buy level +2";
        _stateManager.AddAdventurerBalance(2000);
        _stateManager.AddRaiseRatio(4);
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
            _stateManager.AddAdventurerBalance(500);
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

    private void AskForAdviceResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "The conch guides the adventurer to a treasure chest, +200g";
        _stateManager.AddAdventurerBalance(200);
    }

    private void DontAskForAdviceResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "The conch makes a melodious melody, and the whole team recovers 5 HP";
        _dungeonSceneController.DontAskForAdvice();
    }

    private void TrustTeacherResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "The sensibility of adventurers prevailed, and they decided to take a gamble. ";
        if (Random.Range(0, 100) < 90)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "The teacher was a fraud, and the team lost 300 gold coins, and the desire to buy level +1";
            _stateManager.AddAdventurerBalance(-300);
            _stateManager.AddRaiseRatio(2);
        }
        else
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "The teacher is a fraud, but the adventurers are lucky enough to find the teacher's secret stash, and the team gains 2000 gold coins";
            _stateManager.AddAdventurerBalance(2000);
        }
    }

    private void Call165Result()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "The rationality of the risk taker prevailed, and he decided to report this stock market teacher, +300g but the desire to buy level -1";
        _stateManager.AddAdventurerBalance(300);
        _stateManager.AddRaiseRatio(-2);
    }

    private void BuyScratchCardsResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Doing public welfare to accumulate character, -100g but the desire to buy level +1";
        _stateManager.AddAdventurerBalance(-100);
        _stateManager.AddRaiseRatio(2);
    }

    private void BuyFireworksResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "This thing can scare even Nian Beasts away, -200g, the attack power of the whole team +1";
        _stateManager.AddAdventurerBalance(-200);
        _dungeonSceneController.BuyFireworks();
    }

    private void UseUltimateMageDeckResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "It's faith! 100% wins but this deck is too expensive and only wins 200g";
        _stateManager.AddAdventurerBalance(200);
    }

    private void UseBlackMagicDeckResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "It is obviously not a wise choice to use the most familiar deck of the game against the game. ";
        int random = Random.Range(0, 100);
        if (random < 30)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "The whole team was brutally slaughtered with full blood HP-10";
            _dungeonSceneController.UseBlackMagicDeckSlaughtered();
        }
        else if (random < 80)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "Easily defeated, all team HP-5";
            _dungeonSceneController.UseBlackMagicDeckDefeated();
        }
        else
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "Miraculously won the victory, +1000g, team attack +1";
            _stateManager.AddAdventurerBalance(1000);
            _dungeonSceneController.UseBlackMagicDeckVictory();
        }
    }

    private void AnotherOrderResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "-400g, spent another 400g, but still lost, team attack +1, team speed +1";
        _stateManager.AddAdventurerBalance(-800);
        _dungeonSceneController.AnotherOrder();
    }

    private void IdiotGameResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "-400g, team attack +1";
        _stateManager.AddAdventurerBalance(-400);
        _dungeonSceneController.IdiotGame();
    }

    private void UseEightArtifactsResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Attack speed HP +10, lose all money if detected after a random round";
        _dungeonSceneController.UseEightArtifacts(adventurer);
        _stateManager.AddAdventurerBalance(-_stateManager.GetAdventurerBalance());
    }

    private void TrashEightArtifactsResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Game designer thanks for your kindness.";
        _dungeonSceneController.TrashEightArtifacts();
    }

    private void OolongCoveredWithMulletRoeResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "Surprisingly delicious, +3 health +1 speed";
        _dungeonSceneController.OolongCoveredWithMulletRoe(adventurer);
    }

    private void BubbleMilkTeaResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "The basic model is so bad, the fist is hard, attack +2";
        _dungeonSceneController.BubbleMilkTea(adventurer);
    }
}
