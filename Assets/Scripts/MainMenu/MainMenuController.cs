using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private float escapeHoldTime = 0f;
    private const float escapeHoldThreshold = 1f;

    public void OnSettingsButton()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            escapeHoldTime += Time.deltaTime;
            if (escapeHoldTime >= escapeHoldThreshold)
                Quit();
        }
        else
        {
            escapeHoldTime = 0f;
        }
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
