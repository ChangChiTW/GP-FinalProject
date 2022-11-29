using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private float _openBookmarkX;
    private float _closeBookmarkX;

    void Start()
    {
        _openBookmarkX = _introBookmark.GetComponent<Transform>().position.x;
        _closeBookmarkX = _stageBookmark.GetComponent<Transform>().position.x;
    }

    public void OnSelectIntroBook()
    {
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

    public void OnOpenIntroBook() { 
        SceneManager.LoadScene("IntroBookScene");
    }

    public void OnOpenStageBook() { 
        SceneManager.LoadScene("StageBookScene");
    }
}
