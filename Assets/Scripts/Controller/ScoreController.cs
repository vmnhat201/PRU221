using System.Collections;
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

    private void Awake()
    {
        instance = this;
        Debug.Log("ScoreController Awake");
    }

    public void Addpoint(int points)
    {
        score += points;
        scoreText.text = ScorePreFix + score.ToString();
        FileManager.WriteToFile("Score.txt", score.ToString());
    }
}
