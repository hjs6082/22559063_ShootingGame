using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI nowScoreUI;
    public int nowScore;
    public TextMeshProUGUI bestScoreUI;
    public int bestScore;

    public void Start()
    {
        bestScore =  PlayerPrefs.GetInt("bestscore");
        bestScoreUI.text = "Best Score : " + bestScore.ToString();
    }
}
