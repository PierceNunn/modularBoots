using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseScreen;
    [SerializeField] private GameObject ControlsScreen;
    // Start is called before the first frame update
    void Start()
    {
        PauseScreen.SetActive(false);
        ControlsScreen.SetActive(false);
    }

    public void OnPause()
    {
        if(Time.timeScale > 0)
        {
            PauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            onResume();
        }
    }
    public void onResume()
    {
        PauseScreen.SetActive(false);
        ControlsScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void onExit()
    {
        Application.Quit();
    }

    public void onControls()
    {
        ControlsScreen.SetActive(true);
    }
    public void onBack()
    {
        ControlsScreen.SetActive(false);
    }

    public void OnMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
