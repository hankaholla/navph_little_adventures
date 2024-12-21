using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data;

public class Quest_4 : Quest_Logic
{
    [SerializeField] private List <Image> questList = new List<Image>();
    [SerializeField] private Quest quest; 
    [SerializeField] private float proximityThreshold;

    [SerializeField] private string city_scene;
    [SerializeField] private string shop_scene;

    private bool Accept = false;
    private bool Finished = false;
    private GameObject player;

    private string show_update;
    private bool cutscene = true;

    private bool inshop = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Accept  && !Finished)   {
            //player = GameObject.FindWithTag("Player");

            StartCoroutine(ExecuteAfterDelay(10));
        }

        if(Finished)
            Finish_quest();

        
    }

    IEnumerator ExecuteAfterDelay(int delay)
    {
        yield return new WaitForSeconds(delay);
        Finished = true;
    }

    public override void Accept_quest()
    {
        //interact = false;
        //show_text.text = description;
        Accept = true;
        
    }

    void Finish_quest(){
        Finished = true;
        quest.Finish_quest();
    }
}
