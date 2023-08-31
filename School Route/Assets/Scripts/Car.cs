using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [HideInInspector]public Road road;
    public float speed;
    public bool stopsAtCrosswalks;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (CanvasManager.instance.retryPanel.activeInHierarchy) return;

        if (stopsAtCrosswalks)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 2))
            {
                // Stop and wait for kids to cross the road
                if ((hit.collider.tag == "CrossBarrier" && GameManager.instance.roadIndex == road.index && 
                    !GameManager.instance.gameEnded) || hit.collider.GetComponent<Car>()) 
                    rb.velocity = Vector3.zero;
                else rb.velocity = Vector3.right * speed * Time.fixedDeltaTime;
            }
            else rb.velocity = Vector3.right * speed * Time.fixedDeltaTime;
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.right, out hit, 2))
            {
                // Stop and wait for car in front to move
                if (hit.collider.GetComponent<Car>()) rb.velocity = Vector3.zero;
                else rb.velocity = Vector3.right * speed * Time.fixedDeltaTime;
            }
            else rb.velocity = Vector3.right * speed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Kid kid))
        {
            kid.Flatten();
            GameManager.instance.GameOver();
            rb.velocity = Vector3.zero;
        }
    }
}
