using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject CreditsScreen;
    [SerializeField]
    private GameObject ControlsScreen;
    [SerializeField]
    private GameObject MenuScreen;
    [SerializeField]
    private GameObject InstructionsScreen;
    private bool isMenu;
    private bool credits;
    private bool instructions;
    private bool controls;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
        isMenu = true;
        credits = false;
        instructions = false;
        MenuScreen.SetActive(true); 
        ControlsScreen.SetActive(false);
        CreditsScreen.SetActive(false);
        InstructionsScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Para el Menu de Pausa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenu)
            {
                GoBack();
            } 
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
    }

    public void ShowControls()
    {
        MenuScreen.SetActive(false);
        isMenu = false;
        controls = true;
        ControlsScreen.SetActive(true);
    }
    public void ShowCredits()
    {
        MenuScreen.SetActive(false);
        isMenu = false;
        credits = true;
        CreditsScreen.SetActive(true);
    }

    public void ShowIntructions()
    {
        MenuScreen.SetActive(false);
        isMenu = false;
        instructions = true;
        InstructionsScreen.SetActive(true);
    }
    public void GoBack()
    {
        isMenu = true;
        if (credits)
        {
            credits = false;
            CreditsScreen.SetActive(false);
        }
        else if (instructions)
        {
            instructions = false;
            InstructionsScreen.SetActive(false);
        }
        else if (controls)
        {
            controls = false;
            ControlsScreen.SetActive(false);
        }
        MenuScreen.SetActive(true);
    } 
}
