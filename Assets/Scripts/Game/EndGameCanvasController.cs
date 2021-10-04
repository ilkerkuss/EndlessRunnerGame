using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameCanvasController : CanvasManager
{
    public static EndGameCanvasController Instance;

    [SerializeField] private Text _endGameCoinsText;
    [SerializeField] private Text _endGameScoreText;

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

    public void SetEndGameScores()
    {
        _endGameCoinsText.text = "Taken Coins " + PlayerManager.Instance.GetNumberOfCoin().ToString();
        _endGameScoreText.text = "End Game Score " + PlayerManager.Instance.GetScoreOfPlayer().ToString();
    }
}
