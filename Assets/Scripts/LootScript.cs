using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    private bool hasLoot = true;
    public int LootType = 0;

    public GameObject loot;

    private 
    // Start is called before the first frame update
    void Start()
    {
        lootAnimFrame = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(lootAnimFrame>0){
            lootAnimFrame--;
            loot.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, lootAnimFrame/MaxLootAnimFrame);
            float ratio = 1f-lootAnimFrame/MaxLootAnimFrame;
            loot.transform.localScale = new Vector3(1+ratio, 1+ratio);
        }
        if(lootAnimFrame==0){
            loot.SetActive(false);
        }
    }

    private float lootAnimFrame = 0f;
    private float MaxLootAnimFrame = 70f;

    private void OnTriggerEnter(Collider other) {
        if(gameObject.GetComponent<MonsterRoomScript>() == null){
            DespenseLoot(other.gameObject);
        }
    }

    public void DespenseLoot(GameObject g){
        if(hasLoot){
            switch(LootType){
                case 0:
                    HotSpring(g);
                    break;
                case 1:
                    Spinach(g);
                    break;
                case 2:
                    SpikeTrap(g);
                    break;
                case 3:
                    GoldenShower(g);
                    break;
                default:
                    HotSpring(g);
                    break;
            }
            hasLoot = false;
            loot.SetActive(true);
            lootAnimFrame = MaxLootAnimFrame;
        }
    }

    private void HotSpring(GameObject a){
        
    }

    private void Spinach(GameObject a){

    }

    private void SpikeTrap(GameObject a){

    }

    private void GoldenShower(GameObject a){

    }
}
