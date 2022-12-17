using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRunHeadButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject hover;

    private GameObject AdventurerCollection;
    // Start is called before the first frame update
    void Start()
    {
        AdventurerCollection = GameObject.Find("AdventurerCollection");
    }

    public void TestingEnter(){
        AdventurerCollection.transform.Find(gameObject.name).transform.Find("Circle").gameObject.SetActive(true);
    }
    public void TestingExit(){
        AdventurerCollection.transform.Find(gameObject.name).transform.Find("Circle").gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
