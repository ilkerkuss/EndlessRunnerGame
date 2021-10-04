using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public enum GameStates
    {
        IsGameOver,
        IsGameLoaded,
        IsGamePlaying,
        IsGamePaused

    }

    public GameStates GameState;

    [SerializeField] private GameObject _player;

    private void Awake()
    {
        if (Instance==null)
        {
            Instance=this;
        }

        
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (GameState == GameStates.IsGamePlaying)
        {
           InGameCanvasController.Instance.ShowScoreOfPlayer(PlayerManager.Instance.GetScoreOfPlayer());
        }

    }

    private void OnEnable()
    {
        PlayerController.PlayerDead += GameOver;
    }
    private void OnDisable()
    {
        PlayerController.PlayerDead -= GameOver;

    }



    public void Init()
    {
        GameState = GameStates.IsGameLoaded;
    }



    public void Replay()
    {
        _player.GetComponent<PlayerController>().CharacterDead();

        TileManager.Instance.ResetTiles();

        CanvasController.Instance.GameOverPanel.HidePanel();

        CanvasController.Instance.PausePanel.ShowPanel();

        AudioController.Instance._sounds[0].AudioSource.Play(); //background sound çalma.

        PlayerManager.Instance.ResetScores();

        PlayerManager.Instance.ActivateHighScoreUI();

        Init(); 

    }


    public void GameOver()
    {

        GameState = GameStates.IsGameOver;

        AudioController.Instance._sounds[0].AudioSource.Stop();  //Background sesini durdurur. replay ile tekrar aktif edilir.

        CanvasController.Instance.GameOverPanel.ShowPanel();

        EndGameCanvasController.Instance.SetEndGameScores();

        PlayerManager.Instance.DisableHighScoreUI();

        CanvasController.Instance.PausePanel.HidePanel();


    }

    public void StartGame()
    {
        GameState = GameStates.IsGamePlaying;

        CanvasController.Instance.InGamePanel.ShowPanel();
    }


    public void PlayGame() // After press play game button
    {
        Replay();
        StartGame();
        
    }


}
