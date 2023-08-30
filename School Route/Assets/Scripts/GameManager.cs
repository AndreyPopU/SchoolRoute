using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameOver;
    public bool finished;
    public bool hasAuthority = true;

    [Header("Crossing roads & Kids")]
    public List<Kid> kids = new List<Kid>();
    public Kid nextKid;
    public Transform currentBarrier;
    public int kidIndex = 0;
    public int roadIndex;
    public bool canPass;


    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // Temporary input
        if (hasAuthority && Input.GetKeyDown(KeyCode.Space))
        {
            canPass = true;
            currentBarrier.gameObject.SetActive(false);
        }

        if (hasAuthority && Input.GetKeyUp(KeyCode.Space))
        {
            canPass = false;
            currentBarrier.gameObject.SetActive(true);
        }

        // If Kid is in position to cross the road and player says it can pass, start moving forward
        if (canPass && kids[kidIndex].readyToGo)
        {
            kids[kidIndex].moving = true;
            kids[kidIndex].readyToGo = false;

            if (kidIndex >= kids.Count)
            {
                hasAuthority = false;
                canPass = false;
            }
        }
    }

    public void NextRoad()
    {
        if (finished)
        {
            // Prompt UI
            print("Finished");
            hasAuthority = false;
            CameraManager.instance.target = kids[0].transform.position;
            return;
        }

        // Add new kid
        kids.Insert(0, nextKid);
        currentBarrier = nextKid.nextBarrier;
        kidIndex = 0;
        roadIndex++;
        hasAuthority = true;

        canPass = false;
        foreach (Kid kid in kids)
        {
            kid.index++;
            kid.crossed = false;
            kid.readyToGo = true;
        }

        CameraManager.instance.target = kids[0].transform.position;
    }

    public void GameOver()
    {
        gameOver = true;
        // Prompt UI
        print("Game Over");
    }
}
