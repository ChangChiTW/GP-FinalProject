using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SuccessSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _balanceText;

    private StateManager _stateManager;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    void Start()
    {
        int totalBalance = _stateManager.GetBalance() - _stateManager.GetDebt();
        _balanceText.GetComponent<TMP_Text>().text = "$" + totalBalance.ToString();
    }

    public void OnNextBtnClick()
    {
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlayBtnClick();
        _stateManager.Reset();
        SceneManager.LoadScene("MenuScene");
    }
}
