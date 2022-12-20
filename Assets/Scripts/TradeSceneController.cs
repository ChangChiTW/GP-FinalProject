using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TradeSceneController : MonoBehaviour
{
    public Inventory myBag;
    [SerializeField]
    private Image _adventurerImg;

    [SerializeField]
    private Text _adventurerName;

    [SerializeField]
    private Text _adventurerHp;

    [SerializeField]
    private Text _adventurerAtk;

    [SerializeField]
    private Text _adventurerDef;

    [SerializeField]
    private List<Image> _preferenceImages;

    [SerializeField]
    private List<Image> _adventurerImages;

    [SerializeField]
    private GameObject _shopPanel;

    private StateManager _stateManager;
    private AdventurerManager _adventurerManager;

    private AdventurerInfo[] _adventurerList;
    private int _adventurerIndex = 0;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        _adventurerManager = GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>();
    }

    void Start()
    {
        if (_stateManager.GetLayer() != 0)
        {
            _shopPanel.SetActive(false);
        }
        _adventurerList = _adventurerManager.GetAdventurerList();
        ShowAdventurerInfo(0);
    }

    private void ShowAdventurerInfo(int index)
    {
        _adventurerName.text = _adventurerList[index].name;
        _adventurerImg.GetComponent<Image>().sprite = _adventurerList[index].img;
        _adventurerHp.text = "HP:    " + _adventurerList[index].hp.ToString("0");
        _adventurerAtk.text = "ATK:  	" + _adventurerList[index].atk.ToString();
        _adventurerDef.text = "DEF:   " + _adventurerList[index].def.ToString();
        for (int i = 0; i < _preferenceImages.Count; i++)
        {
            if (i < _adventurerList[index].preferenceImgs.Count)
            {
                _preferenceImages[i].sprite = _adventurerList[index].preferenceImgs[i];
                _preferenceImages[i].enabled = true;
            }
            else
            {
                _preferenceImages[i].sprite = null;
                _preferenceImages[i].enabled = false;
            }
        }
        for (int i = 0; i < _adventurerImages.Count; i++)
        {
            if (i < _adventurerList[index].itemImgs.Count)
            {
                _adventurerImages[i].sprite = _adventurerList[index].itemImgs[i];
                _adventurerImages[i].enabled = true;
            }
            else
            {
                _adventurerImages[i].sprite = null;
                _adventurerImages[i].enabled = false;
            }
        }
    }

    public void OnNextAdventure()
    {
        _adventurerIndex++;
        if (_adventurerIndex >= _adventurerList.Length)
        {
            _adventurerIndex = 0;
        }
        ShowAdventurerInfo(_adventurerIndex);
    }

    public void OnPrevAdventure()
    {
        _adventurerIndex--;
        if (_adventurerIndex < 0)
        {
            _adventurerIndex = _adventurerList.Length - 1;
        }
        ShowAdventurerInfo(_adventurerIndex);
    }

    public void OnCheckFloorInfo()
    {
        _stateManager.SetLastSceneToStageBookScene("TradeScene");
        _stateManager.SetStageBookPage(0);
        SceneManager.LoadScene("StageBookScene");
    }

    public void OnBackToShop()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void OnGoDungeon()
    {
        _stateManager.AddLayer();
        if (_stateManager.GetLayer() > 3 || myBag.itemList.Count == 0)
        {
            SceneManager.LoadScene("SettlementScene");
        }
        else
        {
            SceneManager.LoadScene("DungeonRunScene");
        }
    }

    private void AdjustAdventurerInfo(float hp, float atk, float def, Sprite img)
    {
        if (_adventurerList[_adventurerIndex].itemImgs.Count < 6)
        {
            _adventurerList[_adventurerIndex].hp += hp;
            _adventurerList[_adventurerIndex].atk += atk;
            _adventurerList[_adventurerIndex].def += def;
            _adventurerList[_adventurerIndex].itemImgs.Add(img);
            _adventurerManager.SetAdventurerList(_adventurerList);
            ShowAdventurerInfo(_adventurerIndex);
        }
    }

    public void SellToAdventurer()
    {
        Item item = TradeManager.GetChosenItem();
        if (_adventurerList[_adventurerIndex].preferenceImgs.Contains(item.itemImage))
        {
            AdjustAdventurerInfo(item.HP * 1.1f, item.ATK * 1.1f, item.DEF * 1.1f, item.itemImage);
        }
        else
        {
            AdjustAdventurerInfo(item.HP, item.ATK, item.DEF, item.itemImage);
        }
        TradeManager.AddNewItem();
        TradeManager.CloseDes();
    }

    public void playButtonSE()
    {
        _stateManager.playButtonSE();
    }

    public void playFlipBookSE()
    {
        _stateManager.playFlipBookSE();
    }
}
