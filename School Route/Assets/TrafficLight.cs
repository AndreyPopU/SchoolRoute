using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    public Transform carBarrier;
    [HideInInspector] public float redLight;
    [HideInInspector] public Road road;

    private void Start() => redLight = Random.Range(2f, 4f);

    private void Update()
    {
        if (GameManager.instance.roadIndex != road.index || GameManager.instance.gameEnded) return;

        if (redLight > 0) redLight -= Time.deltaTime;
        else
        {
            carBarrier.gameObject.SetActive(true);
            GetComponent<Renderer>().material.color = Color.green;
        }
    }
}
