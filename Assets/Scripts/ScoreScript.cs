using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
　　// スコアを表示するテキスト
    private TextMeshProUGUI _scoreText = default;

    // 技（TETRIS、TSPINなど）を表示するテキスト
    private TextMeshProUGUI _actionText = default;

    // 現在のスコア
    private int _scoreCount = default;

    private void Start()
    {
        // TextMeshProUGUIを取得
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        // TextMeshProUGUIを取得
        _actionText = GameObject.Find("ActionText").GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// スコアを画面に表示する処理
    /// </summary>
    /// <param name="_scoreCount">消した段数</param>
    public void ScoreDisplay(int _eraseCount)
    {
        _scoreCount += _eraseCount * 100;

        // スコアを画面に表示
        _scoreText.text = "SCORE : " + _scoreCount.ToString();        
    }
    /// <summary>
    /// 技（TETRIS、TSPINなど）を画面に表示する処理
    /// </summary>
    /// <param name="_action">技の名前</param>
    public void ActionDisplay(string _action)
    {
        // 技を画面に表示
        _actionText.text = _action;

        // 時間差でテキストの表示を消す
        StartCoroutine(ActionCoroutine());
    }
    /// <summary>
    /// テキストの表示を消す処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActionCoroutine()
    {
        // 2秒まつ
        yield return new WaitForSeconds(2);

        // テキストの表示を消す
        _actionText.text = "";
    }
}
