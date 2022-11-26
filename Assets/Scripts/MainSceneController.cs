using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour
{
    [SerializeField]
    private GameObject _main;

    [SerializeField]
    private GameObject _npc1;

    public void OnNpcInfo()
    {
        _npc1.SetActive(true);
        _main.SetActive(false);
    }

    public void OnBackToMainScene()
    {
        _npc1.SetActive(false);
        _main.SetActive(true);
    }

    public void OnGoShop()
    {
        SceneManager.LoadScene("ShopScene");
    }
}
