using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSelectManager : MonoBehaviour
{
    public static PlayerSelectManager Instance;

    public List<PlayerConfig> selectedPlayers = new List<PlayerConfig>();
    int currentPlayer = 0;
    bool waitingForLeftKey = false;
    bool waitingForRightKey = false;

    HashSet<KeyCode> forbiddenKeys = new()
    {
        KeyCode.Escape,
        KeyCode.Space,
        KeyCode.F11,
        KeyCode.Print,
        KeyCode.LeftAlt,
        KeyCode.RightAlt,
        KeyCode.LeftControl,
        KeyCode.RightControl,
        KeyCode.CapsLock,
        KeyCode.Numlock,
        KeyCode.ScrollLock,

        KeyCode.Mouse0,
        KeyCode.Mouse1,
        KeyCode.Mouse2,
        KeyCode.Mouse3,
        KeyCode.Mouse4,
        KeyCode.Mouse5,
        KeyCode.Mouse6
    };

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SelectPlayer(int id, string name, Material material, TMP_Text left, TMP_Text right)
    {
        selectedPlayers.Add(new PlayerConfig(id, name, material, left, right));
        if(selectedPlayers.Count > 0)
            waitingForLeftKey = true;
    }

    private void Update()
    {
        if (waitingForLeftKey || waitingForRightKey)
            DetectKey();

        if (Input.GetKeyDown(KeyCode.Space) && selectedPlayers.Count >= 2)
            StartGame();
    }

    void DetectKey()
    {
        foreach(KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {
            if(!Input.GetKeyDown(key)) continue;
            if(forbiddenKeys.Contains(key)) continue;

            var p = selectedPlayers[currentPlayer];

            if(p == null)
            {
                Debug.LogWarning("Nie ma takiego gracza");
                break;
            }

            if(waitingForLeftKey)
            {
                p.leftKey = key;
                waitingForLeftKey = false;
                waitingForRightKey = true;
                ChangeText("Left", p);
                return;
            }
            
            if(waitingForRightKey && p.leftKey != key)
            {
                p.rightKey = key;
                waitingForRightKey = false;
                ChangeText("Right", p);

                currentPlayer++;
                if(currentPlayer < selectedPlayers.Count)
                    waitingForLeftKey = true;
            }
        }
    }

    void StartGame()
    {
        PlayerPrefsData.Save(selectedPlayers);
        SceneHandler.Instance.NextScene();
    }

    void ChangeText(string control, PlayerConfig player)
    {
        if (control == "Left")
            player.leftKeyText.text = KeyNameUtility.ToPretty(player.leftKey);
        else
            player.rightKeyText.text = KeyNameUtility.ToPretty(player.rightKey);
    }
}
