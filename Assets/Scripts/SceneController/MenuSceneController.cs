using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]
    private Inventory _myBag;

    [SerializeField]
    private GameObject _resumeBtn;

    void Start()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayMenuBGM();
        if (GameObject.Find("GameManager").GetComponent<StateManager>().ResumeGame())
        {
            _resumeBtn.GetComponent<Button>().interactable = true;
            _resumeBtn.transform.Find("Text").GetComponent<TMP_Text>().color = Color.black;
        }
        else
        {
            _resumeBtn.GetComponent<Button>().interactable = false;
            Color textColor = Color.black;
            textColor.a = 0.3f;
            _resumeBtn.transform.Find("Text").GetComponent<TMP_Text>().color = textColor;
        }
    }

    public void OnNewGame()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        GameObject.Find("GameManager").GetComponent<StateManager>().NewGame();
        for (int i = _myBag.itemList.Count - 1; i >= 0; i--)
        {
            _myBag.itemList[i].itemHeld = 1;
            _myBag.itemList.Remove(_myBag.itemList[i]);
        }
        SceneManager.LoadScene("IntroBookScene");
    }

    public void OnResume()
    {
        GameObject.Find("GameManager").GetComponent<AudioManager>().PlayBtnClick();
        GameObject.Find("GameManager").GetComponent<StateManager>().ResumeGame();
        SceneManager.LoadScene("StageBookScene");
    }
}
