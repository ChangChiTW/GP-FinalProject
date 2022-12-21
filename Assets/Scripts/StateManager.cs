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
    private int[] _goldRatio = { 100, 125, 150, 200 };
    private int[] _settlement = { 0, 0, 0, 0, 0 };
    private int[] _expectedBalance = { 1500, 2100, 2760, 3416, 3965, 4345, -3000 };
    private List<string> _specialConditions = new List<string>();

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void ResetDay()
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

    private void ResetLayer()
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

    private void BalanceMinusDebt()
    {
        _balance -= _debt[_day - 1];
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

    private void ResetLastSceneToStageBookScene()
    {
        _lastSceneToStageBookScene = "ShopScene";
    }

    public void SetLastSceneToStageBookScene(string sceneName)
    {
        _lastSceneToStageBookScene = sceneName;
    }

    public string GetLastSceneToStageBookScene()
    {
        return _lastSceneToStageBookScene;
    }

    public int GetGoldRatio()
    {
        return _goldRatio[_layer];
    }

    public int GetNextGoldRatio()
    {
        return _goldRatio[_layer + 1];
    }

    private void ResetSettlement()
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

    private void ResetSpecialConditions()
    {
        _specialConditions.Clear();
    }

    public void AddSpecialCondition(string condition)
    {
        _specialConditions.Add(condition);
    }

    public List<string> GetSpecialConditions()
    {
        return _specialConditions;
    }

    public void DailyReset()
    {
        BalanceMinusDebt();
        AddDay();
        ResetLayer();
        ResetSettlement();
        ResetLastSceneToStageBookScene();
    }

    public void Reset()
    {
        ResetDay();
        ResetLayer();
        ResetBalance();
        ResetSettlement();
        ResetSpecialConditions();
        ResetLastSceneToStageBookScene();
    }
}
