using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneHandler
{
    public static int SceneIndex() { return SceneManager.GetActiveScene().buildIndex; }

    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneIndex());
    }

    public static void GoToMenu() { SceneManager.LoadScene("MainMenu"); }
    public static void GoToGame() { SceneManager.LoadScene("GameBoard"); }
    public static void GoToSettings() { SceneManager.LoadScene("SettingsScene"); }
}