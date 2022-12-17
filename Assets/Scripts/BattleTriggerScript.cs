using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTriggerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        other.gameObject.transform.GetComponent<AdventurerBehavior>().EnterBattle();
    }
}
