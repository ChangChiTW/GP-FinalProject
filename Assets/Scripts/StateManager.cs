using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private int _day = 1;
    private int _layer = 0;
    private int _balance = 1000;
    private int _debt = 2000;
    private string _lastSceneToStageBookScene;
    private bool _lastSelectStage = false;
    private int _stageBookPage = 0;
    private int _goldRatio = 100;
    private List<string> _specialConditions = new List<string>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetDay(int day)
    {
        _day = day;
    }

    public void AddDay()
    {
        _day++;
    }

    public int GetDay()
    {
        return _day;
    }

    public void SetLayer(int layer)
    {
        _layer = layer;
    }

    public void AddLayer()
    {
        _layer++;
    }

    public int GetLayer()
    {
        return _layer;
    }

    public void SetBalance(int balance)
    {
        _balance = balance;
    }

    public void AddBalance(int balance)
    {
        _balance += balance;
    }

    public int GetBalance()
    {
        return _balance;
    }

    public void SetDebt(int debt)
    {
        _debt = debt;
    }

    public void AddDebt(int debt)
    {
        _debt += debt;
    }

    public int GetDebt()
    {
        return _debt;
    }
    public void SetLastSceneToStageBookScene(string sceneName)
    {
        _lastSceneToStageBookScene = sceneName;
    }

    public string GetLastSceneToStageBookScene()
    {
        return _lastSceneToStageBookScene;
    }

    public void SetLastSelectStage(bool lastSelectStage)
    {
        _lastSelectStage = lastSelectStage;
    }

    public bool GetLastSelectStage()
    {
        return _lastSelectStage;
    }

    public void SetStageBookPage(int page)
    {
        _stageBookPage = page;
    }

    public int GetStageBookPage()
    {
        return _stageBookPage;
    }

    public void SetGoldRatio(int ratio)
    {
        _goldRatio = ratio;
    }

    public int GetGoldRatio()
    {
        return _goldRatio;
    }

    public void AddSpecialCondition(string condition)
    {
        _specialConditions.Add(condition);
    }

    public List<string> GetSpecialConditions()
    {
        return _specialConditions;
    }
}
