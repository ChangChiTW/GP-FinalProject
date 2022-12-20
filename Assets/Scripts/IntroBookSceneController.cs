using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroBookSceneController : MonoBehaviour
{
    [SerializeField]
    protected GameObject _book;

    [SerializeField]
    protected GameObject _leftBtn;

    [SerializeField]
    protected GameObject _rightBtn;

    [SerializeField]
    protected GameObject _nextBtn;

    [SerializeField]
    protected List<GameObject> _pages;
    protected StateManager _stateManager;
    protected AdventurerManager _adventurerManager;
    protected int _currentPage;

    void Awake()
    {
        _stateManager = GameObject.Find("StateManager").GetComponent<StateManager>();
        _adventurerManager = GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>();
    }

    void Start()
    {
        init();
        StartCoroutine(openBook());
    }

    protected virtual void init()
    {
        _currentPage = 0;
    }

    protected void disActivateAllContents()
    {
        _pages[_currentPage].SetActive(false);
        _leftBtn.SetActive(false);
        _rightBtn.SetActive(false);
        if (_nextBtn != null)
        {
            _nextBtn.SetActive(false);
        }
    }

    protected IEnumerator openBook()
    {
        _book.GetComponent<BookAnimator>().OpenBook();
        yield return new WaitForSeconds(0.8f);
        _pages[_currentPage].SetActive(true);
        if (_currentPage > 0)
        {
            _leftBtn.SetActive(true);
        }
        if (_currentPage < _pages.Count - 1)
        {
            _rightBtn.SetActive(true);
        }
    }

    protected IEnumerator closeBook()
    {
        disActivateAllContents();
        _book.GetComponent<BookAnimator>().CloseBook();
        yield return new WaitForSeconds(0.8f);
        if (SceneManager.GetActiveScene().name == "IntroBookScene")
        {
            SceneManager.LoadScene("MainScene");
        }
        else
        {
            SceneManager.LoadScene(_stateManager.GetLastSceneToStageBookScene());
        }
    }

    protected IEnumerator flipRight()
    {
        disActivateAllContents();
        _book.GetComponent<BookAnimator>().FlipRight();
        yield return new WaitForSeconds(0.6f);
        _currentPage--;
        _pages[_currentPage].SetActive(true);
        _rightBtn.SetActive(true);
        if (_currentPage != 0)
        {
            _leftBtn.SetActive(true);
        }
    }

    protected IEnumerator flipLeft()
    {
        disActivateAllContents();
        _book.GetComponent<BookAnimator>().FlipLeft();
        yield return new WaitForSeconds(0.6f);
        _currentPage++;
        _pages[_currentPage].SetActive(true);
        _leftBtn.SetActive(true);
        if (_currentPage != _pages.Count - 1)
        {
            _rightBtn.SetActive(true);
        }
        if (_currentPage == _pages.Count - 1 && _nextBtn != null)
        {
            _nextBtn.SetActive(true);
        }
    }

    public virtual void OnCloseBook()
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
