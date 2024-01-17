/*--------------------------------------------
 *
 * 更新日 11月9日
 * 
 * 制作者　本木　大地
 -------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    #region 定数

    // ポイントの増える倍率
    private const int UP_POINT = 100;

    // Tagの名前 -----------------------------------
    private const string SCORE_TEXT = "ScoreText";
    private const string ACTION_TEXT = "ActionText";
    // ---------------------------------------------

    #endregion


    // スコアを表示するテキスト
    private Text _scoreText = default;

    // 特殊消し（TETRIS、TSPINなど）を表示するテキスト
    private Text _actionText = default;

    // 現在のスコア
    private int _scoreCount = default;

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // TextMeshProUGUIを取得
        _scoreText = GameObject.FindGameObjectWithTag(SCORE_TEXT).GetComponent<Text>();

        // TextMeshProUGUIを取得
        _actionText = GameObject.FindGameObjectWithTag(ACTION_TEXT).GetComponent<Text>();
    }
    /// <summary>
    /// ScoreDisplay
    /// スコアを画面に表示する
    /// </summary>
    /// <param name="_scoreCount">消した段数</param>
    public void ScoreDisplay(int eraseCount)
    {
        // 消した段数分のポイントを加算
        _scoreCount += eraseCount * UP_POINT;

        // スコアを画面に表示
        _scoreText.text = "SCORE " + _scoreCount.ToString();        
    }

    /// <summary>
    /// ActionDisplay
    /// 特殊消し（TETRIS、TSPINなど）を画面に表示する
    /// </summary>
    /// <param name="action">技の名前</param>
    public void ActionDisplay(string action)
    {
        // 特殊消しを画面に表示
        _actionText.text = action;

        // 時間差でテキストの表示を消す
        StartCoroutine(ActionCoroutine());
    }

    /// <summary>
    /// ActionCoroutine
    /// テキストの表示を消す
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
