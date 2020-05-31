using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MonoBehaviour
{

    public static bool GameIsPause = false;
    [SerializeField]
    public GameObject PauseMenuUI;
    [SerializeField]
    public GameObject DeathScreen;
    [SerializeField]
    public GameObject VictoryScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPause = true;
    }

    public void LoadMenu()
    {
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("Menu");
        Debug.Log("Loading menu, menu not ready yet...");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    //DEATH MENU
    public void Restart()
    {
        //OJO QUE SI HACEMOS VARIOS NIVELES, TENEMOS QUE ASEGURARNOS DE EN QUE NIVEL ESTA
        //Time.timeScale = 1f;
        //SceneManager.LoadScene("Level");
        Debug.Log("Cargar el nivel de nuevo");
    }


}
