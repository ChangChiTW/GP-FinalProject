using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroBookSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _introBook;

    [SerializeField]
    private GameObject _inPageUI;

    [SerializeField]
    private GameObject[] _pageContents = new GameObject[2];
    private int _currentPage = 0;

    void Start()
    {
        StartCoroutine(openBook());
    }

    private IEnumerator openBook()
    {
        _introBook.GetComponent<BookAnimator>().OpenBook();
        yield return new WaitForSeconds(0.8f);
        _inPageUI.SetActive(true);
        _pageContents[0].SetActive(true);
    }

    private IEnumerator closeBook()
    {
        _pageContents[_currentPage].SetActive(false);
        _inPageUI.SetActive(false);
        _introBook.GetComponent<BookAnimator>().CloseBook();
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator flipRight()
    {
        if (_currentPage != 0)
        {
            _pageContents[_currentPage].SetActive(false);
            _inPageUI.SetActive(false);
            _introBook.GetComponent<BookAnimator>().FlipRight();
            yield return new WaitForSeconds(0.6f);
            _currentPage--;
            _pageContents[_currentPage].SetActive(true);
            _inPageUI.SetActive(true);
        }
    }

    private IEnumerator flipLeft()
    {
        if (_currentPage != _pageContents.Length - 1)
        {
            _pageContents[_currentPage].SetActive(false);
            _inPageUI.SetActive(false);
            _introBook.GetComponent<BookAnimator>().FlipLeft();
            yield return new WaitForSeconds(0.6f);
            _currentPage++;
            _pageContents[_currentPage].SetActive(true);
            _inPageUI.SetActive(true);
        }
    }

    public void OnCloseBook()
    {
        StartCoroutine(closeBook());
    }

    public void OnFlipRight()
    {
        StartCoroutine(flipRight());
    }

    public void OnFlipLeft()
    {
        StartCoroutine(flipLeft());
    }
}
