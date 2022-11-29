using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private bool _lastSelectStage = false;
    private int _stageBookPage = 0;
    private int _goldRatio = 100;
    private List<string> _specialConditions = new List<string>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
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
