using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneController : MonoBehaviour
{
    private StateManager _stateManager;

    void Start()
    {
        Shop = GameObject.Find("Trade").GetComponent<Canvas>();
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    public void OnCheckAdventure()
    {
        _stateManager.SetLastSceneToStageBookScene("ShopScene");
        _stateManager.SetStageBookPage(2);
        SceneManager.LoadScene("StageBookScene");
    }

    public void OnCheckFloorInfo()
    {
        _stateManager.SetLastSceneToStageBookScene("ShopScene");
        _stateManager.SetStageBookPage(0);
        SceneManager.LoadScene("StageBookScene");
    }

    public void OnGoDungeon()
    {
        SceneManager.LoadScene("DungeonRunScene");
    }
}
