using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class AnxietyCircleController : MonoBehaviour
{
    // references to player and this NPC object
    private Transform playerTransform;
    private Transform npcTransform;

    private float anxietyCircleRadius;

    public LineRenderer lineRenderer;
    [SerializeField] public Color anxietyCircleColor = Color.green;

    void Start()
    {
        anxietyCircleRadius = AnxietyController.proximityThreshold;
        npcTransform = gameObject.transform;

        // Get the player by tag
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // initialize line renderer
        if (lineRenderer == null)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }
        lineRenderer.positionCount = 50; // Number of points for the circle
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.startColor = anxietyCircleColor;
        lineRenderer.endColor = anxietyCircleColor;
    }

    void Update()
    {   
        // show anxiety circle around NPC if player in vicinity
        float distance = Vector3.Distance(transform.position, playerTransform.position);
        if (distance < anxietyCircleRadius)
        {
            lineRenderer.enabled = true;
            DrawAnxietyCircle();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    // draw circle around the npc slightly above the ground level 
    void DrawAnxietyCircle()
    {
        float angleStep = 360f / lineRenderer.positionCount;
        float halfNPCHeight = 0.5f * npcTransform.localScale.y;

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            // Calculate the angle for each point in the circle
            float angle = i * angleStep * Mathf.Deg2Rad;
            
            // Set the X and Z positions for the circle
            float x = Mathf.Cos(angle) * anxietyCircleRadius;
            float z = Mathf.Sin(angle) * anxietyCircleRadius;
            
            // Set the position of each point
            Vector3 point = new Vector3(x + npcTransform.position.x, npcTransform.position.y - halfNPCHeight + 0.1f, npcTransform.position.z + z);

            lineRenderer.SetPosition(i, point);
        }
    }

}
