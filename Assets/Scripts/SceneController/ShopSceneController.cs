using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopSceneController : MonoBehaviour
{
    [SerializeField]
    private Inventory myBag;
    private AudioManager _audioManager;
    private StateManager _stateManager;

    void Awake()
    {
        _audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
        _stateManager = GameObject.Find("GameManager").GetComponent<StateManager>();
    }

    void Update()
    {
        if (_stateManager.DeveloperMode())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _stateManager.SecretAddBalance(1000);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _stateManager.SecretAddBalance(-1000);
            }
        }
    }

    public void OnCheckFloorInfo()
    {
        _audioManager.PlayBtnClick();
        _stateManager.SetLastSceneToStageBookScene("ShopScene");
        SceneManager.LoadScene("StageBookScene");
    }

    public void OnGoDungeon()
    {
        _audioManager.PlayBtnClick();
        _stateManager.AddLayer();
        if (_stateManager.GetLayer() > 3 || myBag.itemList.Count == 0)
        {
            SceneManager.LoadScene("SettlementScene");
        }
        else
        {
            SceneManager.LoadScene("DungeonScene");
        }
    }
}
