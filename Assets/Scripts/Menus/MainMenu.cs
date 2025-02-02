using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ControlsBackground;
    // Start is called before the first frame update
    void Start()
    {
        ControlsBackground.SetActive(false);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenControls()
    {
        ControlsBackground.SetActive(true);
    }

    public void BackToMainmenu()
    {
        ControlsBackground.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
