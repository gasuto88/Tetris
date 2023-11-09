/*--------------------------------------------
 *
 * 更新日 11月9日
 * 
 * 制作者　本木　大地
 -------------------------------------------*/
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

    // ポイントの増える倍率
    private const int UP_POINT = 100;

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // TextMeshProUGUIを取得
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        // TextMeshProUGUIを取得
        _actionText = GameObject.Find("ActionText").GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// <para>ScoreDisplay</para>
    /// <para>スコアを画面に表示する</para>
    /// </summary>
    /// <param name="_scoreCount">消した段数</param>
    public void ScoreDisplay(int _eraseCount)
    {
        // 消した段数分のポイントを加算
        _scoreCount += _eraseCount * UP_POINT;

        // スコアを画面に表示
        _scoreText.text = "SCORE : " + _scoreCount.ToString();        
    }
    /// <summary>
    /// <para>ActionDisplay</para>
    /// <para>技（TETRIS、TSPINなど）を画面に表示する</para>
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
    /// <para>ActionCoroutine</para>
    /// <para>テキストの表示を消す</para>
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
