using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControl : MonoBehaviour
{
    public GameObject pauseMenuScreen;
    public GameObject gameOverScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void HandlePlayButtonOnClickEvent()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }
    public void HandleQuitButtonOnClickEvent()
    {
        Application.Quit();
    }
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void HandlePauseButtonOnClickEvent()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }
    public void HandleResumeButtonOnClickEvent()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("MenuScenes");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
