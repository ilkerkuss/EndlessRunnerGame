using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PausePanelController : CanvasManager
{
    public static PausePanelController Instance;

    public Button PauseButton;
    public GameObject PausePanel;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public new void ShowPanel()
    {
        base.ShowPanel();

    }

    public new void HidePanel()
    {
        base.HidePanel();
    }

    public void ShowPausePanel()
    {
        PausePanel.SetActive(true);
    }

    public void HidePausePanel()
    {
        PausePanel.SetActive(false);
    }
}
