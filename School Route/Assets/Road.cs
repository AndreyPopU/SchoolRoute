using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public int index;
    public CarSpawner[] spawner;
    public TrafficLight trafficLight;
    public GameObject crossBarrier;

    void Start()
    {
        // Assign spawners this road
        for (int i = 0; i < spawner.Length; i++) spawner[i].road = this;

        // Assign road index
        if (trafficLight) trafficLight.road = this;
    }

    void Update()
    {
        if (GameManager.instance.roadIndex - index > 2) Destroy(gameObject);
    }
}
