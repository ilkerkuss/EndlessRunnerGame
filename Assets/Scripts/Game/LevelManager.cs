using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public static bool IsGameOver;
    public static bool IsGameStarted;
    public static bool IsGamePaused;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _PausePanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private Text _startingText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _endGameCoinsText;
    [SerializeField] private Text _endGameScoreText;
  
    private GameObject _mainMenu;

    private int _startCounter;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance=this;
        }

        
    }



    void Start()
    {
        _mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
        Init();

    }



    void Update()
    {
        if (!IsGameOver && !IsGamePaused)
        {
            StartGame();
            ShowTakenCoinNumber();
            ShowScoreOfPlayer();

        }
        Debug.Log("over,pause,start" + LevelManager.IsGameOver + LevelManager.IsGamePaused + LevelManager.IsGameStarted);

    }




    public void Init()
    {

        IsGameOver = false;
        IsGameStarted = false;
        IsGamePaused = false;

        _startingText.gameObject.SetActive(true);
        _coinsText.gameObject.SetActive(true);
        _scoreText.gameObject.SetActive(true);

        _startCounter = 3;
        //  Time.timeScale = 1;
    }





    public void Replay()
    {
        
        _player.GetComponent<PlayerController>().CharacterDead();
        TileManager.Instance.ResetTiles();

        //Hide();
        _gameOverPanel.SetActive(false);

        AudioController.Instance._sounds[0].AudioSource.Play(); //background sound çalma.

        PlayerManager.Instance.ResetScores();
        PlayerManager.Instance.ActivateHighScoreUI();

        ActivateInGameUI();

        Init(); 

    }

    /*
    public void Hide()
    {
        _gameOverPanel.SetActive(false);
        //Time.timeScale = 1;
    }
    */

    public void ShowGameOver()
    {
        //Time.timeScale = 0;
        //IsGameOver = true;
        //IsGameStarted = false;

        AudioController.Instance._sounds[0].AudioSource.Stop();  //Background sesini durdurur. replay ile tekrar aktif edilir.

        _gameOverPanel.SetActive(true);


        SetEndGameScores();

        PlayerManager.Instance.DisableHighScoreUI();

        DisableInGameUI();

    }

    public void StartGame()
    {
        if (Input.GetMouseButtonDown(0) && !IsGameStarted)
        {
            StartCoroutine(CountForStart());

        }

    }

    public IEnumerator CountForStart()
    {
        Debug.Log("geri sayým baþladý");
        int counter = _startCounter;
        
        while (counter > 0)
        {
            _startingText.text = counter.ToString();
            yield return new WaitForSeconds(1f);
            counter--;

        }

        _startingText.gameObject.SetActive(false);
        StopCoroutine(CountForStart());

        IsGameStarted = true;

    }



    public void ShowTakenCoinNumber()
    {
        //Debug.Log(PlayerController.Instance.GetNumberOfCoin());
        _coinsText.text = "Coins :"+ PlayerManager.Instance.GetNumberOfCoin().ToString();
    }

    public void ShowScoreOfPlayer()
    {
       // Debug.Log(PlayerController.Instance.GetScoreOfPlayer());
        _scoreText.text = "Score :"+ PlayerManager.Instance.GetScoreOfPlayer().ToString();
    }



    public void PlayGame()
    {
        _mainMenu.SetActive(false);
        Replay();
        StartGame();
        
    }

    public void BackToMenu()
    {

        _mainMenu.SetActive(true);
        _gameOverPanel.SetActive(false);

        
    }

    public void PauseGame()
    {
        if (!IsGameOver && !IsGamePaused)
        {
            _PausePanel.SetActive(true);

            Time.timeScale = 0;
            IsGamePaused = true;

            

        }
    }

    public void ResumeGame()
    {
        if (!IsGameOver && IsGamePaused)
        {
            _PausePanel.SetActive(false);

            Time.timeScale = 1;
            IsGamePaused = false;

            
        }
    }


    private void SetEndGameScores()
    {
        _endGameCoinsText.text = "Taken Coins " + PlayerManager.Instance.GetNumberOfCoin().ToString();
        _endGameScoreText.text = "End Game Score " + PlayerManager.Instance.GetScoreOfPlayer().ToString();
    }



    private void DisableInGameUI()
    {
        _coinsText.gameObject.SetActive(false);
        _scoreText.gameObject.SetActive(false);
    }

    private void ActivateInGameUI()
    {
        _startingText.text = "TAP TO START";
        _startingText.gameObject.SetActive(true);

        _coinsText.gameObject.SetActive(true);
        _scoreText.gameObject.SetActive(true);
    }






}
