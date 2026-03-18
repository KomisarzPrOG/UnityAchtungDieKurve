using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void OnSettingsButton()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftWindows))
            Quit();
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
