/*----------------------------------------------------
  FieldManagerScript.cs
  
  作成日　11月10日
  
  作成者　本木　大地
 ---------------------------------------------------*/
using UnityEngine;

/// <summary>
/// フィールドを管理する
/// </summary>
public class FieldManagerScript : MonoBehaviour
{
    #region 定数

    // フィールドの縦幅と横幅
    private const int HEIGHT = 19;
    private const int WIDTH = 10;

    // 特殊消しの名前 ----------------------------------------
    private const string TETRIS = "TETRIS";
    private const string TSINGLE = "TSPIN SINGLE";
    private const string TDOUBLE = "TSPIN DOUBLE";
    private const string TTRIPLE = "TSPIN TRIPLE";
    // -------------------------------------------------------

    // オブジェクトのタグの名前 ------------------------------
    private const string SCORE_CANVAS = "ScoreCanvas";
    private const string MINO_CONTROLLER = "MinoController";
    private const string GAME_CONTROLLER = "GameController";
    // -------------------------------------------------------
    #endregion

    #region フィールド変数

    // スコアを表示するスクリプト
    private ScoreManagerScript _scoreScript = default;

    // Tスピンかを調べるスクリプト
    private TSpinCheckScript _tspinCheckScript = default;

    // 音を再生するスクリプト
    private AudioManagerScript _audioManagerScript = default;

    // フィールドデータ
    private GameObject[,] _fieldData = new GameObject[10,20];

    #endregion

    //フィールドの情報
    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    // フィールドの高さ
    public int Height { get => HEIGHT;}

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // ScoreScriptを取得
        _scoreScript = GameObject.FindGameObjectWithTag(SCORE_CANVAS).GetComponent<ScoreManagerScript>();

        // TSpinCheckScriptを取得
        _tspinCheckScript = GameObject.FindGameObjectWithTag(MINO_CONTROLLER).GetComponent<TSpinCheckScript>();

        // AudioManagerScriptを取得
        _audioManagerScript = GameObject.FindGameObjectWithTag(GAME_CONTROLLER).GetComponent<AudioManagerScript>();
    }

    /// <summary>
    /// DeleteMino
    /// フィールドのミノを消す
    /// </summary>
    public void DeleteMino()
    {       
        // 消した段数
        int deleteCount = 0;

        // フィールドの1番下から1番上まで進む
        for (int i = HEIGHT; i >= 0; i--)
        {           
            // 横1列にブロックが全部埋まっていたら
            if (CheckDeleteMino(i))
            {
                // ブロックの埋まった段から1番上まで進む
                for (int k = i; k >= 0; k--)
                {
                    // フィールドの1番左から1番右まで進む
                    for (int l = 0; l < WIDTH; l++)
                    {
                        // 現在の高さがブロックの埋まった高さと同じ　かつ　フィールドにブロックが置いてあったら
                        if (k == i && FieldData[l, i] != null)
                        {
                            // ブロックを削除する
                            Destroy(FieldData[l, i].gameObject);

                            // そのブロックのフィールドデータを初期化
                            FieldData[l, i] = null;

                        }
                        // 1番上の段より下　かつ　1つ上の段にブロックが置いてあったら　
                        if (k > 0 && FieldData[l, k - 1] != null)
                        {
                            // ブロックを1つ下に移動
                            FieldData[l, k - 1].transform.Translate(-Vector3.up, Space.World);

                            //　1つ上の段のデータを現在のデータに上書きする
                            FieldData[l, k] = FieldData[l, k - 1];

                            // 1つ上の段のフィールドデータを初期化
                            FieldData[l, k - 1] = null;
                        }
                    }
                }

                // 次に調べる段を1つ下げる
                i++;

                // 消した段数
                deleteCount++;
            }
        }

        // 消した段数が1以上だったら
        if (deleteCount >= 1)
        {
            // 表示する文字を選択する
            SelectDisplayText(deleteCount);

            //　消した段数を送る
            _scoreScript.ScoreDisplay(deleteCount);
        }
    }

    /// <summary>
    /// 横1列にブロックが全部埋まっているか
    /// </summary>
    /// <param name="row">段</param>
    /// <returns>ブロックが全部埋まっているか</returns>
    private bool CheckDeleteMino(int row)
    {
        // フィールドの1番左から1番右まで進む
        for (int j = 0; j < WIDTH; j++)
        {
            // フィールドにブロックが置いていなかったら
            if (FieldData[j, row] == null)
            {
                // ブロックが全部埋まっていない
                return false;
            }
        }

        // ブロックが全部埋まっている
        return true;
    } 
    /// <summary>
    /// 表示する文字を選択する
    /// </summary>
    /// <param name="deleteCount">消した段数</param>
    private void SelectDisplayText(int deleteCount)
    {
        // 4段消したら
        if (deleteCount >= 4)
        {
            // TETRISを画面に表示する
            _scoreScript.ActionDisplay(TETRIS);

            // ミノを４段消した時の音を再生
            _audioManagerScript.ManyDeleteSound();
        }
        // 1段以上消したら
        else if (deleteCount >= 1)
        {
            // ミノを消した時の音を再生
            _audioManagerScript.DeleteSound();
        }

        // Ｔスピンだったら
        if (_tspinCheckScript.IsTSpin)
        {
            // 消した段数によって分ける
            switch (deleteCount)
            {
                // 1段
                case 1:
                    // TSINGLEを画面に表示する
                    _scoreScript.ActionDisplay(TSINGLE);
                    break;

                // 2段
                case 2:
                    // TDOUBLEを画面に表示する
                    _scoreScript.ActionDisplay(TDOUBLE);
                    break;

                // 3段
                case 3:
                    // TTRIPLEを画面に表示する
                    _scoreScript.ActionDisplay(TTRIPLE);
                    break;
            }
        }
    }
}
