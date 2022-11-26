using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    private Canvas _shop;

    void Start()
    {
        _shop = GameObject.Find("Shop").GetComponent<Canvas>();
    }
}
