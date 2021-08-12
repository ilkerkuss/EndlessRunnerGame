using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{


    public void  ReplayGame() //onclick
    {
        LevelManager.Instance.Replay();


    }

    public void QuitGame()
    {
        Application.Quit();
    }


}
