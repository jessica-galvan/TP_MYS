using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject CreditsScreen;
    [SerializeField]
    private GameObject MenuScreen;
    [SerializeField]
    private GameObject InstructionsScreen;
    private bool isMenu;
    private bool credits;
    private bool instructions;

     void Start()
    {
        Time.timeScale = 1f;
        isMenu = true;
        credits = false;
        instructions = false;
        MenuScreen.SetActive(true); 
        CreditsScreen.SetActive(false);
        InstructionsScreen.SetActive(false);
    }

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
        PlayerBehaviour.Restart();
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting game...");
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
        MenuScreen.SetActive(true);
    } 
}
