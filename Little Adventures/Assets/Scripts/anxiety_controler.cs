using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
public class anxiety_controler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countText;
    [SerializeField] string anxiety_tag;


    public float proximityThreshold = 5f;  // Set the distance threshold for being "too close"

    private float proximityTimer = 0f;
    public float decayRate = 1f;

    public float upRate = 2f;

    private bool decay = true;  

    public anxiety_Fade cameraController;

    private bool fading = false;

    void Start()
    {
        // Get the CameraController script from the Main Camera
    }
    void Update()
    {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag(anxiety_tag);
        
        decay = true;
        foreach (GameObject npc in npcs)
        {
            float distance = Vector3.Distance(transform.position, npc.transform.position);

            if (distance < proximityThreshold)
            {
                decay = false;
                proximityTimer += upRate * Time.deltaTime;
            }
        }

        if (decay && proximityTimer > 0)
        {
            // Decay timer when player is outside the radius
            proximityTimer -= decayRate * Time.deltaTime;
            proximityTimer = Mathf.Max(proximityTimer, 0f); // Clamp to 0

            if(proximityTimer < 0)
                proximityTimer = 0;
        }
        proximityTimer = (float)(Mathf.Round(proximityTimer * 100) / 100.0);
        countText.text = proximityTimer.ToString();

        Check_anxiety_level();
    }

    void Check_anxiety_level()
    {
        if (proximityTimer < 2)
        {
            if (fading)
            {
                fading = false;
                cameraController.TriggerFadeIn();
                Debug.Log("controlel");
            }
            return;
        }
        else
        {
            if (!fading)
            {
                
                fading = true;
                cameraController.TriggerFadeOut();
                Debug.Log("controlel");
            }
                
        }
            
    }


}
