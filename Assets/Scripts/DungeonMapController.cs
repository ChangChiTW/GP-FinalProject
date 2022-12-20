using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DungeonMapController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject MapDot;
    [SerializeField] private Transform StartPoint;

    [SerializeField] private GameObject MonsterEncounter;
    [SerializeField] private GameObject SmallChestEncounter;
    [SerializeField] private GameObject BossEncounter;
    [SerializeField] private GameObject LargeChestEncounter;


    [SerializeField] private Transform MapParent;
    [SerializeField] private Transform AdventurerParent;
    [SerializeField] private GameObject CheckAdventurers;

    [SerializeField] private GameObject Mage;
    [SerializeField] private GameObject Knight;
    [SerializeField] private GameObject Archer;
    [SerializeField] private GameObject King;

    [SerializeField] private Sprite bg1;
    [SerializeField] private Sprite bg2;
    [SerializeField] private Sprite bg3;






    private int[] DungeonLayout1 = {2, 3, 1, 2, 2, 1, 1};
    private int[] DungeonLayout2 = {3, 4, 3, 2, 1, 1};

    private int[] DL11 = {1, 1};
    private int[] DL22 = {2, 1};
    private int[] DL21 = {2, 1};

    private int[] DL31 = {2, 1, 1};


    private string[] Dungeonpaths1 = {"12", "12a3", "1a1a1", "12", "1a2", "1a1", "1"};

    private string[] DP11 = {"1", "1"};
    private string[] DP22 = {"12", "1a1"};
    private string[] DP21 = {"12", "1a1"};
    private string[] DP31 = {"12", "1a1", "1"};

    private string[] DungeonRooms1 = {"mm", "mtm", "m", "mm", "tt", "b", "t"};
    private string[] DR11 = {"m", "t"};
    private string[] DR22 = {"mt", "p"};
    private string[] DR21 = {"mt", "p"};
    private string[] DR31 = {"pq", "t", "r"};

    private int[] CurrentLayout;
    private string[] CurrentPaths;
    private string[] CurrentRooms;

    private string CurrentGameLevel;
    

    private AdventurerInfo[] Temp;

    private float[,,] FloorCoords = new float[10, 5, 2]; //[floor, which one, x/y]
    void Start()
    {
        
        CurrentLayout = DungeonLayout1;
        CurrentPaths = Dungeonpaths1;
        CurrentRooms = DungeonRooms1;
        if(GameObject.Find("StateManager") != null)
            CurrentGameLevel = "level"
                                +GameObject.Find("StateManager").GetComponent<StateManager>().GetDay().ToString()
                                +GameObject.Find("StateManager").GetComponent<StateManager>().GetLayer().ToString();
        else
            CurrentGameLevel = "level1";
        Sprite bg;
        if(CurrentGameLevel == "level11"){
            bg = bg1;
            CurrentLayout = DL11;
            CurrentPaths = DP11;
            CurrentRooms = DR11;
        }else if(CurrentGameLevel == "level22"){
            bg = bg2;
            CurrentLayout = DL22;
            CurrentPaths = DP22;
            CurrentRooms = DR22;
        }
        else if(CurrentGameLevel == "level13"){
            bg = bg3;
            CurrentLayout = DungeonLayout1;
            CurrentPaths = Dungeonpaths1;
            CurrentRooms = DungeonRooms1;
        }else if(CurrentGameLevel == "level21"){
            bg = bg2;
            CurrentLayout = DL21;
            CurrentPaths = DP21;
            CurrentRooms = DR21;
        }else if(CurrentGameLevel == "level31"){
            bg = bg3;
            CurrentLayout = DL31;
            CurrentPaths = DP31;
            CurrentRooms = DR31;
        }else if(CurrentGameLevel == "level32"){
            bg = bg3;
            CurrentLayout = DL31;
            CurrentPaths = DP31;
            CurrentRooms = DR31;
        }else if(CurrentGameLevel == "level33"){
            bg = bg3;
            CurrentLayout = DL31;
            CurrentPaths = DP31;
            CurrentRooms = DR31;
        }else{
            bg = bg1;
            CurrentLayout = DungeonLayout1;
            CurrentPaths = Dungeonpaths1;
            CurrentRooms = DungeonRooms1;
        }

        GameObject.Find("Square").GetComponent<SpriteRenderer>().sprite = bg;
        GameObject.Find("Square (1)").GetComponent<SpriteRenderer>().sprite = bg;
        GameObject.Find("Square (2)").GetComponent<SpriteRenderer>().sprite = bg;
        GameObject.Find("Square (3)").GetComponent<SpriteRenderer>().sprite = bg;

        if(GameObject.Find("AdventurerManager")!= null){
            Temp = GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>().GetAdventurerList();
        }else{
            Temp = gameObject.GetComponent<AdventurerManager>().GetAdventurerList();
        }
        float LevelLength = 4; //How long (x) is one dungeon Level

        for (int i=0; i<=CurrentLayout.Length; i++){ // i is current dungeon Level
            if(i == CurrentLayout.Length){
                FloorCoords[i, 0, 0] = StartPoint.position.x + LevelLength*(i+1);
                FloorCoords[i, 0, 1] = 0;
                Instantiate(LargeChestEncounter, new Vector3(StartPoint.position.x + LevelLength*(i+1), 0, 0), Quaternion.identity, MapParent);
            }else{
                float BranchCount = CurrentLayout[i]; //How many branches this dungeon level
                float angle = Mathf.PI/(BranchCount+1);

                for(int a = 0; a< BranchCount; a++){ //each line same X
                    float CurrStartX = StartPoint.position.x + LevelLength*i;
                    float CurrEndX = StartPoint.position.x + LevelLength*(i+1);
                    float CurrAngle = Mathf.PI/2 - angle*(a+1);
                    float CurrEndY = LevelLength*Mathf.Sin(CurrAngle);
                    FloorCoords[i, a, 0] = CurrEndX;
                    FloorCoords[i, a, 1] = CurrEndY;
                    GameObject bruh;
                    switch(CurrentRooms[i][a]){
                        case 'm':
                            bruh = Instantiate(MonsterEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            bruh.GetComponent<MonsterRoomScript>().SetMon(0);
                            break;
                        case 'p':
                            bruh = Instantiate(MonsterEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            bruh.GetComponent<MonsterRoomScript>().SetMon(1);
                            break;
                        case 'q':
                            bruh = Instantiate(MonsterEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            bruh.GetComponent<MonsterRoomScript>().SetMon(2);
                            break;
                        case 'r':
                            bruh = Instantiate(MonsterEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            bruh.GetComponent<MonsterRoomScript>().SetMon(3);
                            break;
                        case 't':
                            bruh = Instantiate(SmallChestEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            break;
                        case 'b':
                            bruh = Instantiate(BossEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            break;
                        default:
                            bruh = Instantiate(MonsterEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                            bruh.GetComponent<MonsterRoomScript>().SetMon(0);
                            break;
                    }
                }
            }
        }

        foreach(AdventurerInfo adv in Temp){ //Spawn Adventurer
            GameObject spawned;
            switch(adv.job){
                case "King":
                    spawned = Instantiate(King, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                case "Mage":
                    spawned = Instantiate(Mage, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                case "Archer":
                    spawned = Instantiate(Archer, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                case "Knight":
                    spawned = Instantiate(Knight, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                default:
                    spawned = Instantiate(Knight, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
            }
            spawned.name = adv.name;

            
            Vector2[] g = new Vector2[CurrentLayout.Length+1]; //Determine Path

            for(int f=0; f<CurrentLayout.Length; f++){
                int r = Random.Range(
                    int.Parse(CurrentPaths[f].Split('a')[0][0].ToString())-1 , 
                    int.Parse(CurrentPaths[f].Split('a')[0][CurrentPaths[f].Split('a')[0].Length-1].ToString()));
                g[f] = new Vector2(FloorCoords[f, r, 0], FloorCoords[f, r, 1]);
            }
            g[CurrentLayout.Length] = new Vector2(FloorCoords[CurrentLayout.Length, 0, 0], FloorCoords[CurrentLayout.Length, 0, 1]);
            spawned.GetComponent<AdventurerBehavior>().SetWalkGoals(g, CurrentLayout.Length+1);
            spawned.GetComponent<AdventurerBehavior>().name = adv.name;
            spawned.GetComponent<AdventurerBehavior>().job = adv.job;
            spawned.GetComponent<AdventurerBehavior>().img = adv.img;
            spawned.GetComponent<AdventurerBehavior>().hp = adv.hp;
            spawned.GetComponent<AdventurerBehavior>().atk = adv.atk;
            spawned.GetComponent<AdventurerBehavior>().def = adv.def;
            spawned.GetComponent<AdventurerBehavior>().speed = adv.speed;
            spawned.GetComponent<AdventurerBehavior>().preferenceImgs = new List<Sprite>(adv.preferenceImgs);
            spawned.GetComponent<AdventurerBehavior>().itemImgs = new List<Sprite>(adv.itemImgs);
        }
        
        for(int f=0; f<CurrentLayout.Length; f++){
                for(int r=int.Parse(CurrentPaths[f].Split('a')[0][0].ToString())-1; 
                    r< int.Parse(CurrentPaths[f].Split('a')[0][CurrentPaths[f].Split('a')[0].Length-1].ToString());
                     r++)
                     {
                        
                     }
            }
    

        
    }

    // Update is called once per frame
    private bool flag = false;
    void Update()
    {
        if(!flag){
            if(CheckAdventurers.transform.childCount>0){
                foreach( Transform c in CheckAdventurers.transform){
                    flag = true;
                    if(c.gameObject.GetComponent<AdventurerBehavior>().Alive && !c.gameObject.GetComponent<AdventurerBehavior>().Arrived){
                        flag = false;
                        break;
                    }//Alive and not arrived
                }
                if(flag){
                    Debug.Log("ended");
                    EndScene();
                }
            }
        }
        
    }

    private void EndScene(){
        if(GameObject.Find("AdventurerManager")!= null){
            AdventurerInfo[] r = OutputAdventurers();
            if (r.Length == 0) {
                SceneManager.LoadScene("SettlementScene");
            } else {
                GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>().SetAdventurerList(OutputAdventurers());
                SceneManager.LoadScene("TradeScene");
            }
        }else{
            Debug.Log("can't find adv manager");
        }
        
    }

    public AdventurerInfo[] OutputAdventurers(){
        List<AdventurerInfo> r = new List<AdventurerInfo>();
        AdventurerInfo n;
        int count = 0;
        foreach( Transform c in CheckAdventurers.transform){
            if(c.gameObject.GetComponent<AdventurerBehavior>().Alive){
                n = new AdventurerInfo();
                n.name = c.gameObject.GetComponent<AdventurerBehavior>().name;
                n.job = c.gameObject.GetComponent<AdventurerBehavior>().job;
                n.img = c.gameObject.GetComponent<AdventurerBehavior>().img;
                n.hp = c.gameObject.GetComponent<AdventurerBehavior>().hp;
                n.atk = c.gameObject.GetComponent<AdventurerBehavior>().atk;
                n.def = c.gameObject.GetComponent<AdventurerBehavior>().def;
                n.speed = c.gameObject.GetComponent<AdventurerBehavior>().speed;
                n.preferenceImgs = new List<Sprite>(c.gameObject.GetComponent<AdventurerBehavior>().preferenceImgs);
                n.itemImgs = new List<Sprite>(c.gameObject.GetComponent<AdventurerBehavior>().itemImgs);
                r.Add(n);
                count++;
            }
        }
        return r.ToArray();
    }


}
