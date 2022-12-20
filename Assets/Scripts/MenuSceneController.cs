using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]
    private Inventory _myBag;

    public void OnStart()
    {
        for (int i = _myBag.itemList.Count - 1; i >= 0; i--)
        {
            _myBag.itemList[i].itemHeld = 1;
            _myBag.itemList.Remove(_myBag.itemList[i]);
        }

        SceneManager.LoadScene("IntroBookScene");
    }

    public void OnLoad()
    {
        // TODO: load game feature
        Debug.Log("LoadNewGame");
    }
}
