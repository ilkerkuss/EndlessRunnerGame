using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public static bool IsGameOver;
    public static bool IsGameStarted;

    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _player;
    [SerializeField] private Text _startingText;
    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _scoreText;


    // Start is called before the first frame update

    private void Awake()
    {
        if (Instance==null)
        {
            Instance=this;
        }
    }

    void Start()
    {
        

        IsGameOver = false;
        IsGameStarted = false;

      //  Time.timeScale = 1;
    }


    void Update()
    {
        StartGame();
        ShowTakenCoinNumber();
        ShowScoreOfPlayer();
  
    }

    public void Replay()
    {
        
        _player.GetComponent<PlayerController>().CharacterDead();
        TileManager.Instance.ResetTiles();

        Hide();

    }

    public void Hide()
    {
        _gameOverPanel.SetActive(false);
        //Time.timeScale = 1;
    }


    public void ShowGameOver()
    {
        //Time.timeScale = 0;
        IsGameOver = true;
        _gameOverPanel.SetActive(true);

    }

    public void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(CountForStart());

        }

    }

    public IEnumerator CountForStart()
    {
        int counter = 3;
        while (counter >0)
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
        Debug.Log(PlayerController.Instance.GetNumberOfCoin());
        _coinsText.text = "Coins :"+PlayerController.Instance.GetNumberOfCoin().ToString();
    }

    public void ShowScoreOfPlayer()
    {
        Debug.Log(PlayerController.Instance.GetScoreOfPlayer());
        _scoreText.text = "Score :"+PlayerController.Instance.GetScoreOfPlayer().ToString();
    }

}
