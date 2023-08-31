using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager instance;

    public GameObject nextPanel, retryPanel;

    private void Awake() => instance = this;

    public void StartGame()
    {
        GameManager.instance.gameEnded = false;
        GameManager.instance.roadIndex++;
    }

    public void ChangeLevel(int index) => SceneManager.LoadScene(index);

    public void IncrementLevel(int index) => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
}
