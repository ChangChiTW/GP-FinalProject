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
    void Start()
    {
        transform.position = InitPos;
    }

    // Update is called once per frame
    void Update()
    {
        
        AvgX = 0;
        if(AllCharacters.transform.childCount>0){
            foreach( Transform c in AllCharacters.transform){
                AvgX+=c.position.x;
            }
            AvgX/=AllCharacters.transform.childCount;
        
            if(AvgX>-3 && AvgX<6)
                transform.position = Vector3.Lerp(transform.position, new Vector3(AvgX, transform.position.y, -10), 0.06f);
        }
    }
}
