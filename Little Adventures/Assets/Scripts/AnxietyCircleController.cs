using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class AnxietyCircleController : MonoBehaviour
{
    // references to player and this NPC object
    private Transform playerTransform;
    private Transform npcTransform;

    private float anxietyCircleRadius = AnxietyController.proximityThreshold;

    public LineRenderer lineRenderer;
    [SerializeField] public Color anxietyCircleColor = Color.green;

    // nav mesh agent fields
    private NavMeshAgent agent;
    [SerializeField] public float maxMoveDistance = 10f;   // Max distance for random movement
    [SerializeField] public float movementWaitTime = 10f;  // Time to wait before moving again
    private float timer;

    void Start()
    {
        npcTransform = gameObject.transform;

        // initialize NPC with random size and color
        float randomXZ = UnityEngine.Random.Range(1f, 2f);  // the same random number for X and Z coordinates to make NPC symmetrical
        npcTransform.localScale = new Vector3(randomXZ, UnityEngine.Random.Range(1f, 1.5f), randomXZ);
        
        agent = GetComponent<NavMeshAgent>();
        // initialize timer randomly to desync NPC movements
        timer = UnityEngine.Random.Range(0.0f, movementWaitTime);  

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
        // move the NPC randomly
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                MoveToRandomPoint();
                timer = UnityEngine.Random.Range(0.0f, movementWaitTime);
            }
        }

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

    void MoveToRandomPoint()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * maxMoveDistance;
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, maxMoveDistance, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position); // Move the NPC to a valid NavMesh point
        }
    }

}
