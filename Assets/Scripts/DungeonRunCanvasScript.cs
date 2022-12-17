using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRunCanvasScript : MonoBehaviour
{

    [SerializeField] private Transform ButtonParent;

    [SerializeField] private GameObject WizardButton;
    [SerializeField] private GameObject WarriorButton;
    [SerializeField] private GameObject WarlockButton;
    [SerializeField] private GameObject KingButton;
    private AdventurerInfo[] Temp;
    
    void Start()
    {
        if(GameObject.Find("AdventurerManager")!= null){
            Temp = GameObject.Find("AdventurerManager").GetComponent<AdventurerManager>().GetAdventurerList();
        }else{
            Temp = GameObject.Find("DungeonMapController").GetComponent<AdventurerManager>().GetAdventurerList();
        }

        Vector2 StartPoint = new Vector2(960-850, 540+450);
        int ButtNum = 0;
        foreach(AdventurerInfo adv in Temp){ //Spawn Adventurer
            GameObject spawned;
            switch(adv.job){
                case "King":
                    spawned = Instantiate(KingButton, new Vector2(StartPoint.x+200*ButtNum, StartPoint.y), Quaternion.identity, ButtonParent);
                    break;
                case "Wizard":
                    spawned = Instantiate(WizardButton, new Vector2(StartPoint.x+200*ButtNum, StartPoint.y), Quaternion.identity, ButtonParent);
                    break;
                case "Warlock":
                    spawned = Instantiate(WarlockButton, new Vector2(StartPoint.x+200*ButtNum, StartPoint.y), Quaternion.identity, ButtonParent);
                    break;
                case "Warrior":
                    spawned = Instantiate(WarriorButton, new Vector2(StartPoint.x+200*ButtNum, StartPoint.y), Quaternion.identity, ButtonParent);
                    break;
                default:
                    spawned = Instantiate(WarriorButton, new Vector2(StartPoint.x+200*ButtNum, StartPoint.y), Quaternion.identity, ButtonParent);
                    break;
            }
            spawned.name = adv.name;
            ButtNum++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
