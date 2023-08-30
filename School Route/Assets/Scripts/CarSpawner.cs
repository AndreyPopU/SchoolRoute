using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public int roadIndex;
    public float intervalMax;
    public float intervalMin;
    public bool fastCars;

    private float interval;

    void Start()
    {
        interval = Random.Range(1, 4);
    }

    void Update()
    {
        if (GameManager.instance.gameOver) return;

        if (interval > 0) interval -= Time.deltaTime;
        else
        {
            // Spawn car
            Car car = Instantiate(carPrefab, transform.position, Quaternion.identity).GetComponent<Car>();

            // Choose direction
            if (transform.position.x > 0) car.speed *= -1;

            // If spawner spawns fast cars, tripple the speed
            if (fastCars) car.speed *= 3;

            // Assign road index
            car.roadIndex = roadIndex;

            // Asign 50/50 chance to stop at a crosswalk
            car.stopsAtCrosswalks = Random.Range(0, 101) < 50 ? true : false;

            interval = Random.Range(intervalMin, intervalMax);
        }
    }
}
