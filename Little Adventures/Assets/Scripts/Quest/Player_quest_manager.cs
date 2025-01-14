using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player_quest_manager : MonoBehaviour
{
    [SerializeField] Quest_manager quest_Manager;
    public List <Quest> quest_list;

    [SerializeField] float proximityThreshold;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        List <Quest> quest_list = quest_Manager.questList;
        
        foreach (Quest quest in quest_list)
        {
            float distance = Vector3.Distance(transform.position, quest.transform.position);

            //Debug.Log(distance);

            if (distance < proximityThreshold && quest.interact)
            {
                quest.Show_Button();

                if(Input.GetKeyDown(KeyCode.C))
                {
                    quest.Accept_quest();
                }
            }
            else if (distance > proximityThreshold && quest.show)
            {
                quest.Hide_Button();
            }

            //on press x
            //quest.npc_quest_Manager.Accept_quest();
                
        } 

    }
}
