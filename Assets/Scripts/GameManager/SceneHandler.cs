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

    // by PrzemekBarczyk
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        int nextIndex = (currentIndex + 1) % sceneCount;
        SceneManager.LoadScene(nextIndex);
    }
}