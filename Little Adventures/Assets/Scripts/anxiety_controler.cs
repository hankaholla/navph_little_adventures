using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

[RequireComponent(typeof(Anxiety_Bar))]
public class anxiety_controler : MonoBehaviour
{
    //[SerializeField] TextMeshProUGUI countText;
    [SerializeField] string anxiety_tag;

    public float proximityThreshold = 5f;  // Set the distance threshold for being "too close"

    public int Max_Anxiety = 5;
    private float proximityTimer = 0f;
    public float decayRate = 1f;

    public Anxiety_Bar anxiety_bar;

    public float upRate = 2f;

    private bool decay = true;  

    public anxiety_Fade cameraController;

    private int up_choice = 0;
    private int down_choice = 0;

    public float maxFadeAlpha1 = 0.5f;
    public float maxFadeAlpha2 = 0.7f;
    public float maxFadeAlpha3 = 0.9f;

    void Start()
    {
        // Get the CameraController script from the Main Camera
        anxiety_bar.SetMaxAnxiety(Max_Anxiety);
        anxiety_bar.SetAnxiety(0);
    }
    void Update()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(anxiety_tag);
        
        decay = true;
        foreach (GameObject npc in npcs)
        {
            float distance = Vector3.Distance(transform.position, npc.transform.position);

            if (distance < proximityThreshold)
                decay = false;
        }

        if (!decay)
        {
            proximityTimer += upRate * Time.deltaTime;

            if(proximityTimer > Max_Anxiety)
                proximityTimer = Max_Anxiety;
            anxiety_bar.SetAnxiety((float)(Mathf.Round(proximityTimer * 100) / 100.0));
        }

        if (decay && proximityTimer > 0)
        {
            // Decay timer when player is outside the radius
            proximityTimer -= decayRate * Time.deltaTime;
            proximityTimer = Mathf.Max(proximityTimer, 0f); // Clamp to 0

            if(proximityTimer < 0)
                proximityTimer = 0;
            anxiety_bar.SetAnxiety((float)(Mathf.Round(proximityTimer * 100) / 100.0));
        }
        proximityTimer = (float)(Mathf.Round(proximityTimer * 100) / 100.0);
        //countText.text = proximityTimer.ToString();

        Check_anxiety_level();
    }

    void Check_anxiety_level()
    {

            if (decay)
            {
                if (proximityTimer < 3 && down_choice != 0)
                {
                    down_choice = 0;
                    cameraController.TriggerFadeIn(maxFadeAlpha1, 0);
                    Debug.Log("controlel");
                }

                if (proximityTimer >= 3 && proximityTimer < 4 && down_choice != 1)
                {
                    down_choice = 1;
                    cameraController.TriggerFadeIn(maxFadeAlpha2, maxFadeAlpha1);
                    Debug.Log("controlel");
                }

                if (proximityTimer >= 4 && proximityTimer < 5 && down_choice != 2)
                {
                    down_choice = 2;
                    cameraController.TriggerFadeIn(maxFadeAlpha3, maxFadeAlpha2);
                    Debug.Log("controlel");
                }
            }

            if(!decay)
            {
                if (proximityTimer >= 2 && proximityTimer < 3 && up_choice != 1)
                {   
                    up_choice = 1;
                    cameraController.TriggerFadeOut(0, maxFadeAlpha1);
                    Debug.Log("maxFadeAlpha1");
                }
                if (proximityTimer >= 3 && proximityTimer < 4 && up_choice != 2)
                {
                    up_choice = 2;
                    cameraController.TriggerFadeOut(maxFadeAlpha1, maxFadeAlpha2);
                    Debug.Log("maxFadeAlpha2");
                }
                if (proximityTimer >= 4 && up_choice != 3)
                {
                    up_choice = 3;
                    cameraController.TriggerFadeOut(maxFadeAlpha2, maxFadeAlpha3);
                    Debug.Log("maxFadeAlpha3");
                }
            }    
        }
            
    }

