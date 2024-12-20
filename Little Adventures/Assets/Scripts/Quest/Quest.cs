using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Data;
using System.Security.Cryptography.X509Certificates;

public class Quest : MonoBehaviour
{

    public string title;
    public int id;
    public string description;
    public string cutscene;

    public string EndMessage;

    [SerializeField] private Quest_Logic QuestLogic;
    [SerializeField] private Image button;
    [SerializeField] private Image quest_mark;
    [SerializeField] private TextMeshProUGUI show_text;
    [SerializeField] private TextMeshProUGUI show_CutScene_text;
    [SerializeField] private TextMeshProUGUI Money;

    //[SerializeField] private List <Image> questList = new List<Image>();

    public bool show = false; 

    public bool interact = true;
    public bool finished = false;

    private bool cutscene_show = false;

    void Start()
    {
        button.enabled = false;
        quest_mark.enabled = true;
    }

    void Update()
    {

    }
    
    public void Add_money(int m){
        if (int.TryParse(Money.text, out int number))
        {
            number += m; // Increment the number
            Money.text = number.ToString(); // Update the text with the new number
        }
    }

    public void Remove_money(int m){
        if (int.TryParse(Money.text, out int number))
        {
            number -= m; // Increment the number
            Money.text = number.ToString(); // Update the text with the new number
        }
    }

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

    public void Accept_quest(){
        interact = false;
        show_text.text = description;

        cutscene_Show(cutscene);
        Hide_Button();
        Hide_Quest_Mark();

    
        QuestLogic.Accept_quest();
        
            
        //Show_items();
    }

    private void cutscene_Show(){
        cutscene_show = true;
        show_CutScene_text.text = cutscene;
    }

    public void cutscene_Show(string A){
        cutscene_show = true;
        StartCoroutine(ShowMessage(A, 1));
    }

    public void Update_quest(string upd){
        show_text.text = upd;
    }

    public void Finish_quest(){
        StartCoroutine(Show_Last_Message(EndMessage, 3));
        show_text.gameObject.SetActive(false);
        
    }

    IEnumerator ShowMessage(string message, float duration)
    {
        // Set the text and make it visible
        show_CutScene_text.text = message;
        show_CutScene_text.gameObject.SetActive(true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);
        //finished = true;
        // Hide the text after the duration
        show_CutScene_text.gameObject.SetActive(false);
    }

    IEnumerator Show_Last_Message(string message, float duration)
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
