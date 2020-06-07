using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{

    public static bool GameIsPause = false;
    [SerializeField]
    private GameObject PauseMenuUI;
    [SerializeField]
    private GameObject DeathScreen;
    [SerializeField]
    private GameObject VictoryScreen;
    [SerializeField]
    private GameObject HUDScreen;
    [SerializeField]
    private GameObject[] levels;
    //public static string level;
    private bool level1 = false;
    private bool level2 = false;

    void Start()
    {
        Time.timeScale = 1f;
        levels = GameObject.FindGameObjectsWithTag("Level");
        if (levels[0].name == "Level1")
        {
            level1 = true;
        } 
        else if(levels[0].name == "Level2")
        {
            level2 = true;
        }
    }    

    void Update()
    {
        //Para el Menu de Pausa
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (GameIsPause)
            {
                Resume();
            } else
            {
                Pause();
            }
        }
    }

    //MENU DE PAUSA
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPause = false;
        HUDScreen.SetActive(true);
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
        HUDScreen.SetActive(false);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    //DEATH MENU
    public void Restart()
    {
        Time.timeScale = 1f;
        if (level2)
        {
            SceneManager.LoadScene("Level2");
        }
        else if(level1)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        if (level1)
        {
            SceneManager.LoadScene("Level1");
        }
        else if (level1)
        {
            SceneManager.LoadScene("Level2");
        }
    }

}
