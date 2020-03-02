using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePause : MonoBehaviour
{
    bool pause;
    public GameObject pausePanel;


    private void Start()
    {
        Time.timeScale = 1; 
    }
    // Start is called before the first frame update
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pause == false)
            {
                Time.timeScale = 0;
                pausePanel.SetActive(true);
                pause = true;
            }
            else
            {
                Time.timeScale = 1;
                pausePanel.SetActive(false);
                pause = false;
            }
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        pause = false;

    }

    public void GameExit()
    {
        Application.Quit();
    }

    public void HoumePanel()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
}
