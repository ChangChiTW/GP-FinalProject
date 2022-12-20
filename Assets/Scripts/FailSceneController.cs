using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FailSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _dayText;

    [SerializeField]
    private GameObject _balanceText;

    [SerializeField]
    private GameObject _debtText;

    [SerializeField]
    private GameObject _text;
    private StateManager _stateManager;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    void Start()
    {
        _dayText.GetComponent<TMP_Text>().text = "Day " + _stateManager.GetDay().ToString();
        _balanceText.GetComponent<TMP_Text>().text = "$" + _stateManager.GetBalance().ToString();
        _debtText.GetComponent<TMP_Text>().text = "$" + _stateManager.GetDebt().ToString();
        _text.GetComponent<TMP_Text>().text =
            "Not enough to pay debts in Day" + _stateManager.GetDay().ToString() + " !!!";
    }

    public void OnRetryBtnClick()
    {
        _stateManager.Reset();
        SceneManager.LoadScene("MenuScene");
    }
}
