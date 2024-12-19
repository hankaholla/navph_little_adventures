using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data;

public class Quest_1 : Quest_Logic
{

    [SerializeField] private List <Image> questList = new List<Image>();
    [SerializeField] private Quest quest; 

    private int max = 3;
    private int taken = 0;

    private bool Accept = false;
    private GameObject player;

    private string show_update;

    private bool Finished = false;

    [SerializeField] private float proximityThreshold;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hide_at_start();
    }

    void Hide_at_start()
    {
        for (int i = 0; i < questList.Count; i++) { 
            questList[i].enabled = false;
        }
    }

    public override void Accept_quest()
    {
        //interact = false;
        //show_text.text = description;
        Accept = true;
        Show_items();
    }

    void Show_items(){
        for (int i = 0; i < questList.Count; i++) { 
            questList[i].enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Accept  && !Finished)
        {
            player = GameObject.FindWithTag("Player");

            for (int i = 0; i < questList.Count; i++) 
            { 
                float distance = Vector3.Distance(questList[i].transform.position, player.transform.position);

                if (distance < proximityThreshold)
                {
                    taken++;
                    Update_quest(taken, max);
                    questList[i].enabled = false;
                    Destroy(questList[i]);

                    questList.RemoveAt(i);
                }
            }

            if(max == taken && !Finished)
                Finish_quest();
        }
    }

    void Update_quest(int taken, int max){
        show_update = taken + "/" + max;
        quest.Update_quest(show_update);
    }

    void Finish_quest(){
        Finished = true;
        quest.Finish_quest();
    }
}
