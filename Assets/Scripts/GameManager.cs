using System.Collections;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject hud, pauseMenu, gameOver;
    private bool isPaused = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowPauseMenu(true);
        gameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("PauseGame"))
        {
            ShowPauseMenu(!isPaused);
        }
    }
    
    public void ShowPauseMenu(bool pause)
    {
        pauseMenu.SetActive(pause);
        hud.SetActive(!pause);
        isPaused = pause;
        Time.timeScale = pause ?  0 : 1;
    }

    public void QuitGame()
    {
        Application.Quit();
        EditorApplication.isPlaying = false;
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        gameOver.SetActive(true);
    }
}
