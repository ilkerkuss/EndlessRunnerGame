using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    //player altýn sayýsý ve skoru
    private int _scoreOfPlayer;
    private int _coinsOfPlayer;

    private float _increaseAmount; //score için artýþ deðeri
    private int _coinValue;  //coinlerin deðeri


    [SerializeField] private Text _highScoreText;
    [SerializeField] private Text _totalCoinsText;


    private void Awake()
    {
        if (Instance==null)
        {
            Instance = this;
        }

        PlayerPrefs.GetInt("HighScore",0);
        PlayerPrefs.GetInt("TotalCoins",0);
    }


    void Start()
    {
        Init();

    }


    void Update()
    {


        if (_scoreOfPlayer > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore",_scoreOfPlayer);
            _highScoreText.text = PlayerPrefs.GetInt("HighScore",_scoreOfPlayer).ToString();
        }


    }


    private void LateUpdate()
    {
        if (LevelManager.Instance.GameState == LevelManager.GameStates.IsGamePlaying)
        {
            IncreaseScore();
        }

    }

    public void Init()
    {
        _scoreOfPlayer = 0;
        _coinsOfPlayer = 0;

        _increaseAmount = 0;
        _coinValue = 50;


        _highScoreText.gameObject.SetActive(true);
        _highScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();


        _totalCoinsText.gameObject.SetActive(true);
        _totalCoinsText.text = PlayerPrefs.GetInt("TotalCoins", 0).ToString();
    }

    //get player score and coin stats
    public int GetNumberOfCoin()
    {
        return _coinsOfPlayer;

    }

    public int GetScoreOfPlayer()
    {
        return _scoreOfPlayer;

    }

    public void IncreaseNumberOfCoin()
    {
        _coinsOfPlayer += 1;

        InGameCanvasController.Instance.ShowTakenCoinNumber(_coinsOfPlayer);

    }



    public void IncreaseScore() //ilerleme(distance) deðerine ve alýnan coine göre puan arttýrma
    {
        _increaseAmount = GameObject.FindGameObjectWithTag("Player").transform.position.z;

        _scoreOfPlayer = (int)Mathf.Round(_increaseAmount);
        _scoreOfPlayer += (_coinsOfPlayer * _coinValue);


    }

    public void ResetScores()
    {
        _scoreOfPlayer = 0;
        _increaseAmount = 0;
        _coinsOfPlayer = 0;
        _coinValue = 50;

        InGameCanvasController.Instance.ShowTakenCoinNumber(_coinsOfPlayer);

    }

    public void ActivateHighScoreUI()
    {
        _highScoreText.gameObject.SetActive(true);
        _totalCoinsText.gameObject.SetActive(true);
    }
    public void DisableHighScoreUI()
    {
        _highScoreText.gameObject.SetActive(false);
        _totalCoinsText.gameObject.SetActive(false);
    }
    




    //Star item func.
    public int GetCoinValue()
    {
        return _coinValue;
    }
    public void SetCoinValue(int Value)
    {
        _coinValue = Value;
    }
}
