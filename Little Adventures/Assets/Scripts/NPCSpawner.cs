using UnityEngine;
using UnityEngine.AI;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField] private GameObject npcPrefab;
    [SerializeField] private int npcCount = 10;
    [SerializeField] private float spawnRadius = 50f;
    private Transform spawnCenter;

    private void Start()
    {
        spawnCenter = gameObject.transform;
        SpawnNPCs();
    }

    private void SpawnNPCs()
    {
        for (int i = 0; i < npcCount; i++)
        {
            Vector3 randomPosition = GetRandomNavMeshPosition();
            if (randomPosition != Vector3.zero)
            {
                Instantiate(npcPrefab, randomPosition, Quaternion.identity);
                // Debug.Log("Spawned NPC on position: " + randomPosition);
            }
        }
    }

    private Vector3 GetRandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * spawnRadius;
        randomDirection += spawnCenter.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, spawnRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        else
        {
            return Vector3.zero;
        }
    }
}
