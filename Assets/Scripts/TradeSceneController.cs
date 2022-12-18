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
    private GameObject _shopPanel;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        _adventurerManager = GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>();
    }

    void Start()
    {
        _adventurerList = _adventurerManager.GetAdventurerList();
        ShowAdventurerInfo(0);
    }

    private void ShowAdventurerInfo(int index)
    {
        // string imgPath = "Adventurer/" + _adventurerList[index].job;
        // _adventurerImg.GetComponent<Image>().sprite = Resources.Load<Sprite>(imgPath);
        _adventurerName.text = _adventurerList[index].name;
        _adventurerHp.text = "HP:    " + _adventurerList[index].hp.ToString();
        _adventurerAtk.text = "ATK:  	" + _adventurerList[index].atk.ToString();
        _adventurerDef.text = "DEF:   " + _adventurerList[index].def.ToString();
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
        SceneManager.LoadScene("DungeonRunScene");
    }
}
