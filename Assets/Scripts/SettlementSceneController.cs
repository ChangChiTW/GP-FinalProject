using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettlementSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _backgroundImage;

    [SerializeField]
    private List<Sprite> _backgroundImages = new List<Sprite>();

    [SerializeField]
    private GameObject _rightBtn;

    [SerializeField]
    private GameObject _leftBtn;

    [SerializeField]
    private GameObject _doneBtn;
    private StateManager _stateManager;
    private int _currentLayer = 0;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    private void ChangeBackgroundImage(int layer)
    {
        _backgroundImage.GetComponent<Image>().sprite = _backgroundImages[layer];
    }

    public void OnRightBtnClick()
    {
        if (_currentLayer < _backgroundImages.Count - 1)
        {
            _currentLayer++;
            ChangeBackgroundImage(_currentLayer);
            _leftBtn.SetActive(true);
        }
        if (_currentLayer == _backgroundImages.Count - 1)
        {
            _rightBtn.SetActive(false);
            _doneBtn.SetActive(true);
        }
    }

    public void OnLeftBtnClick()
    {
        if (_currentLayer > 0)
        {
            _currentLayer--;
            ChangeBackgroundImage(_currentLayer);
            _rightBtn.SetActive(true);
            _doneBtn.SetActive(false);
        }
        if (_currentLayer == 0)
        {
            _leftBtn.SetActive(false);
        }
    }

    public void OnDoneBtnClick()
    {
        _stateManager.SetInDungeon(false);
        _stateManager.AddDay();
        SceneManager.LoadScene("MainScene");
    }
}
