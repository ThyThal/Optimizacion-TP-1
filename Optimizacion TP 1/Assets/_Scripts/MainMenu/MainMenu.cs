using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenu : MonoBehaviourUI
{
    
    [SerializeField] GameObject panelMainMenu;
    [SerializeField] private Credits _credits;
    private string levelScene = "GAMEPLAY";

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
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#endif
        Application.Quit();
    }
    public void LoadMainMenu()
    {
        panelMainMenu.SetActive(true);
    }

    public void ShowCredits()
    {
        _credits.ShowPopup();
    }
}
