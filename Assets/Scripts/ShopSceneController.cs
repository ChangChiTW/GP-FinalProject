using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSceneController : MonoBehaviour
{
    Canvas Shop;

    void Start()
    {
        Shop = GameObject.Find("Shop").GetComponent<Canvas>();
    }
}
