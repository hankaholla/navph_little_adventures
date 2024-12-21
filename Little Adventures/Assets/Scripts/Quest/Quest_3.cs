using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data;
public class Quest_3 : Quest_Logic
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private List <Image> questList = new List<Image>();
    [SerializeField] private Quest quest; 
    [SerializeField] private float proximityThreshold;

    private bool Accept = false;
    private bool Finished = false;
    private GameObject player;

    private string show_update;
    private bool cutscene = true;


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
        Debug.Log(questList.Count);
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
                    questList[i].enabled = false;
                    Destroy(questList[i]);
                    questList.RemoveAt(i);

                    Finished = true;
                }
            }

            if(Finished)
                Finish_quest();
        }
    } 

    void Finish_quest(){
        Finished = true;
        quest.Finish_quest();
    }
}
 