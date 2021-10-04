using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        AudioController.Instance.PlaySound("ButtonSound");
    }


    public void OnClickPlayGame()
    {
        LevelManager.Instance.PlayGame();

        InGameCanvasController.Instance.PlayButton.gameObject.SetActive(false);

        CanvasController.Instance.PausePanel.ShowPanel();

        AudioController.Instance.PlaySound("ButtonSound");
    }

    public void OnClickPauseResumeGame()
    {
        if (LevelManager.Instance.GameState == LevelManager.GameStates.IsGamePlaying)
        {

            PausePanelController.Instance.ShowPausePanel();

            LevelManager.Instance.GameState = LevelManager.GameStates.IsGamePaused;
        }
        else{

            PausePanelController.Instance.HidePausePanel();

            LevelManager.Instance.GameState = LevelManager.GameStates.IsGamePlaying;

        }
        
    }
}
