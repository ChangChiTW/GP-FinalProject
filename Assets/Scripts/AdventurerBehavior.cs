using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AdventurerBehavior : MonoBehaviour
{
    public float hp = 100f;
    public float maxHP = 100f;
    public AdventurerHPBar HealthBar;
    // Start is called before the first frame update
    public bool Walking = false;
    public bool Arrived = false;
    public float speed = 1;

    public GameObject WalkingDot;

    private Vector2[] WalkGoals = new Vector2[100];
    private int CurrFloor = 0;
    private int maxFloor = 10;
    public bool Alive = true;

    public string job;
    public float atk;
    private int Steps = 0;
    public float def;
    public Sprite img;
    public List<Sprite> preferenceImgs = new List<Sprite>();
    public List<Sprite> itemImgs = new List<Sprite>();
    void Start()
    {
        HealthBar.SetHP(hp, maxHP);
        oriY = txt.transform.position.y;
        History = GameObject.Find("History").GetComponent<TextLogScript>();
    }

    // Update is called once per frame
    private int roomwait = 0;
    private int dmgAnimationFrame = 0;
    private int moreHPAnimationFrame = 0;
    private float maxFrames = 600;
    private float oriY;
    [SerializeField] TextMeshProUGUI txt;

    private TextLogScript History;
    void Update()
    {
        if(Alive && ! Arrived){
            if(Walking && Vector2.Distance(WalkGoals[CurrFloor], gameObject.transform.position)<0.01f){ //Next Floor
                if(roomwait>50){
                    roomwait = 0;
                    CurrFloor++;
                }
                roomwait++;
            }

            if(dmgAnimationFrame>0){
                dmgAnimationFrame--;
                txt.alpha = Mathf.Min(dmgAnimationFrame, maxFrames)/maxFrames;
                if(dmgAnimationFrame <= 0){
                    txt.gameObject.SetActive(false);
                }
            }

            if(moreHPAnimationFrame>0){
                moreHPAnimationFrame--;
                txt.alpha = Mathf.Min(moreHPAnimationFrame, maxFrames)/maxFrames;
                if(moreHPAnimationFrame <= 0){
                    txt.gameObject.SetActive(false);
                }
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
                var step =  speed/7f * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, WalkGoals[CurrFloor], step);
                Steps++;
                if(Steps>150){
                    Steps = 0;
                    Instantiate(WalkingDot, transform.position, Quaternion.identity, GameObject.Find("DotCollection").transform);
                }
            }

        }else if(!Alive){ //dead
        
            Color a = transform.Find("Circle").gameObject.GetComponent<SpriteRenderer>().color;
            if(a.a>0.01f){
                a.a = a.a*0.995f;
            }else{
                a.a = 0;
            }
            transform.Find("Circle").gameObject.GetComponent<SpriteRenderer>().color = a;
            GameObject.Find("Canvas").gameObject.transform.Find(gameObject.name).Find("isDead").gameObject.SetActive(true);
        }
    }


    public void TakeHit(float dmg){
        txt.text = "-"+dmg.ToString();
        txt.gameObject.SetActive(true);
        History.UpdateNewText(gameObject.name+" has taken "+dmg.ToString()+" damage", Color.black);
        dmgAnimationFrame = Mathf.FloorToInt(maxFrames)+10;
        hp -= dmg;
        HealthBar.SetHP(hp, maxHP);

        transform.position = new Vector3(transform.position.x-1, transform.position.y, transform.position.z);

        if(hp<=0){
            transform.Find("Grave").gameObject.SetActive(true);
            transform.Find("Sprite").gameObject.SetActive(false);
            transform.Find("Canvas").gameObject.SetActive(false);
            History.UpdateNewText(gameObject.name+" has died", Color.red);
            gameObject.GetComponent<BoxCollider>().size = new Vector3(0, 0, 0);
            Alive = false;
        }
    }
    public void AddATK(float a){
        History.UpdateNewText(gameObject.name+" has gained "+a.ToString()+" ATK", Color.blue);
        atk+=a;
    }
    public void AddHP(float more){
        txt.text = "+"+more.ToString();
        txt.gameObject.SetActive(true);
        History.UpdateNewText(gameObject.name+" has recovered "+more.ToString()+" HP", Color.green);
        moreHPAnimationFrame = Mathf.FloorToInt(maxFrames)+10;
        hp += more;
        HealthBar.SetHP(hp, maxHP);

    }

    public void SetWalkGoals(Vector2[] goals, int m){
        WalkGoals = goals;
        maxFloor = m;
    }

    public void EnterBattle(){

    }
}
