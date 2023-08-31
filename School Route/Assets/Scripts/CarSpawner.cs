using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public int carLimit;

    [Header("Speed")]
    public float difficultyMultiplier = 15;
    public float defaultSpeed = 300;
    public float maxSpeed = 800;

    public bool stopAtCrosswalks;
    public float intervalMax;
    public float intervalMin;
    public bool fastCars;

    [HideInInspector] public Road road;
    private float interval;
    private int carsSpawned;

    void Start()
    {
        interval = Random.Range(.5f, 1.5f);
    }

    void Update()
    {
        if (CanvasManager.instance.retryPanel.activeInHierarchy) return;

        if (interval > 0) interval -= Time.deltaTime;
        else
        {
            // Spawn car
            Car car = Instantiate(carPrefab, transform.position, Quaternion.identity, transform).GetComponent<Car>();

            // Apply Progressive Difficulty
            car.speed += defaultSpeed + (road.index * difficultyMultiplier);

            // If spawner spawns fast cars, tripple the speed
            if (fastCars) car.speed *= 3;

            // Cap car speed
            car.speed = car.speed > maxSpeed ? maxSpeed : car.speed;

            // Choose direction
            if (transform.position.x > 0) car.speed *= -1;

            // Assign road index
            car.road = road;

            if (stopAtCrosswalks) car.stopsAtCrosswalks = true;
            else
            {
                // Asign 50/50 chance to stop at a crosswalk
                car.stopsAtCrosswalks = Random.Range(0, 101) < 50 ? true : false;
            }

            // Limit
            carsSpawned++;
            if (carLimit > 0 && carsSpawned >= carLimit) Destroy(this);

            interval = Random.Range(intervalMin, intervalMax);
        }
    }
}
