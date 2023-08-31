using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralGeneration : MonoBehaviour
{
    public int generationLimit; 

    public GameObject[] roadSegments;
    public GameObject schoolZonePrefab;

    public float startOffset = 5;
    public float offset;

    public int roadIndex;

    void Start()
    {
        for (int i = 0; i < 4; i++) GenerateRoad();
    }

    public void GenerateRoad()
    {
        if (roadIndex >= generationLimit)
        {
            if (roadIndex == generationLimit) GenerateSchoolZone();
            return;
        }

        // Decide on offset - road width + kid distance * kids count
        offset = (7 + .65f * roadIndex + 1.5f) * roadIndex + startOffset;

        // Pick random road and instantiate it
        Road road = Instantiate(roadSegments[Random.Range(0, roadSegments.Length)], 
            Vector3.forward * offset + Vector3.up * -.5f, Quaternion.identity).GetComponent<Road>();

        if (roadIndex == 0) // First road
        {
            Kid firstKid = road.GetComponentInChildren<Kid>();

            // Assign player and barrier to GameManager
            GameManager.instance.kids.Add(firstKid);
            GameManager.instance.currentBarrier = road.crossBarrier;

            // Setup kid for crossing the road
            firstKid.transform.position -= Vector3.forward * 2;
            firstKid.crossed = false;
            firstKid.index = 0;
            firstKid.transform.SetParent(GameManager.instance.transform);
        }

        road.index = roadIndex++;
    }

    public void GenerateSchoolZone()
    {
        // Decide on offset - road width + kid distance * kids count
        offset = (7 + .65f * (roadIndex - 1) + 1.5f) * (roadIndex - 1) + startOffset;

        // Pick random road and instantiate it
        Instantiate(schoolZonePrefab, Vector3.forward * offset + Vector3.up * -.5f, Quaternion.identity).GetComponent<Road>();
    }
}
