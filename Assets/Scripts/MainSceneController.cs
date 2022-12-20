using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _introBook;

    [SerializeField]
    private GameObject _stageBook;

    [SerializeField]
    private GameObject _introBookmark;

    [SerializeField]
    private GameObject _stageBookmark;

    private StateManager _stateManager;
    private float _openBookmarkX;
    private float _closeBookmarkX;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
    }

    void Start()
    {
        _openBookmarkX = _introBookmark.GetComponent<Transform>().position.x;
        _closeBookmarkX = _stageBookmark.GetComponent<Transform>().position.x;
        if (_stateManager.GetLastSelectStage())
        {
            OnSelectStageBook();
        }
        string day = "Day " + _stateManager.GetDay();
        _stageBook.transform.Find("Title").GetComponent<TMP_Text>().text = day;
        _stageBookmark.transform
            .Find("StageBook")
            .transform.Find("Title")
            .GetComponent<TMP_Text>()
            .text = day;
    }

    public void OnSelectIntroBook()
    {
        _stateManager.SetLastSelectStage(false);
        _introBook.SetActive(true);
        _introBookmark.GetComponent<Transform>().position = new Vector3(
            _openBookmarkX,
            _introBookmark.GetComponent<Transform>().position.y,
            _introBookmark.GetComponent<Transform>().position.z
        );
        _stageBook.SetActive(false);
        _stageBookmark.GetComponent<Transform>().position = new Vector3(
            _closeBookmarkX,
            _stageBookmark.GetComponent<Transform>().position.y,
            _stageBookmark.GetComponent<Transform>().position.z
        );
    }

    public void OnSelectStageBook()
    {
        _stateManager.SetLastSelectStage(true);
        _introBook.SetActive(false);
        _introBookmark.GetComponent<Transform>().position = new Vector3(
            _closeBookmarkX,
            _introBookmark.GetComponent<Transform>().position.y,
            _introBookmark.GetComponent<Transform>().position.z
        );
        _stageBook.SetActive(true);
        _stageBookmark.GetComponent<Transform>().position = new Vector3(
            _openBookmarkX,
            _stageBookmark.GetComponent<Transform>().position.y,
            _stageBookmark.GetComponent<Transform>().position.z
        );
    }

    public void OnOpenIntroBook()
    {
        _stateManager.SetLastSceneToStageBookScene("MainScene");
        SceneManager.LoadScene("IntroBookScene");
    }

    public void OnOpenStageBook()
    {
        _stateManager.SetLastSceneToStageBookScene("MainScene");
        SceneManager.LoadScene("StageBookScene");
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
