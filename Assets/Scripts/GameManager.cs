using System.Collections.Generic;
using UnityEngine;

public enum GameEvent
{
    Start,
    Pause,
    Restart,
    EndGame,
    Running
}

public interface IGameEventHandler
{
    void HandleGameEvent(GameEvent gameEvent);
}

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<GameManager>();
            return instance;
        }
    }
    private GameEvent currentGameEvent;
    public GameEvent CurrentGameEvent => currentGameEvent;


    private bool isGamePaused = false;

    private Vector3 initialPlayerPosition;
    [SerializeField] private int playerStartBullets = 15;

    private List<IGameEventHandler> gameEventHandlers = new List<IGameEventHandler>();

    private void Start()
    {
        GameStateChange(GameEvent.Start);
        initialPlayerPosition = Player.Instance.GetComponent<Transform>().position;
        InitializeGame();
        isGamePaused = false;
    }

    private void LateUpdate()
    {
        if (Player.Instance.AmmoCount <= 0 && currentGameEvent != GameEvent.EndGame)
        {
            GameStateChange(GameEvent.EndGame);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && currentGameEvent == GameEvent.Running)
        {
            GameStateChange(GameEvent.Pause);
        }
    }

    private void InitializeGame()
    {
        Player.Instance.Init();

        Player.Instance.GetComponent<Transform>().position = initialPlayerPosition;
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            Destroy(enemy);
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Item"))
            Destroy(item);
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet"))
            Destroy(bullet);
    }

    private void StartGame()
    {
        //...
    }

    private void PauseGame()
    {
        isGamePaused = true;
    }
    private void RestartGame()
    {
        InitializeGame();
        GameStateChange(GameEvent.Running);
    }

    private void EndGame()
    {
        SoundsManager.Instance.PlayGameOverSound();
    }
    public void GameStateChange(GameEvent gameEvent)
    {
        NotifyEventHandlers(gameEvent);
        currentGameEvent = gameEvent;

        switch (gameEvent)
        {
            case GameEvent.Start:
                StartGame();
                break;
            case GameEvent.Pause:
                PauseGame();
                break;
            case GameEvent.Restart:
                RestartGame();
                break;
            case GameEvent.EndGame:
                EndGame();
                break;
            case GameEvent.Running:
                isGamePaused = false;
                break;
        }
    }

    public void AddListener(IGameEventHandler gameEventHandler)
    {
        gameEventHandlers.Add(gameEventHandler);
    }
    public void RemoveListener(IGameEventHandler gameEventHandler)
    {
        gameEventHandlers.Remove(gameEventHandler);
    }
    private void NotifyEventHandlers(GameEvent gameEvent)
    {
        foreach (IGameEventHandler gameEventHandler in gameEventHandlers)
            gameEventHandler.HandleGameEvent(gameEvent);
    }
}
