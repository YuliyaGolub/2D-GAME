using UnityEngine;

public class UIManager : ModalWindowController, IGameEventHandler
{
    private void Awake()
    {
        base.Awake();
        GameManager.Instance.AddListener(this);
    }

    public void HandleGameEvent(GameEvent gameEvent)
    {
        switch (gameEvent)
        {
            case GameEvent.Start:
                ShowStartPanel();
                break;
            case GameEvent.Pause:
                ShowPausePanel();
                break;
            case GameEvent.Restart:
                //ShowRestartPanel();
                break;
            case GameEvent.EndGame:
                ShowRestartPanel();
                break;
            case GameEvent.Running:
                ShowPlayerUI();
                break;
        }
    }

    private void ShowStartPanel()
    {
        ShowModalWindow("GameStart");
        Time.timeScale = 0;
    }

    private void ShowPausePanel()
    {
        ShowModalWindow("GamePause");
        Time.timeScale = 0;
    }

    private void ShowRestartPanel()
    {
        ShowModalWindow("GameRestart");
        Time.timeScale = 0;
    }
    private void ShowPlayerUI()
    {
        ShowModalWindow("PlayerUI");
        Time.timeScale = 1;
    }

    public void StartGameButton()
    {
        GameManager.Instance.GameStateChange(GameEvent.Running);
    }

    public void RestartGameButton()
    {
        GameManager.Instance.GameStateChange(GameEvent.Restart);
    }

    public void ContinueGameButton()
    {
        GameManager.Instance.GameStateChange(GameEvent.Running);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
