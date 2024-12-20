using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data;

public class Quest : MonoBehaviour
{

    public string title;
    public int id;
    public string description;
    public string cutscene;

    public string EndMessage;

    [SerializeField] private float proximityThreshold;
    [SerializeField] private Image button;
    [SerializeField] private Image quest_mark;
    [SerializeField] private TextMeshProUGUI show_text;
    [SerializeField] private TextMeshProUGUI show_CutScene_text;

    [SerializeField] private List <Image> questList = new List<Image>();

    public bool show = false; 

    public bool interact = true;
    public bool finished = false;

    private bool Accept = false;
    private bool cutscene_show = false;


    private int max = 3;
    private int taken = 0;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        button.enabled = false;
        quest_mark.enabled = true;
        Hide_at_start();
    }

    void Hide_at_start()
    {
        for (int i = 0; i < questList.Count; i++) { 
            questList[i].enabled = false;
        }
    }

    // Update is called once per frame

    public void Show_Button(){
        button.enabled = true;
        show = true;      
    }

    public void Hide_Button(){     
        button.enabled = false;
        show = false;      
    }
    public void Hide_Quest_Mark(){     
        quest_mark.enabled = false;
        show = false;      
    }

    public void Show_Quest_Mark(){     
        quest_mark.enabled = false;
        show = false;      
    }

    void Update()
    {

        if (Accept)
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

            if(max == taken && !finished)
                Finish_quest();
        }

        if(cutscene_show && Input.anyKeyDown){
            cutscene_show = false;
            show_CutScene_text.gameObject.SetActive(false);
        }
    }

    void Update_quest(int taken, int max){
        string s = taken + "/" + max;
        show_text.text = s;
    }
    void Show_items(){
        for (int i = 0; i < questList.Count; i++) { 
            questList[i].enabled = true;
        }
    }

    public void Accept_quest(){
        interact = false;
        show_text.text = description;
        Accept = true;

        cutscene_Show();
        Hide_Button();
        Hide_Quest_Mark();
        Show_items();
    }

    private void cutscene_Show(){
        cutscene_show = true;
        show_CutScene_text.text = cutscene;
    }

    void Finish_quest(){
        StartCoroutine(ShowMessage(EndMessage, 1));
        show_text.gameObject.SetActive(false);
        
    }

    IEnumerator ShowMessage(string message, float duration)
    {
        // Set the text and make it visible
        show_CutScene_text.text = message;
        show_CutScene_text.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);
        finished = true;
        // Hide the text after the duration
        show_CutScene_text.gameObject.SetActive(false);
    }
}
