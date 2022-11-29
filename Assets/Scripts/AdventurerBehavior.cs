using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerBehavior : MonoBehaviour
{
    public float hp = 100f;
    public float maxHP = 100f;
    public AdventurerHPBar HealthBar;
    // Start is called before the first frame update
    public bool Walking = false;
    public bool Arrived = false;

    private float speed = 1f;

    private Vector2[] WalkGoals = new Vector2[100];
    private int CurrFloor = 0;
    private int maxFloor = 4;

    public string job;
    public int atk;
    public int def;
    public ItemInfo[] items;

    void Start()
    {
        HealthBar.SetHP(hp, maxHP);
    }

    // Update is called once per frame
    void Update()
    {
        if(Walking && Vector2.Distance(WalkGoals[CurrFloor], gameObject.transform.position)<0.01f){ //Next Floor
            CurrFloor++;
        }

        if(CurrFloor>=WalkGoals.Length){
            Arrived = true;
        }

        if(CurrFloor<WalkGoals.Length && (WalkGoals[CurrFloor].x!=0 || WalkGoals[CurrFloor].y!=0)){
            Walking = true;
        }else{
            Walking = false;
        }

        
        HealthBar.SetHP(hp, maxHP);
        if(Walking){
            var step =  speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, WalkGoals[CurrFloor], step);
        }
        
    }

    public void TakeHit(float dmg){
        hp -= dmg;
        HealthBar.SetHP(hp, maxHP);

        transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);

        if(hp<=0){
            Destroy(gameObject);
        }
    }

    public void SetWalkGoals(Vector2[] goals, int m){
        WalkGoals = goals;
        maxFloor = m;
    }

    public void SetSpeed(float s){
        speed = s;
    }
}
