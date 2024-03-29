﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject prefab;
    public static ScoreController instance;
    public int score = 0;
    public const string ScorePreFix = "Score: ";
    public TextMeshProUGUI scoreText;
    public int coins = 0;
    public TextMeshProUGUI coinText;

    private void Awake()
    {
        instance = this;
    }   

    private void Update()
    {  
        scoreText.text = "Score: " + score;
        coinText.text = coins.ToString();
    }

    public void Addpoint(int points)
    {
        this.score += points;
        scoreText.text = ScorePreFix + this.score;
    }

    public void AddCoin(int coin)
    {
        this.coins += coin;
        GameManager.instance.totalCoins = this.coins;
        coinText.text = coins.ToString();
    }
}
