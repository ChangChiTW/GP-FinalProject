using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _backgroundStory;

    [SerializeField]
    private GameObject _howToPlay;

    public void OnStart()
    {
        _backgroundStory.SetActive(true);
    }

    public void OnBackgroundStoryContinue()
    {
        _backgroundStory.SetActive(false);
        _howToPlay.SetActive(true);
    }

    public void OnHowToPlayContinue()
    {
        _howToPlay.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }

    public void OnOption()
    {
        Debug.Log("OnOption");
    }
}
