using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public static SceneHandler Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int SceneIndex() { return SceneManager.GetActiveScene().buildIndex; }

    public void RestartScene()
    {
        SceneManager.LoadScene(SceneIndex());
    }

    public void GoToMenu() { SceneManager.LoadScene("MainMenu"); }
    public void GoToGame() { SceneManager.LoadScene("GameBoard"); }
    public void GoToSettings() { SceneManager.LoadScene("SettingsScene"); }
}