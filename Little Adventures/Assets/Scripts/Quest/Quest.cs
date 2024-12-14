using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Quest : MonoBehaviour
{

    public string title;
    public int id;
    public string description;
    public string cutscene;
    [SerializeField] private Image button;
    [SerializeField] private TextMeshProUGUI show_text ;

    public bool show = false; 

    public bool interact = true;



    void Start()
    {
        button.enabled = false;
    }

    // Update is called once per frame

    public void Show_Button()
    {
        button.enabled = true;
        show = true;
            
    }

    public void Hide_Button()
    {     
        button.enabled = false;
        show = false;      
    }
    void Update()
    {
        
    }

    public void Accept_quest()
    {
        interact = false;
        show_text.text = description;
        Hide_Button();
    }

    void Finish_quest()
    {

    }
}
