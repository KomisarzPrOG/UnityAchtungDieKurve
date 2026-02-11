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

    // by PrzemekBarczyk
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneIndex());
    }

    public void NextScene()
    {
        int currentIndex = SceneIndex();
        int sceneCount = SceneManager.sceneCountInBuildSettings;

        int nextIndex = (currentIndex + 1) % sceneCount;
        SceneManager.LoadScene(nextIndex);
    }
}