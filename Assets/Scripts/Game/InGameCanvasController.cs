using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameCanvasController : CanvasManager
{
    public static InGameCanvasController Instance;

    public Button PlayButton;

    [SerializeField] private Text _coinsText;
    [SerializeField] private Text _scoreText;



    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void ShowTakenCoinNumber(int CoinNumber)
    {
        _coinsText.text = "Coins :"+ CoinNumber;
    }

    public void ShowScoreOfPlayer(int PlayerScore)
    {
        _scoreText.text = "Score :"+ PlayerScore;
    }
}
