using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject objectToSpawn; // Assign the prefab in the inspector
    public Transform[] spawnPoints;  // Assign empty game objects as spawn points
    public float spawnInterval = 10f;

    void Start()
    {
        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            foreach (Transform spawnPoint in spawnPoints)
            {
                if (spawnPoint != null && objectToSpawn != null)
                {
                    Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
                }
            }
        }
    }
}
