using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AdventurerHPBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider Slider;
    public Vector3 Offset;
    void Start()
    {

    }

    public void SetHP(float HP, float MaxHP)
    {
        Slider.gameObject.SetActive(HP <= MaxHP);
        Slider.value = HP;
        Slider.maxValue = MaxHP;
    }

    // Update is called once per frame
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);

    }
}
