﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        yield return new WaitForSecondsRealtime(SoundController.instance.GameStart.length);
        countBar.SetActive(false);
        pauseButton.SetActive(true);
        isGamePause = false;
        SpawnManager.instance.StartSpawn();

    }
    public void Resume()
    {

        StartCoroutine(ReadyToResumeGame());


    }

    public IEnumerator ReadyToResumeGame()
    {
        List<GameObject> cloneObjects = GameObject.FindObjectsOfType<GameObject>().ToList();
        List<GameObject> destroyedMap = new List<GameObject>();

        foreach (GameObject cloneObject in cloneObjects)
        {
            if (cloneObject.name.Contains("(Clone)") && cloneObject.name.Contains("Map1"))
            {
                destroyedMap.Add(cloneObject);
                Destroy(cloneObject);
            }
        }
        GameManager.instance.player.SetPlayerWeaponWhenResume();
        List<GameObject> allObjectsAfterLoad = GameObject.FindObjectsOfType<GameObject>().ToList();
        List<GameObject> newMapObjects = new List<GameObject>();
        foreach (GameObject item in allObjectsAfterLoad)
        {
            if (item.name.Contains("(Clone)") && item.name.Contains("Map1"))
            {
                bool exist = false;
                foreach (GameObject oldMap in destroyedMap)
                {
                    if (item == oldMap)
                    {
                        exist = true;
                        Debug.Log($"type : {item.GetType()}, {item.GetInstanceID()}, {item.activeSelf} ,{item.activeInHierarchy}");
                    }

                }
                if (!exist)
                {
                    newMapObjects.Add(item);
                }
            }


        }
        MapController mapController = GameObject.FindObjectOfType<MapController>();

        Debug.Log("Số lượng Map sau khi load :" + newMapObjects.Count);
        for (int i = 0; i < newMapObjects.Count; i++)
        {
            Debug.Log("Số lượng Map trước khi thay đổi dữ liệu :" + mapController.activeMaps.Count);

            mapController.activeMaps[i] = newMapObjects[i];

        }
        Debug.Log("Số lượng Map sau khi thay đổi dữ liệu :" + mapController.activeMaps.Count);

        foreach (var item in mapController.activeMaps)
        {
            Debug.Log($"Map : {item.name}");
        }
        startMenu.SetActive(false);
        isGameStart = true;
        pauseButton.SetActive(true);
        isGamePause = false;
        yield return new WaitForSecondsRealtime(5);
        SpawnManager.instance.StartSpawn();
    }
    private void saveCodeResume()
    {
        //PlayerData playerData = FileManager.ReadPlayerData();
        //List<Enemies> enemies = FileManager.ReadEnemiesData();
        //if (enemies != null)
        //{
        //    foreach (var item in enemies)
        //    {
        //        GameManager.instance.CurEnemies.Add(item);
        //    }
        //    Debug.Log("Số lượng Enemies trong hiện thực :" + GameManager.instance.CurEnemies.Count);

        //}
        //playerData.Player(GameManager.instance.playerNew);
        //Debug.Log("Player Data :" + playerData);    
        //ScoreController.instance.score = FileManager.ReadScoreFile();
        //ScoreController.instance.scoreText.text = ScoreController.ScorePreFix + ScoreController.instance.score.ToString();
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
        ES3AutoSaveMgr.Current.Save();
        //Debug.Log("Save Player Date when quit application");
        //Application.Quit();
        //GameObject playerObject = GameObject.Find("Player");
        //if (playerObject != null)
        //{

        //    Debug.Log("Enemies :" + GameManager.instance.Enemies.Count);
        //    FileManager.SaveEnemiesData();
        //    FileManager.SavePlayerData();

        //}
    }
}
