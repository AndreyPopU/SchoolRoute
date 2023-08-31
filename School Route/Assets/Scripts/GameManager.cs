using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool gameEnded;
    public bool finished;
    public bool hasAuthority = true;

    [Header("Crossing roads & Kids")]
    public List<Kid> kids = new List<Kid>();
    public Kid nextKid;
    public GameObject currentBarrier;
    public int roadIndex;
    public bool canPass;

    private void Awake() => instance = this;

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
    }

    public void NextRoad()
    {
        if (finished)
        {
            // Prompt UI
            print("Finished");
            CanvasManager.instance.nextPanel.SetActive(true);
            hasAuthority = false;
            CameraManager.instance.target = kids[0].transform.position;
            return;
        }

        // Generate new road
        GetComponent<ProceduralGeneration>().GenerateRoad();

        // Add new kid
        nextKid.transform.SetParent(transform);
        kids.Insert(0, nextKid);
        currentBarrier = nextKid.road.crossBarrier;
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
        gameEnded = true;
        // Prompt UI
        CanvasManager.instance.retryPanel.SetActive(true);
        print("Game Over");
    }
}
