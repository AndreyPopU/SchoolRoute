using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kid : MonoBehaviour
{
    public float speed;
    public int index;

    public bool readyToGo;
    public bool moving;
    public bool crossed;

    public Transform nextBarrier;

    private Rigidbody rb;
    private Transform GFX;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GFX = transform.GetChild(0);
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (index < 0 || GameManager.instance.gameOver)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        // Shoot a raycast if it hits something, stop
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 1))
        {
            if (moving) // If moving check for obstacles
            {
                if (hit.collider.tag == "CrossBarrier") Stop();

                if (hit.collider.tag == "Finish") // Stop and finish the game
                {
                    Stop();
                    crossed = true;
                    GameManager.instance.finished = true;
                }

                if (hit.collider.TryGetComponent(out Kid kid)) // Stop and check if that was the last kid to cross the street
                {
                    // If kid is alone, it will join the other kids when they all cross the road
                    if (kid.index == -1) GameManager.instance.nextKid = kid;

                    Stop();

                    if (kid.crossed) crossed = true;

                    // Check if that was the last kid to cross the street
                    if (kid.crossed && index == GameManager.instance.kids.Count - 1)
                    {
                        print(index + " " + (GameManager.instance.kids.Count - 1));
                        GameManager.instance.NextRoad();
                    }
                }
            }
        }
        else moving = true;


        // Once it has started moving check for kids in it's way
        if (moving) rb.velocity = Vector3.forward * speed * Time.fixedDeltaTime;

        // If crossed the road
        if (crossed)
        {
            // Turn around to wait for other kids
            if (GFX.transform.eulerAngles.y < 180) GFX.transform.Rotate(Vector3.up, 10f);
        }
        else
        {
            // Turn around to cross the road
            if (GFX.transform.eulerAngles.y > 0) GFX.transform.Rotate(Vector3.up, -10f);
        }
    }

    private void Stop()
    {
        rb.velocity = Vector3.zero;
        moving = false;
    }

    public void Flatten()
    {
        animator.SetTrigger("flatten");
        rb.velocity = Vector3.zero;
        moving = false;
    }
}
