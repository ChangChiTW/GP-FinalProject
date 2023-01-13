using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    private GameData _gameData;
    private int _layer = 0;
    private int[] _debt = { 100, 300, 600, 1000, 1500, 2000, 10000 };
    private string _lastSceneToStageBookScene = "ShopScene";
    private int[] _settlement = { 0, 0, 0, 0, 0 };
    private int[] _expectedBalance = { 1500, 2100, 2760, 3416, 3965, 4345, -3000 };
    private int[] _monsterRatio = { 100, 100, 105, 110, 120 };
    private FileDataHandler _fileDataHandler;
    private int _currentday;

    void Start()
    {
        if (GameObject.FindGameObjectsWithTag("GameManager").Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        _fileDataHandler = new FileDataHandler(
            Application.persistentDataPath,
            "saveData.json",
            true
        );
    }

    public void AddDay()
    {
        _gameData.day++;
    }

    public int GetDay()
    {
        return _gameData.day;
    }

    public void SetCurentDay(int today)
    {
        _currentday = today;
    }

    public int GetCurrentDay()
    {
        return _currentday;
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

    private void BalanceMinusDebt()
    {
        _gameData.balance -= _debt[_gameData.day - 1];
    }

    public void SecretAddBalance(int balance)
    {
        _gameData.balance += balance;
    }

    public void AddBalance(int balance)
    {
        _settlement[_layer] += balance;
        _gameData.balance += balance;
    }

    public int GetBalance()
    {
        return _gameData.balance;
    }

    public int GetDebt()
    {
        return _debt[_gameData.day - 1];
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

    public int GetMonsterRatio()
    {
        return _monsterRatio[_layer];
    }

    public int GetAdventurerBalance()
    {
        return _gameData.adventurerBalance;
    }

    public void AddAdventurerBalance(int balance)
    {
        _gameData.adventurerBalance += balance;
    }

    public int GetRaiseRatio()
    {
        return _gameData.raiseRatio;
    }

    public void AddRaiseRatio(int ratio)
    {
        _gameData.raiseRatio += ratio;
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
        return _expectedBalance[_gameData.day - 1];
    }

    private void SetGameData(GameData gameData)
    {
        _gameData = gameData;
    }

    public void SaveGame()
    {
        _fileDataHandler.Save(_gameData);
    }

    public void NewGame()
    {
        SetGameData(new GameData());
    }

    public bool ResumeGame()
    {
        GameData loadedData = _fileDataHandler.Load();
        if (loadedData == null)
        {
            return false;
        }
        SetGameData(loadedData);
        return true;
    }

    public bool DeveloperMode()
    {
        return true;
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
        _fileDataHandler.Delete();
        NewGame();
        ResetLayer();
        ResetSettlement();
        ResetLastSceneToStageBookScene();
    }
}
