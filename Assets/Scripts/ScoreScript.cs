using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    private TextMeshProUGUI _scoreText = default;

    private void Start()
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    public void ScoreDisplay(int _scoreCount)
    {
        _scoreText.text = "SCORE : " + _scoreCount.ToString();
    }
}
