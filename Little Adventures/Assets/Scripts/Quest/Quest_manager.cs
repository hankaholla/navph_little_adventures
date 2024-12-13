using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
public class Quest_manager : MonoBehaviour
{

    public static Quest_manager quest_manager; 
    public static float proximityThreshold;
    

    public List <Quest> questList = new List<Quest>();
    public List <Quest> currentQuestList = new List<Quest>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
