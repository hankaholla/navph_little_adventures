using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data;

public class Quest_2 : Quest_Logic
{
    // Vyskoč na miesto a sprav fotku
    [SerializeField] private List <Image> questList = new List<Image>();
    [SerializeField] private Quest quest; 
    [SerializeField] private float proximityThreshold;

    private bool Accept = false;
    private bool Finished = false;
    private GameObject player;

    private string show_update;
    private bool cutscene = true;


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
                    if (cutscene)
                    {
                        Update_quest_cutscene();
                        cutscene = false;
                    }
                    if(Input.GetMouseButtonDown(0)){
                        Debug.Log("WTF");
                        Destroy(questList[i]);
                        questList.RemoveAt(i);
                        Finish_quest();
                    }
                }
            }
        }
    }

    void Update_quest_cutscene(){
        quest.cutscene_Show("Výborne, teraz sa pozri na ten krásny výhlad, nechceš si spraviť fotku?");
        quest.Update_quest("Odfoď sa");
    }

    void Finish_quest(){
        Finished = true;
        quest.Finish_quest();
    }
}
