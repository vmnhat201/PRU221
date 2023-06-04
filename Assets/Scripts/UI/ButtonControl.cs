using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonControl : Singleton<ButtonControl>
{
    public bool isGameStart;
    public bool isGamePause;
    public bool isShowIntro;
    public bool isBuffCircle;

    public GameObject startMenu;
    public GameObject pauseMenuScreen;
    public GameObject gameOverScreen;
    public GameObject pauseButton;
    public GameObject countBar;
    public GameObject question;
    public GameObject toggle;

    // Start is called before the first frame update

    private void Awake()
    {
        isBuffCircle = false;
        isGameStart = false;
        isGamePause = true;
        isShowIntro = false;
        countBar.SetActive(false);
        startMenu.SetActive(true);
        pauseMenuScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseButton.SetActive(false);
        question.SetActive(true);
    }


    public void Reset()
    {
        SceneManager.LoadScene("SampleSence");
    }
    private void Update()
    {
        if (isGamePause || isShowIntro)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
            if (isBuffCircle)
            {
                GameManager.instance.player.FindClosestEnemy();
            }
            else
            {
                GameManager.instance.player.Shoot();
            }
        }
        GameSave.instance.isIntro = toggle.GetComponent<Toggle>().isOn;

    }

    public void StartGame()
    {
        StartCoroutine(ReadyToStartGame());
    }
    public IEnumerator ReadyToStartGame()
    {
        startMenu.SetActive(false);
        countBar.SetActive(true);
        SoundController.instance.PlayGameStart();
        isGameStart = true;
        //yield return new WaitForSecondsRealtime(SoundController.instance.GameStart.length);
        yield return new WaitForSecondsRealtime(1);

        countBar.SetActive(false);
        pauseButton.SetActive(true);
        isGamePause = false;
        SpawnManager.instance.StartSpawn();

    }
    public void Resume()
    {
        PlayerData playerData = FileManager.ReadPlayerData();
        List<Enemies> enemies = FileManager.ReadEnemiesData();
        if (enemies != null)
        {
            foreach (var item in enemies)
            {
                GameManager.instance.CurEnemies.Add(item);
            }
            Debug.Log("Số lượng Enemies trong hiện thực :" + GameManager.instance.CurEnemies.Count);
          
        }
        playerData.Player(GameManager.instance.player);
        startMenu.SetActive(false);
        isGameStart = true;
        pauseButton.SetActive(true);
        isGamePause = false;

    }

    public void RePlay()
    {
        StartCoroutine(ReadyToReplayGame());
    }
    public IEnumerator ReadyToReplayGame()
    {
        startMenu.SetActive(false);
        countBar.SetActive(true);
        SoundController.instance.PlayGameStart();
        isGameStart = true;
        yield return new WaitForSecondsRealtime(SoundController.instance.GameStart.length);
        countBar.SetActive(false);
        pauseButton.SetActive(true);
        isGamePause = false;
    }
    public void PauseGame()
    {
        isGamePause = true;
        pauseMenuScreen.SetActive(true);
    }
    public void ResumeGame()
    {
        isGamePause = false;
        pauseMenuScreen.SetActive(false);
    }

    public void GameOver()
    {
        isGamePause = true;
        gameOverScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Save Player Date when quit application");
        Application.Quit();
        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {

                Debug.Log("Enemies :" + GameManager.instance.Enemies.Count);
                FileManager.SaveEnemiesData();
                FileManager.SavePlayerData();

        }
    }
}
