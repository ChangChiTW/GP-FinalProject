using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{    
    public Inventory myBag;

    public void OnStart()
    {
        for(int i = myBag.itemList.Count - 1; i >= 0; i--)
        {
            myBag.itemList[i].itemHeld = 1;
            myBag.itemList.Remove(myBag.itemList[i]);
        }

        SceneManager.LoadScene("IntroBookScene");
    }

    public void OnLoad()
    {
        // TODO: load game feature
        Debug.Log("LoadNewGame");
    }
}
