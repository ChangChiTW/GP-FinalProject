using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingDotScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float LifeSpan = 1300f;
    private float age = 0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        age++;
        Color a = gameObject.GetComponent<SpriteRenderer>().color;
        a.a = (LifeSpan-age)/LifeSpan;
        gameObject.GetComponent<SpriteRenderer>().color = a;
        if(age>LifeSpan){
            Destroy(gameObject);
        }
    }
}
