using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Popup : MonoBehaviourUI
{
    [SerializeField] private TMP_Text _deaths;
    [SerializeField] private TMP_Text _time;

    public void ShowPopup()
    {
        this.gameObject.SetActive(true);
        _deaths.text = $"Amount of Deaths: {GameManager.Instance.LevelManager.PlayerDeaths}";
        _time.text = $"Time: {GameManager.Instance.LevelManager.GetFormattedTime()}";
    }

    public void HidePopup()
    {
        this.gameObject.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
