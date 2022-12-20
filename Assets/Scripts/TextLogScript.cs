using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class TextLogScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txt1;
    [SerializeField] private TextMeshProUGUI txt2;
    [SerializeField] private TextMeshProUGUI txt3;
    [SerializeField] private TextMeshProUGUI txt4;

    [SerializeField] private TextMeshProUGUI txt5;


    // Start is called before the first frame update
    void Start()
    {
        newNum = 0;
        rotate = new TextMeshProUGUI[]{txt5, txt4, txt3, txt2, txt1};

        ChangeText(txt1, new Color(0, 0, 0));
        ChangeText(txt3, new Color(0, 0, 0));
        ChangeText(txt4, new Color(0, 0, 0));
        ChangeText(txt2, new Color(0, 0, 0));
        ChangeText(txt5, new Color(0, 0, 0));
        /*
        txt1.alpha = 0.1f;
        txt2.alpha = 0.3f;
        txt3.alpha = 0.6f;
        txt4.alpha = 0.8f;
        txt5.alpha = 1f;
        */

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeText(TextMeshProUGUI t, Color c, string s=""){
        t.text = s;
        t.color = c;
    }

    private int newNum;
    private TextMeshProUGUI[] rotate;
    public void UpdateNewText(string newest, Color c){
        Vector3 temp = rotate[0].transform.position;
        for(int i=0; i<rotate.Length-1; i++){
            rotate[i].transform.position = rotate[i+1].transform.position;
        }
        rotate[rotate.Length-1].transform.position = temp;
        newNum--;
        newNum+=rotate.Length;
        newNum%=rotate.Length;
        ChangeText(rotate[newNum], c, newest);
        
    }
}
