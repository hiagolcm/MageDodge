using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour {

    public GameObject pauseMenuUi;
    public static bool GameIsPaused = false;

    private void Start()
    {
        Resume();
    }

    // Update is called once per frame
    void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMenuUi.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUi.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
