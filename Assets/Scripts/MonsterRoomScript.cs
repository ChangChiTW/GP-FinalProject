using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRoomScript : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float Attack = 10;
    [SerializeField] private float Lives = 20;

    [SerializeField] private Sprite Mon1;
    [SerializeField] private Sprite Mon2;
    [SerializeField] private Sprite Mon3;

    [SerializeField] private Sprite Mon4;
    public bool IsEnd = false;

    private Sprite[] heads = new Sprite[]{null, null, null, null};
    private float[] ATKs = {5, 9, 7, 11};
    private float[] HPs = {20, 30, 15, 50};


    void Start()
    {
        heads[0] = Mon1;
        heads[1] = Mon2;
        heads[2] = Mon3;
        heads[3] = Mon4;

        int a = Random.Range(0, 4);
        Attack = ATKs[a];
        Lives = HPs[a];
        gameObject.transform.Find("RoomActive").GetComponent<SpriteRenderer>().sprite = heads[a];
        gameObject.GetComponent<LootScript>().LootType = 4;
        if(IsEnd){
            Attack = ATKs[a]*2;
            Lives = HPs[a]*3;
        }
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
            float actualDmg = Mathf.Max(Attack - other.gameObject.GetComponent<AdventurerBehavior>().def, 1);
            other.gameObject.GetComponent<AdventurerBehavior>().TakeHit(actualDmg);
            Lives-=other.gameObject.GetComponent<AdventurerBehavior>().atk;
            if(Lives<=0){
                transform.GetComponent<LootScript>().DespenseLoot(other.gameObject);
                transform.Find("RoomActive").gameObject.SetActive(false);
                transform.Find("RoomDead").gameObject.SetActive(true);
            }
        }
        if(IsEnd && transform.Find("RoomDead").gameObject.activeSelf && other.gameObject.tag == "Adventurer"){
            //TODO: End and return surviving adventurers
        }
    }
}
