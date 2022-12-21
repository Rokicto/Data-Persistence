using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public InputField playerNameField;

    public void SetPlayerName(string name)
    {
        GameManager.Instance.playerName = name;
    }

    public void StartClicked()
    {
        SceneManager.LoadScene("main");
    }

    public void HighScoreClicked()
    {
        SceneManager.LoadScene("highscores");
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
