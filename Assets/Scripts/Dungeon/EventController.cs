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

    public void ShowBattleResult(int gold, float[] beforeHp, AdventurerInfo[] after, bool isWin)
    {
        _eventInfo.SetActive(true);
        _battleResultPanel.SetActive(true);
        if (isWin)
        {
            _battleResultPanel.transform.Find("Title").GetComponent<TMP_Text>().text = "Victory!";
            _battleResultPanel.transform.Find("Gold").GetComponent<TMP_Text>().text =
                "Gold +" + gold.ToString();
        }
        else
        {
            _battleResultPanel.transform.Find("Title").GetComponent<TMP_Text>().text = "Defeat!";
            _battleResultPanel.transform.Find("Title").GetComponent<TMP_Text>().color = Color.red;
            _battleResultPanel.transform.Find("Gold").GetComponent<TMP_Text>().text = "Gold +0";
        }
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
            if (after[i].hp == 0)
            {
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("Dead")
                    .gameObject.SetActive(true);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("down")
                    .gameObject.SetActive(false);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("HPBefore")
                    .gameObject.SetActive(false);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("HPAfter")
                    .gameObject.SetActive(false);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("Before")
                    .gameObject.SetActive(false);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("After")
                    .gameObject.SetActive(false);
            }
            else
            {
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("Dead")
                    .gameObject.SetActive(false);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("down")
                    .gameObject.SetActive(true);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("HPBefore")
                    .gameObject.SetActive(true);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("HPAfter")
                    .gameObject.SetActive(true);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("Before")
                    .gameObject.SetActive(true);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("After")
                    .gameObject.SetActive(true);
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("Before")
                    .GetComponent<TMP_Text>()
                    .text = beforeHp[i].ToString("0");
                _battleResultPanel.transform
                    .Find("Adventurer" + i)
                    .Find("After")
                    .GetComponent<TMP_Text>()
                    .text = after[i].hp.ToString("0");
            }
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
        _eventName.GetComponent<TMP_Text>().text = "巨石陷阱";
        _description.GetComponent<TMP_Text>().text =
            "在狹長的道路上" + adventurer.name + "踩到了陷阱，右側石門打開後出現一塊巨石朝著" + adventurer.name + "高速滾動";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "向前衝刺";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                DashForwardResult(adventurer.name);
            });

        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "往後翻滾";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                RollBackResult(adventurer.name);
            });
    }

    private void ObtrusiveTreasureChestEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "突兀的寶箱";
        _description.GetComponent<TMP_Text>().text = "在一個空曠的房間中，一個與周圍格格不入的寶箱顯眼的擺在房間中央";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "打開它";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                OpenChestResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "無視他";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                IgnoreChestResult();
            });
    }

    private void CannedSpinachEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "菠菜罐頭";
        _description.GetComponent<TMP_Text>().text = "撿到一個菠菜罐頭，上面還印著卜派的頭像，該怎麼吃呢?";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "直接吃掉";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                EatDirectlyResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "將其做成菜餚";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                MakeIntoDishResult();
            });
    }

    private void MagicConchEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "神奇海螺";
        _description.GetComponent<TMP_Text>().text = "冒險者們撿到一個海螺";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "問";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                AskForAdviceResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "不問";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                DontAskForAdviceResult();
            });
    }

    private void ReceiveSoaringSharesEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "領取飆股";
        _description.GetComponent<TMP_Text>().text = "出現一個奇怪的老師，聲稱只要買他指定的道具就會發家致富，要跟著老師一起起飛嗎？";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "信他一把";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                TrustTeacherResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "撥打165專線";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                Call165Result();
            });
    }

    private void HappyNewYearEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "新年快樂";
        _description.GetComponent<TMP_Text>().text = "年關將近，來到雜貨店否放一下";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "買刮刮樂";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                BuyScratchCardsResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "買沖天炮";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                BuyFireworksResult();
            });
    }

    private void DuelEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "決鬥";
        _description.GetComponent<TMP_Text>().text = "冒險者們來到一個房間，站在前方 的正是武藤遊戲，武藤遊戲對冒險者發出了決鬥!";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "使用究極大法師牌組";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                UseUltimateMageDeckResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "使用黑魔導牌組";
        _optionB.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                UseBlackMagicDeckResult();
            });
    }

    private void AfricanChiefEvent()
    {
        ShowDescription();
        _eventName.GetComponent<TMP_Text>().text = "非洲酋長";
        _description.GetComponent<TMP_Text>().text = "遇到一台可以玩的拉霸機，但是玩了200次都沒中大獎";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "再來一單";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                AnotherOrderResult();
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "白痴遊戲";
        _optionB.transform
            .Find("Btn")
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
        _eventName.GetComponent<TMP_Text>().text = "八門神器";
        _description.GetComponent<TMP_Text>().text = "這個腳本讓你攻擊速度血量+10，保證無毒，絕對安全";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "我還不用爆";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                UseEightArtifactsResult(adventurer.name);
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "垃圾遊戲";
        _optionB.transform
            .Find("Btn")
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
        _eventName.GetComponent<TMP_Text>().text = "地雷飲料";
        _description.GetComponent<TMP_Text>().text = adventurer.name + "發現一家可愛的飲料店販賣不同的飲料";
        _optionA.transform.Find("Name").GetComponent<TMP_Text>().text = "烏魚子奶蓋烏龍";
        _optionA.transform
            .Find("Btn")
            .GetComponent<Button>()
            .onClick.AddListener(() =>
            {
                OolongCoveredWithMulletRoeResult(adventurer.name);
            });
        _optionB.transform.Find("Name").GetComponent<TMP_Text>().text = "珍珠奶茶";
        _optionB.transform
            .Find("Btn")
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
            "冒險者找到了一處地下溫泉，好好休息了一番，全隊恢復15HP";
        _dungeonSceneController.UndergroundHotSprings();
    }

    private void GoldCoinsFromTheSkyEvent()
    {
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "地牢各處開始下起金幣雨，冒險者購買力大幅提升，+2000g，購買慾望等級+2";
        _stateManager.AddAdventurerBalance(2000);
        _stateManager.AddRaiseRatio(4);
    }

    // Results
    private void DashForwardResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "雖然躲過了巨石，但卻在衝刺時扭傷腳踝，" + adventurer + "速度-3";
        _dungeonSceneController.DashForward(adventurer);
    }

    private void RollBackResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            adventurer + "在驚險地躲過巨石後，與他的隊伍撞成一團，全隊受到5點傷害";
        _dungeonSceneController.RollBack();
    }

    private void OpenChestResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        if (Random.Range(0, 100) < 100)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
                "寶箱裝滿金幣，團隊獲得500金幣";
            _stateManager.AddAdventurerBalance(500);
        }
        else
        {
            Monster monster = _monsterManager.GetRandomMonster();
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
                "箱子裡裝滿了怪物，隊伍遇到了" + monster.name;
            _dungeonSceneController.OpenChestWithMonster(monster);
        }
    }

    private void IgnoreChestResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "無事發生";
    }

    private void EatDirectlyResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "有點過期導致隊伍所有人拉肚子，但也獲得了卜派的祝福，全隊血量-10攻擊力+3";
        _dungeonSceneController.EatDirectly();
    }

    private void MakeIntoDishResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "沒有大口嗑下菠菜少了一個醍醐味，全隊回復5點血量";
        _dungeonSceneController.MakeIntoDish();
    }

    private void AskForAdviceResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "海螺指引冒險者找到一個寶箱，+200g";
        _stateManager.AddAdventurerBalance(200);
    }

    private void DontAskForAdviceResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "海螺發出悠揚旋律，全隊恢復5HP";
        _dungeonSceneController.DontAskForAdvice();
    }

    private void TrustTeacherResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "冒險者的感性佔據上風，決定冒險賭一把，";
        if (Random.Range(0, 100) < 90)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "，-300g，並增加購買欲望等級+1";
            _stateManager.AddAdventurerBalance(-300);
            _stateManager.AddRaiseRatio(2);
        }
        else
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text += "+2000g";
            _stateManager.AddAdventurerBalance(2000);
        }
    }

    private void Call165Result()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "冒險者的理性佔據上風，決定檢舉這個飆股老師，+300g但購買慾望等級-1";
        _stateManager.AddAdventurerBalance(300);
        _stateManager.AddRaiseRatio(-2);
    }

    private void BuyScratchCardsResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "做做公益累積人品，-100g, 購買慾望等級+1";
        _stateManager.AddAdventurerBalance(-100);
        _stateManager.AddRaiseRatio(2);
    }

    private void BuyFireworksResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "這東西連年獸都能嚇跑啊，-200g，全隊攻擊力+1";
        _stateManager.AddAdventurerBalance(-200);
        _dungeonSceneController.BuyFireworks();
    }

    private void UseUltimateMageDeckResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "這是信仰!100%勝利，但這牌組太貴了，只贏得100g";
        _stateManager.AddAdventurerBalance(100);
    }

    private void UseBlackMagicDeckResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "使用遊戲最熟悉的牌組對付遊戲顯然不是明智的選擇";
        int random = Random.Range(0, 100);
        if (random < 30)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "，慘遭遊戲滿血虐殺，全隊HP-10";
            _dungeonSceneController.UseBlackMagicDeckSlaughtered();
        }
        else if (random < 80)
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "，被遊戲輕鬆贏得勝利，全隊HP-5";
            _dungeonSceneController.UseBlackMagicDeckDefeated();
        }
        else
        {
            _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text +=
                "，奇蹟似的打倒遊戲，+1000g，全隊攻擊+1";
            _stateManager.AddAdventurerBalance(1000);
            _dungeonSceneController.UseBlackMagicDeckVictory();
        }
    }

    private void AnotherOrderResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "-100g，又花了100g，但還是慘賠，全隊攻擊+1，全隊速度+1";
        _stateManager.AddAdventurerBalance(-200);
        _dungeonSceneController.AnotherOrder();
    }

    private void IdiotGameResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "-100g，全隊攻擊+1";
        _stateManager.AddAdventurerBalance(-100);
        _dungeonSceneController.IdiotGame();
    }

    private void UseEightArtifactsResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            adventurer + "攻擊速度血量+10，隨機輪後被偵測到失去所有金錢";
        _dungeonSceneController.UseEightArtifacts(adventurer);
        _stateManager.AddAdventurerBalance(-_stateManager.GetAdventurerBalance());
    }

    private void TrashEightArtifactsResult()
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "遊戲設計者感謝您的善良，全隊速度+1";
        _dungeonSceneController.TrashEightArtifacts();
    }

    private void OolongCoveredWithMulletRoeResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text = "意外的很好喝，+3血量+1速度";
        _dungeonSceneController.OolongCoveredWithMulletRoe(adventurer);
    }

    private void BubbleMilkTeaResult(string adventurer)
    {
        CloseDescription();
        _eventInfoPanel.SetActive(true);
        _eventInfoPanel.transform.Find("Des").GetComponent<TMP_Text>().text =
            "基本款竟然這麼雷，拳頭都硬了，" + adventurer + "攻擊+2";
        _dungeonSceneController.BubbleMilkTea(adventurer);
    }
}
