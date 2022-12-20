using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private int _day = 1;
    private int _layer = 0;
    private int _balance = 20000;
    private int[] _debt = { 100, 300, 600, 1000, 1500, 2000, 10000 };
    private string _lastSceneToStageBookScene = "ShopScene";
    private bool _lastSelectStage = false;
    private int _stageBookPage = 0;
    private int[] _goldRatio = { 100, 125, 150, 200 };
    private int[] _settlement = { 0, 0, 0, 0, 0 };
    private int[] _expectedBalance = { 1500, 2100, 2760, 3416, 3965, 4345, -3000 };
    private List<string> _specialConditions = new List<string>();

    public AudioSource audioPlayer;
    public AudioClip buttonSE;
    public AudioClip flipbook;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("StateManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public void ResetDay()
    {
        _day = 1;
    }

    public void AddDay()
    {
        _day++;
    }

    public int GetDay()
    {
        return _day;
    }

    public void ResetLayer()
    {
        _layer = 0;
    }

    public void AddLayer()
    {
        _layer++;
    }

    public int GetLayer()
    {
        return _layer;
    }

    public void ResetBalance()
    {
        _balance = 1000;
    }

    public void AddBalance(int balance)
    {
        _settlement[_layer] += balance;
        _balance += balance;
    }

    public int GetBalance()
    {
        return _balance;
    }

    public int GetDebt()
    {
        return _debt[_day - 1];
    }

    public void SetLastSceneToStageBookScene(string sceneName)
    {
        _lastSceneToStageBookScene = sceneName;
    }

    public string GetLastSceneToStageBookScene()
    {
        return _lastSceneToStageBookScene;
    }

    public void ResetLastSelectStage()
    {
        _lastSelectStage = false;
    }

    public void SetLastSelectStage(bool lastSelectStage)
    {
        _lastSelectStage = lastSelectStage;
    }

    public bool GetLastSelectStage()
    {
        return _lastSelectStage;
    }

    public void ResetStageBookPage()
    {
        _stageBookPage = 0;
    }

    public void SetStageBookPage(int page)
    {
        _stageBookPage = page;
    }

    public int GetStageBookPage()
    {
        return _stageBookPage;
    }

    public int GetGoldRatio()
    {
        return _goldRatio[_layer];
    }

    public int GetNextGoldRatio()
    {
        return _goldRatio[_layer + 1];
    }

    public void ResetSettlement()
    {
        for (int i = 0; i < _settlement.Length; i++)
        {
            _settlement[i] = 0;
        }
    }

    public int GetSettlement(int index)
    {
        return _settlement[index];
    }

    public int[] GetSettlement()
    {
        return _settlement;
    }

    public int GetExpectedBalance()
    {
        return _expectedBalance[_day - 1];
    }

    public void AddSpecialCondition(string condition)
    {
        _specialConditions.Add(condition);
    }

    public List<string> GetSpecialConditions()
    {
        return _specialConditions;
    }

    public void Reset()
    {
        ResetDay();
        ResetLayer();
        ResetBalance();
        ResetSettlement();
        ResetLastSelectStage();
        ResetStageBookPage();
        _specialConditions.Clear();
    }

    public void playButtonSE()
    {
        audioPlayer.PlayOneShot(buttonSE);
    }

    public void playFlipBookSE()
    {
        audioPlayer.PlayOneShot(flipbook);
    }
}
