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

    void Start()
    {
        StartCoroutine(openBook());
    }

    private IEnumerator openBook()
    {
        _introBook.GetComponent<BookAnimator>().OpenBook();
        yield return new WaitForSeconds(0.8f);
        _inPageUI.SetActive(true);
    }

    private IEnumerator closeBook()
    {
        _introBook.GetComponent<BookAnimator>().CloseBook();
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene("MainScene");
    }

    private IEnumerator flipRight()
    {
        _introBook.GetComponent<BookAnimator>().FlipRight();
        yield return new WaitForSeconds(0.8f);
    }

    private IEnumerator flipLeft()
    {
        _introBook.GetComponent<BookAnimator>().FlipLeft();
        yield return new WaitForSeconds(0.8f);
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
