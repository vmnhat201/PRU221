using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public GameObject prefab;
    public static ScoreController instance;
    private int score = 0;
    const string ScorePreFix = "Score: ";
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        instance = this;
    }

    public void Addpoint(int points)
    {
        score += points;
        scoreText.text = ScorePreFix + score.ToString();
    }
}
