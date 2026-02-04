using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState state = GameState.Waiting;

    [SerializeField] List<Head> players;
    [SerializeField] List<LineRenderer> tails;
    [SerializeField] Vector2 minBounds;
    [SerializeField] Vector2 maxBounds;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetUpPlayers();
    }

    void Update()
    {
        if(state == GameState.Waiting && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        else if(state == GameState.GameOver && Input.GetKeyDown(KeyCode.Space))
        {
            RestartGame();
        }
    }

    void SetUpPlayers()
    {
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
            p.enabledMove = false;
        }

        state = GameState.Waiting;
    }

    void StartGame()
    {
        state = GameState.Playing;

        foreach(Head p in players)
            p.enabledMove = true;
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

    void EndRound()
    {
        state = GameState.GameOver;

        foreach (Head p in players)
        {
            p.enabledMove = false;
            if (p.isAlive)
                Debug.Log($"{p.Name} wygrywa!");
        }
    }

    void RestartGame()
    {
        //ClearBoard();
        SetUpPlayers();
    }
}
