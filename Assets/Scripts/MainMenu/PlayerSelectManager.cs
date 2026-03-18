using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerSelectManager : MonoBehaviour
{
    public static PlayerSelectManager Instance;

    public List<PlayerConfig> selectedPlayers = new List<PlayerConfig>();
    [SerializeField] int currentPlayer = 0;
    [SerializeField] bool waitingForLeftKey = false;
    [SerializeField] bool waitingForRightKey = false;

    HashSet<KeyCode> forbiddenKeys = new()
    {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,

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
        ResetState();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        selectedPlayers.Clear();
        currentPlayer = 0;
        waitingForLeftKey = false;
        waitingForRightKey = false;
    }

    public void SelectPlayer(int id, string name, Material material, TMP_Text left, TMP_Text right)
    {
        selectedPlayers.Add(new PlayerConfig(id, name, material, left, right));
        if(selectedPlayers.Count > 0)
            waitingForLeftKey = true;
    }

    public void DeselectPlayer(int id)
    {
        int index = selectedPlayers.FindIndex(p => p.playerID == id);
        if (index == -1) return;

        selectedPlayers.RemoveAt(index);

        waitingForLeftKey = false;
        waitingForRightKey = false;

        currentPlayer = selectedPlayers.FindIndex(p => p.leftKey == KeyCode.None);

        if (currentPlayer == -1)
            currentPlayer = selectedPlayers.Count;
        else
            waitingForLeftKey = true;
    }

    private void Update()
    {
        if (waitingForLeftKey || waitingForRightKey)
            DetectKey();

        if (Input.GetKeyDown(KeyCode.Space) && selectedPlayers.Count >= 2 && !IsWaitingForInput())
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
        SceneHandler.GoToGame();
    }

    void ChangeText(string control, PlayerConfig player)
    {
        if (control == "Left")
            player.leftKeyText.text = KeyNameUtility.ToPretty(player.leftKey);
        else
            player.rightKeyText.text = KeyNameUtility.ToPretty(player.rightKey);
    }

    public bool IsWaitingForInput() => waitingForLeftKey || waitingForRightKey;
}
