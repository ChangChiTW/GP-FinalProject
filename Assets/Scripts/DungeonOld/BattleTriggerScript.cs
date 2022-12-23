using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> Battling;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        Battling.Add(other.gameObject.name);
        other.gameObject.transform.GetComponent<AdventurerBehavior>().EnterBattle();
    }
}
