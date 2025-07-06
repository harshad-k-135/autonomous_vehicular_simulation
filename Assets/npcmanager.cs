using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficManager : MonoBehaviour
{
    public GameObject[] trafficVehicles;
    public Transform[] lanes;
    private float spawnTimer;

    void Start()
    {
        spawnTimer = Random.Range(30f, 60f);
        StartCoroutine(SpawnTraffic());
    }

    private IEnumerator SpawnTraffic()
    {
        yield return new WaitForSeconds(spawnTimer);
        
        while (true)
        {
            int laneIndex = Random.Range(0, lanes.Length);
            int vehicleIndex = Random.Range(0, trafficVehicles.Length);
            Instantiate(trafficVehicles[vehicleIndex], lanes[laneIndex].position, Quaternion.identity);
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}