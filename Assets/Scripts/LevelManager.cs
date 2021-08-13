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

}
