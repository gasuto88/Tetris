using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    private TextMeshProUGUI _scoreText = default;

    private TextMeshProUGUI _actionText = default;

    private void Start()
    {
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        _actionText = GameObject.Find("ActionText").GetComponent<TextMeshProUGUI>();
    }

    public void ScoreDisplay(int _scoreCount)
    {
        _scoreText.text = "SCORE : " + _scoreCount.ToString();

        
    }

    public void ActionDisplay(string _action)
    {
        _actionText.text = _action;

        StartCoroutine(ActionCoroutine());
    }

    private IEnumerator ActionCoroutine()
    {
        yield return new WaitForSeconds(2);

        _actionText.text = "";
    }
}
