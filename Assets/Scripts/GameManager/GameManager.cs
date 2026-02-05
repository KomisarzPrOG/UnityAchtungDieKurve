using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.Waiting;

    [SerializeField] int expectedPlayers;
    [SerializeField] Vector2 minBounds;
    [SerializeField] Vector2 maxBounds;

    List<Head> players = new List<Head>();
    bool paused = false;

    private void Awake()
    {
        if(Instance != null)
        {
            Instance.players.Clear();
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Time.timeScale = 0;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch(state)
            {
                case GameState.Waiting:
                    StartGame(); break;
                case GameState.Playing:
                    PauseGame(); break;
                case GameState.GameOver:
                    RestartGame(); break;
            }
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

            p.tail.ResetTail();
            p.tail.SetStartingTail();
        }
    }

    public void CheckPlayers()
    {
        int alivePlayers = 0;

        foreach(Head p in players)
            if (p.isAlive)
                alivePlayers++;

        if (alivePlayers <= 1)
            EndRound();
    }

    public void RegisterPlayer(Head p)
    {
        if (p == null) return;
        if (players.Contains(p)) return;

        Debug.Log($"Gracz {p.Name} zarejestrowany! ({players.Count+1}/{expectedPlayers})");
        players.Add(p);

        if (players.Count == expectedPlayers)
        {
            SetUpPlayers();
            ScoreHandler.Instance.SetUpPlayerScores();
            Debug.Log("Wykryto wszystkich graczy!");
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


    void EndRound()
    {
        Time.timeScale = 0;
        state = GameState.GameOver;

        foreach (Head p in players)
        {
            if (p.isAlive)
                Debug.Log($"{p.Name} wygrywa!");
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
}
