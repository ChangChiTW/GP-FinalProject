using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float Attack = 10;
    [SerializeField] private int Lives = 2;

    public bool IsEnd = false;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hitAniFrame>0){
            hitAniFrame--;
            gameObject.transform.Find("RoomActive").GetComponent<SpriteRenderer>().color = Color.red;

        }else{
            gameObject.transform.Find("RoomActive").GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private int hitAniFrame = 0;
    private void OnTriggerEnter(Collider other) {
        hitAniFrame = 20;
        if(transform.Find("RoomActive").gameObject.activeSelf && other.gameObject.tag == "Adventurer"){
            other.gameObject.GetComponent<AdventurerBehavior>().TakeHit(Attack);
            Lives-=1;
            if(Lives<=0){
                transform.Find("RoomActive").gameObject.SetActive(false);
                transform.Find("RoomDead").gameObject.SetActive(true);
            }
        }
        if(IsEnd && transform.Find("RoomDead").gameObject.activeSelf && other.gameObject.tag == "Adventurer"){
            //TODO: End and return surviving adventurers
        }
    }
}
