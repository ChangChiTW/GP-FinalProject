using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootScript : MonoBehaviour
{
    public bool hasLoot = true;
    public int LootType = 0;

    public GameObject loot;
    [SerializeField] private Sprite Potion;
    [SerializeField] private Sprite BluePotion;
    [SerializeField] private Sprite Trap1;

    [SerializeField] private Sprite Gold;

    private int waiting = 0;
    // Start is called before the first frame update
    void Start()
    {
        lootAnimFrame = 0f;
        History = GameObject.Find("History").GetComponent<TextLogScript>();
        LootType = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(waiting>0)waiting--;
        if(lootAnimFrame>0 && waiting<=0){
            lootAnimFrame--;
            loot.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, lootAnimFrame/MaxLootAnimFrame);
            float ratio = 1.2f-1.2f*lootAnimFrame/MaxLootAnimFrame;
            loot.transform.localScale = new Vector3(1+ratio, 1+ratio);
        }
        if(lootAnimFrame==0){
            loot.SetActive(false);
        }
    }

    private float lootAnimFrame = 0f;
    private float MaxLootAnimFrame = 80f;
    private TextLogScript History;

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
                case 4:
                    MonsterDrop(g);
                    break;
                default:
                    HotSpring(g);
                    break;
            }
            hasLoot = false;
            loot.SetActive(true);
            lootAnimFrame = MaxLootAnimFrame;
            waiting = 80;
        }
    }

    private void HotSpring(GameObject a){
        loot.GetComponent<SpriteRenderer>().sprite = Potion;
        History.UpdateNewText(a.name+" has found a hot spring!", Color.green);
        a.GetComponent<AdventurerBehavior>().AddHP(10);
    }

    private void Spinach(GameObject a){
        loot.GetComponent<SpriteRenderer>().sprite = BluePotion;

        History.UpdateNewText(a.name+" has found some Spinach!", Color.blue);
        a.GetComponent<AdventurerBehavior>().AddATK(10f);
    }

    private void SpikeTrap(GameObject a){
        loot.GetComponent<SpriteRenderer>().sprite = Trap1;
        History.UpdateNewText(a.name+" walked into a trap!", Color.red);
        a.GetComponent<AdventurerBehavior>().TakeHit(10);
    }

    private void GoldenShower(GameObject a){
        loot.GetComponent<SpriteRenderer>().sprite = Gold;
        History.UpdateNewText(a.name+" received a golden shower!", Color.yellow);
    }

    private void MonsterDrop(GameObject a){
        loot.GetComponent<SpriteRenderer>().sprite = Potion;
        a.GetComponent<AdventurerBehavior>().AddHP(10);
    }
}
