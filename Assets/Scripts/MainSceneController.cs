using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    private Canvas _main;
    private Canvas _npc1;

    void Start()
    {
        _main = GameObject.Find("Main").GetComponent<Canvas>();
        _npc1 = GameObject.Find("Npc1").GetComponent<Canvas>();
    }

    public void OnNPC()
    {
        _npc1.enabled = true;
        _main.enabled = false;
    }

    public void OnBackToMainScene()
    {
        _npc1.enabled = false;
        _main.enabled = true;
    }

    public void OnGoShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
