using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    Canvas Menu;
    Canvas NPCInfo;
    Canvas NPC1;
    Canvas Shop;

    void Start()
    {
        Menu = GameObject.Find("Menu").GetComponent<Canvas>();
        NPCInfo = GameObject.Find("NPCInfo").GetComponent<Canvas>();
        NPC1 = GameObject.Find("NPC1").GetComponent<Canvas>();
        Shop = GameObject.Find("Shop").GetComponent<Canvas>();
    }

    public void OnStart()
    {
        Debug.Log("OnStart");
        Menu.enabled = false;
    }

    public void OnOption()
    {
        Debug.Log("OnOption");
    }

    public void OnNPC()
    {
        NPC1.enabled = true;
        NPCInfo.enabled = false;
    }

    public void BackFromNPC()
    {
        NPC1.enabled = false;
        NPCInfo.enabled = true;
    }

    public void OnShop()
    {
        NPCInfo.enabled = false;
        NPC1.enabled = false;
    }
}
