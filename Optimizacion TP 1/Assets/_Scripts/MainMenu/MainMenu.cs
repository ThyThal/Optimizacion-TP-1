using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviourUI
{
    
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] GameObject panelCredits;
    private string levelScene = "Main";

    public override void Awake()
    {
        base.Awake();
        LoadMainMenu();
    }

    public void LoadSceneLevel()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadCredits()
    {
        panelCredits.SetActive(true);
        panelMainMenu.SetActive(false);
    }
    public void LoadMainMenu()
    {
        panelMainMenu.SetActive(true);
        panelCredits.SetActive(false);
    }
}
