using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonRunController : MonoBehaviour
{
    public int AdventurerCount;
    public float[] AdventurerSurviveProb;

    void Start()
    {
        AdventurerCount = 5;
        AdventurerSurviveProb = [0.8f, 0.2f, 0.5f, 0.5f, 0.5f];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
