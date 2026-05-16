
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public SceneReference playScene;

    public void MainMenuPlayButton()
    {
        SceneManager.LoadScene(playScene.name);
    }
    public void MainMenuQuitButton()
    {
        Debug.Log("Wylaczanie!");
        Application.Quit();
    }
}
