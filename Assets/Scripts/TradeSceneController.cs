using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TradeSceneController : MonoBehaviour
{
    private StateManager _stateManager;
    private AdventurerManager _adventurerManager;

    private AdventurerInfo[] _adventurerList;
    private int _adventurerIndex = 0;

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
    private List<Image> _adventurerImages;

    [SerializeField]
    private GameObject _shopPanel;

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
        _adventurerHp.text = "HP:    " + _adventurerList[index].hp.ToString();
        _adventurerAtk.text = "ATK:  	" + _adventurerList[index].atk.ToString();
        _adventurerDef.text = "DEF:   " + _adventurerList[index].def.ToString();
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
        if (_stateManager.GetLayer() > 3)
        {
            SceneManager.LoadScene("SettlementScene");
        }
        else
        {
            SceneManager.LoadScene("DungeonRunScene");
        }
    }

    private void AdjustAdventurerInfo(float hp, int atk, int def, Sprite img)
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
        AdjustAdventurerInfo(item.HP, item.ATK, item.DEF, item.itemImage);
        TradeManager.AddNewItem();
        TradeManager.CloseDes();
    }
}
