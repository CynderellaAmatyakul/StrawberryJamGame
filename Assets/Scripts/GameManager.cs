using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;

    private bool isPaused = false;

    public void TogglePause()
    {
        isPaused = !isPaused;

        pauseMenuUI.SetActive(isPaused);

        if (isPaused)
        {
            Time.timeScale = 0f; // Freeze game time
            AudioListener.pause = true;
        }
        else
        {
            Time.timeScale = 1f; // Resume normal time
            AudioListener.pause = false;
        }
    }
}
