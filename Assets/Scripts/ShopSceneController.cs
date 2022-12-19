using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneController : MonoBehaviour
{
    private StateManager _stateManager;

    void Start()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    public void OnCheckAdventure()
    {
        SceneManager.LoadScene("TradeScene");
    }

    public void OnCheckFloorInfo()
    {
        _stateManager.SetLastSceneToStageBookScene("ShopScene");
        _stateManager.SetStageBookPage(0);
        SceneManager.LoadScene("StageBookScene");
    }

    public void OnGoDungeon()
    {
        _stateManager.AddLayer();
        SceneManager.LoadScene("DungeonRunScene");
    }
}
