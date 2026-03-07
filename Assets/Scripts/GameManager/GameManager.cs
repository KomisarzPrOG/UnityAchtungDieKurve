using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.Waiting;

    [SerializeField] int expectedPlayers;
    [SerializeField] Vector2 minBounds;
    [SerializeField] Vector2 maxBounds;

    [SerializeField] GameObject winWindowPrefab;

    List<Head> players = new List<Head>();
    bool paused = false;

    float escapeHoldTime = 0;
    [SerializeField] float escapeRequiredTime = 3f;
    TMP_Text escapeText;

    private void Awake()
    {
        if(Instance != null)
        {
            Instance.players.Clear();
            Destroy(gameObject);
            return;
        }

        Instance = this;
        expectedPlayers = PlayerPrefsData.players.Count;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if (SceneHandler.Instance.SceneIndex() == 0) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(state)
            {
                case GameState.Waiting:
                    StartGame(); break;
                case GameState.Playing:
                    PauseGame(); break;
                case GameState.RoundOver:
                    RestartGame(); break;
                case GameState.GameOver:
                    ScoreHandler.Instance.ClearScores();
                    SceneHandler.Instance.GoToMenu(); break;
            }
        }

        if(escapeText == null)
            escapeText = GameObject.Find("EscapeText").GetComponent<TMP_Text>();

        HandleEscapeHold();
    }

    void HandleEscapeHold()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            escapeHoldTime += Time.unscaledDeltaTime;

            if(escapeHoldTime >= escapeRequiredTime)
            {
                ScoreHandler.Instance.ClearScores();
                SceneHandler.Instance.GoToMenu();
            }

            float progress = escapeHoldTime / escapeRequiredTime;

            if (progress >= 0.75f)
                escapeText.text = "Returning to Menu...";
            else if (progress >= 0.5f)
                escapeText.text = "Returning to Menu..";
            else if (progress >= 0.25f)
                escapeText.text = "Returning to Menu.";
            else
                escapeText.text = "Returning to Menu";
        }
        else
        {
            escapeHoldTime = 0;
            escapeText.text = "";
        }
    }

    /* =============================== 
                PLAYER MANAGER
       ===============================*/
    void SetUpPlayers()
    {
        state = GameState.Waiting;

        foreach(Head p in players)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(minBounds.x, maxBounds.x),    
                Random.Range(minBounds.y, maxBounds.y)
            );

            float randomRot = Random.Range(0, 360f);

            p.transform.position = randomPos;
            p.transform.rotation = Quaternion.Euler(0,0,randomRot);

            p.isAlive = true;

            p.tail.SetStartingTail();
        }
    }

    public int CheckPlayers()
    {
        int alivePlayers = 0;

        foreach(Head p in players)
            if (p.isAlive)
                alivePlayers++;

        return alivePlayers;
    }

    public void RegisterPlayer(Head p)
    {
        if (p == null) return;
        if (players.Contains(p)) return;

        Debug.Log($"Player {p.Name} registered! ({players.Count+1}/{expectedPlayers})");
        players.Add(p);

        if (players.Count == expectedPlayers)
        {
            SetUpPlayers();
            ScoreHandler.Instance.SetUpPlayerScores();
            ScoreHandler.Instance.UpdateUI();
            Debug.Log("Detected every player!");
        }
    }


    /* =============================== 
              GAME STATE MANAGER
       ===============================*/
    void StartGame()
    {
        Time.timeScale = 1f;
        state = GameState.Playing;
    }


    public void EndRound()
    {
        Time.timeScale = 0;
        state = GameState.RoundOver;

        foreach(Head p in players)
        {
            if (ScoreHandler.Instance.DidPlayerWin(p.id))
            {
                PlayerWon(p);
                break;
            }
        }
    }

    void RestartGame()
    {
        SceneHandler.Instance.RestartScene();
        state = GameState.Waiting;
    }

    void PauseGame()
    {
        if(paused)
        {
            Time.timeScale = 1f;
        }
        else
        {
            Time.timeScale = 0;
        }
        paused = !paused;
    }

    void PlayerWon(Head player)
    {
        state = GameState.GameOver;
        Debug.Log($"{player.Name} won!");

        GameObject winWindowObj = Instantiate(winWindowPrefab, GameObject.Find("WinScreen").transform);
        WinWindowHandler winWindowAccess = winWindowObj.GetComponent<WinWindowHandler>();

        winWindowAccess.SetWindow(player.playerColor, player.Name);
    }

    /*==========================
            HELPER METHODS
      ===========================*/

    public List<Head> GetAllPlayers()
    {
        return players;
    }

    public Head GetPlayer(int playerId)
    {
        foreach(Head p in players)
        {
            if(p.id == playerId)
                return p;
        }

        return null;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Vector3 center = new Vector3((minBounds.x + maxBounds.x) / 2, (minBounds.y + maxBounds.y) / 2, 0);
        Vector3 size = new Vector3(maxBounds.x - minBounds.x, maxBounds.y - minBounds.y, 0.1f);
        Gizmos.DrawWireCube(center, size);
    }
}
