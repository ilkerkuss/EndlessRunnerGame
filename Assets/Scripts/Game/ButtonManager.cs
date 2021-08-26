using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    

    public void ReplayGame() //onclick
    {
        LevelManager.Instance.Replay();
        LevelManager.Instance.StartGame();

        
        AudioController.Instance.PlaySound("ButtonSound");


    }

    public void QuitGame()
    {
        Application.Quit();

        //PlayerManager.Instance.ResetGold();

        AudioController.Instance.PlaySound("ButtonSound");
    }


    public void OnClickPlayGame()
    {
        LevelManager.Instance.PlayGame();
     

        AudioController.Instance.PlaySound("ButtonSound");
    }

    public void OnClickBackToMenu()
    {
        LevelManager.Instance.BackToMenu();

        AudioController.Instance.PlaySound("ButtonSound");

    }

    public void OnClickPauseResumeGame()
    {
        if (!LevelManager.IsGamePaused)
        {
            LevelManager.Instance.PauseGame();
        }
        else{
            LevelManager.Instance.ResumeGame();
        }
        
    }
}
