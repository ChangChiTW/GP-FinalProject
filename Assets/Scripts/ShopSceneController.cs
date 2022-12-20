using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneController : MonoBehaviour
{
    private StateManager _stateManager;
    public Inventory myBag;

    void Awake()
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
        if (_stateManager.GetLayer() > 3 || myBag.itemList.Count == 0)
        {
            SceneManager.LoadScene("SettlementScene");
        }
        else
        {
            SceneManager.LoadScene("DungeonRunScene");
        }
    }

    public void playButtonSE()
    {
        _stateManager.playButtonSE();
    }

    public void playFlipBookSE()
    {
        _stateManager.playFlipBookSE();
    }
}
