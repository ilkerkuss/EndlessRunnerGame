using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public static bool IsGameOver;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _player;

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
      //  Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (IsGameOver)
        {
            ShowGameOver();

        }
*/
  
    }

    public void Replay()
    {

       // Debug.Log("pos" + _player.GetComponent<PlayerController>().GetPlayerPosition());

        //_player.transform.position = _player.GetComponent<PlayerController>().GetPlayerPosition();

        _player.GetComponent<PlayerController>().CharacterDead();
        TileManager.Instance.ResetTiles();
        Hide();
       // Debug.Log("pos"+ _player.GetComponent<PlayerController>().GetPlayerPosition());
    }

    public void Hide()
    {
        
        _gameOverPanel.SetActive(false);
        //_player.transform.position = _player.GetComponent<PlayerController>().GetPlayerPosition();
        //Time.timeScale = 1;
    }

    public void ShowGameOver()
    {
        //Time.timeScale = 0;
        IsGameOver = true;
        _gameOverPanel.SetActive(true);

    }
}
