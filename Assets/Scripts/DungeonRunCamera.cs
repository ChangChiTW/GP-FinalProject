using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRunCamera : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 InitPos = new Vector3(-3, 0, -10);
    [SerializeField]
    private GameObject AllCharacters;
    private float AvgX = 0;
    private float prevX = -3;

    private float AliveCharCount = 0;
    void Start()
    {
        transform.position = InitPos;
    }

    // Update is called once per frame
    void Update()
    {
        
        AvgX = 0;
        if(AllCharacters.transform.childCount>0){
            AliveCharCount = 0;
            foreach( Transform c in AllCharacters.transform){
                if(c.gameObject.GetComponent<AdventurerBehavior>().Alive){
                    AvgX+=c.position.x;
                    AliveCharCount++;
                }
            }
            if(AliveCharCount>0){
                AvgX/=AliveCharCount;
                if(AvgX>-3 && AvgX<6 && prevX<AvgX)
                    transform.position = Vector3.Lerp(transform.position, new Vector3(AvgX, transform.position.y, -10), 0.06f);
                prevX = transform.position.x;
            }
            
        }
        
    }
}
