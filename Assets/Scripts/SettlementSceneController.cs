using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettlementSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _backgroundImage;

    [SerializeField]
    private List<Sprite> _backgroundImages = new List<Sprite>();

    [SerializeField]
    private GameObject _rightBtn;

    [SerializeField]
    private GameObject _leftBtn;

    [SerializeField]
    private GameObject _doneBtn;

    [SerializeField]
    private GameObject[] _layerBalances = new GameObject[4];

    [SerializeField]
    private GameObject _debtText;

    [SerializeField]
    private GameObject _totalBalanceText;
    private AudioManager _audioManager;
    private StateManager _stateManager;
    private AdventurerManager _adventurerManager;
    private int _currentLayer = 2;

    void Awake()
    {
        _audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        _adventurerManager = GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>();
    }

    void Start()
    {
        UpdateResult();
    }

    private void ChangeBackgroundImage(int layer)
    {
        _backgroundImage.GetComponent<Image>().sprite = _backgroundImages[layer];
    }

    public void OnRightBtnClick()
    {
        _audioManager.PlayBtnClick();
        if (_currentLayer < _backgroundImages.Count - 1)
        {
            _currentLayer++;
            ChangeBackgroundImage(_currentLayer);
        }
        UpdateResult();
    }

    public void OnLeftBtnClick()
    {
        _audioManager.PlayBtnClick();
        if (_currentLayer > 0)
        {
            _currentLayer--;
            ChangeBackgroundImage(_currentLayer);
        }
        UpdateResult();
    }

    public void UpdateResult()
    {
        _leftBtn.SetActive(false);
        _rightBtn.SetActive(false);
        _doneBtn.SetActive(false);
        if (_currentLayer == 0)
        {
            _leftBtn.SetActive(false);
            _rightBtn.SetActive(true);
        }
        else if (_currentLayer == _backgroundImages.Count - 1)
        {
            _leftBtn.SetActive(true);
            _rightBtn.SetActive(false);
            _doneBtn.SetActive(true);
        }
        else
        {
            _leftBtn.SetActive(true);
            _rightBtn.SetActive(true);
        }
        foreach (GameObject layerBalance in _layerBalances)
        {
            layerBalance.SetActive(false);
        }
        int totalBalance = 0;
        for (int i = 0; i < _currentLayer + 2; i++)
        {
            _layerBalances[i].SetActive(true);
            int balance = _stateManager.GetSettlement(i);
            if (balance < 0)
            {
                _layerBalances[i].transform.Find("Balance").GetComponent<TMP_Text>().text =
                    balance.ToString();
                _layerBalances[i].transform.Find("Balance").GetComponent<TMP_Text>().color =
                    Color.red;
            }
            else
            {
                _layerBalances[i].transform.Find("Balance").GetComponent<TMP_Text>().text =
                    "+" + balance.ToString();
                _layerBalances[i].transform.Find("Balance").GetComponent<TMP_Text>().color =
                    Color.green;
            }
            totalBalance += balance;
        }
        int debt = _stateManager.GetDebt();
        _debtText.GetComponent<TMP_Text>().text = "-" + debt.ToString();
        _debtText.GetComponent<TMP_Text>().color = Color.red;
        totalBalance -= debt;
        if (totalBalance < 0)
        {
            _totalBalanceText.GetComponent<TMP_Text>().text = totalBalance.ToString();
            _totalBalanceText.GetComponent<TMP_Text>().color = Color.red;
        }
        else
        {
            _totalBalanceText.GetComponent<TMP_Text>().text = "+" + totalBalance.ToString();
            _totalBalanceText.GetComponent<TMP_Text>().color = Color.green;
        }
    }

    public void OnDoneBtnClick()
    {
        _audioManager.PlayBtnClick();
        _adventurerManager.initAdventurerList();
        int totalBalance = _stateManager.GetBalance() - _stateManager.GetDebt();
        if (totalBalance < 0)
        {
            SceneManager.LoadScene("FailScene");
        }
        else
        {
            if (_stateManager.GetDay() == 7)
            {
                SceneManager.LoadScene("SuccessScene");
            }
            else
            {
                _stateManager.SetStageBookPage(0);
                _stateManager.ResetSettlement();
                _stateManager.BalanceMinusDebt();
                _stateManager.ResetLayer();
                _stateManager.AddDay();
                SceneManager.LoadScene("StageBookScene");
            }
        }
    }
}
