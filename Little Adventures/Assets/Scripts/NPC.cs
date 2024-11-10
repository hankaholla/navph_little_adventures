using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class NPC : MonoBehaviour
{

    private Transform player;
    private Rigidbody rb;

    [SerializeField] TextMeshProUGUI countText;


    public float detectionRadius = 50f;
    public LineRenderer lineRenderer;

    public float radius = 1f; 
    public float height = (-2f);           // Radius distance to check
    public Color radiusColor = Color.green;

    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find player by tag
        rb = GetComponent<Rigidbody>(); 

        if (lineRenderer == null)
        {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 50; // Number of points for the circle
        lineRenderer.useWorldSpace = false; // Draw in local space
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = radiusColor;
        lineRenderer.endColor = radiusColor;
    
        // Draw a circle in the radius
        DrawCircle();
    }

    void Update()
    {   
        //detectionRadius =  anxiety_Controler.proximityThreshold;
        Anxiety_circle();
    }

    void DrawCircle()
    {
        float angleStep = 360f / lineRenderer.positionCount;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            // Calculate the angle for each point in the circle
            float angle = i * angleStep * Mathf.Deg2Rad;
            
            // Set the X and Z positions for the circle
            float x = Mathf.Cos(angle) * radius;
            float z = Mathf.Sin(angle) * radius;
            
            // Set the position of each point (with adjustable height)
            Vector3 point = new Vector3(x, height, z);

            lineRenderer.SetPosition(i, point);
        }
    }

   void Anxiety_circle()
   {
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance < detectionRadius)
        {
            if (!lineRenderer.enabled) // Only update if not already enabled
            {
                lineRenderer.enabled = true; // Show the circle
                //lineRenderer.positionCount = 50; // Reset to the correct number of points
                //DrawCircle();
            }
        }
        else
        {
            //lineRenderer.positionCount = 0;
            lineRenderer.enabled = false;
        }

        
   }
}
