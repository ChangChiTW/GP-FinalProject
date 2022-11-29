using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] private GameObject Wizard;
    [SerializeField] private GameObject Warrior;
    [SerializeField] private GameObject Warlock;
    [SerializeField] private GameObject King;




    private int[] DungeonLayout1 = {2, 3, 4, 3};
    private int[] DungeonLayout2 = {3, 4, 3, 2};

    private int[] CurrentLayout;

    private AdventurerInfo[] Temp;

    private float[,,] FloorCoords = new float[10, 5, 2]; //[floor, which one, x/y]
    void Start()
    {
        int seed = Random.Range(1, 1+2);
        if(seed == 1){
            CurrentLayout = DungeonLayout1;
        } else if(seed == 2){
            CurrentLayout = DungeonLayout2;
        }

        Temp = gameObject.GetComponent<AdventurerManager>().GetAdventurerList();
       
        float LevelLength = 4; //How long (x) is one dungeon Level

        for (int i=0; i<=CurrentLayout.Length; i++){ // i is current dungeon Level
            if(i == CurrentLayout.Length){
                FloorCoords[i, 0, 0] = StartPoint.position.x + LevelLength*(i+1);
                FloorCoords[i, 0, 1] = 0;
                Instantiate(BossEncounter, new Vector3(StartPoint.position.x + LevelLength*(i+1), 0, 0), Quaternion.identity, MapParent);
            }else{
                float BranchCount = CurrentLayout[i]; //How many branches this dungeon level
                float angle = Mathf.PI/(BranchCount+1);

                for(int a = 0; a< BranchCount; a++){ //each line same X
                    float CurrStartX = StartPoint.position.x + LevelLength*i;
                    float CurrEndX = StartPoint.position.x + LevelLength*(i+1);
                    float CurrAngle = Mathf.PI/2 - angle*(a+1) + Random.Range(-100, 100)*0.002f;
                    float CurrEndY = LevelLength*Mathf.Sin(CurrAngle);
                    FloorCoords[i, a, 0] = CurrEndX;
                    FloorCoords[i, a, 1] = CurrEndY;
                    Instantiate(Random.Range(0, 10)>1 ? MonsterEncounter : SmallChestEncounter, new Vector3(CurrEndX, CurrEndY, 0), Quaternion.identity, MapParent);
                }
            }
        }

        foreach(AdventurerInfo adv in Temp){ //Spawn Adventurer
            GameObject spawned;
            switch(adv.job){
                case "King":
                    spawned = Instantiate(King, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                case "Wizard":
                    spawned = Instantiate(Wizard, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                case "Warlock":
                    spawned = Instantiate(Warlock, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                case "Warrior":
                    spawned = Instantiate(Warrior, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
                default:
                    spawned = Instantiate(Warrior, StartPoint.transform.position, Quaternion.identity, AdventurerParent);
                    break;
            }
            spawned.name = adv.name;

            
            Vector2[] g = new Vector2[CurrentLayout.Length+1]; //Determine Path

            for(int f=0; f<CurrentLayout.Length; f++){
                int r = Random.Range(0, CurrentLayout[f]);
                g[f] = new Vector2(FloorCoords[f, r, 0], FloorCoords[f, r, 1]);
            }
            g[CurrentLayout.Length] = new Vector2(FloorCoords[CurrentLayout.Length, 0, 0], FloorCoords[CurrentLayout.Length, 0, 1]);
            spawned.GetComponent<AdventurerBehavior>().SetWalkGoals(g, CurrentLayout.Length+1);
            spawned.GetComponent<AdventurerBehavior>().SetSpeed(adv.atk*0.024f);
            spawned.GetComponent<AdventurerBehavior>().HP = adv.hp;

        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
