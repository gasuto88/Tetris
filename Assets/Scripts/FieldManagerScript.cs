/*----------------------------------------------------
 * FieldManagerScript.cs
 * フィールドを管理する
 * 
 * 作成日　11月10日
 * 
 * 制作者　本木　大地
 ---------------------------------------------------*/
using UnityEngine;

public class FieldManagerScript : MonoBehaviour
{
    // スコアを表示するスクリプト
    private ScoreScript _scoreScript = default;

    // フィールドデータ
    private GameObject[,] _fieldData = new GameObject[10,20];

    // フィールドの高さ
    private const int HEIGHT = 19;

    //private const int

    // 特殊消しの名前 -------------------------------
    private const string TETRIS = "TETRIS";
    private const string TSINGLE = "TSPIN - SINGLE";
    private const string TDOUBLE = "TSPIN - DOUBLE";
    private const string TTRIPLE = "TSPIN - TRIPLE";
    // ----------------------------------------------

    // 消した段数 -----------------------------------
    private const int DELETE_ONE = 1;
    private const int DELETE_TWO = 2;
    private const int DELETE_THREE = 3;
    // ----------------------------------------------

    // Tスピンかどうか
    private bool isTspin = default;

    // Tスピンかを調べるスクリプト
    private TSpinCheckScript _tspinCheckScript = default;

    // 操作しているミノ
    private GameObject _playerMino = default;

    private AudioSource _audioSource = default;

    [SerializeField,Header("横一列を消したときの音")]
    private AudioClip _eraseSound = default;

    [SerializeField, Header("横一列をたくさん消したときの音")]
    private AudioClip _manyEraseSound = default;

    //フィールドの情報
    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    // フィールドの高さ
    public int Height { get => HEIGHT;}
    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // ScoreScriptを取得
        _scoreScript = GameObject.Find("Canvas").GetComponent<ScoreScript>();

        // TSpinCheckScriptを取得
        _tspinCheckScript = GameObject.Find("MinoController").GetComponent<TSpinCheckScript>();

        // AudioSourceを取得
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// <para>DeleteFieldMino</para>
    /// <para>フィールドのミノを消す</para>
    /// </summary>
    public void DeleteFieldMino()
    {
        // 横一列のブロック数
        int blockCount = 0;
        
        // 消した段数
        int deleteCount = 0;

        // 操作しているミノがTミノ　かつ　Tスピンだったら
        if (_playerMino.tag == "TMino" && _tspinCheckScript.TSpinCheck(_playerMino)) 
        {
            // Tスピンしている
            isTspin = true;
        }
        else
        {
            // Tスピンしていない
            isTspin = false;
        }

        // フィールドの一番下から一つずつ上に進む
        for (int i = HEIGHT; i >= 0; i--)
        {
            // フィールドの一番左から一つずつ右に進む
            for (int j = 0; j < 10; j++)
            {
                // フィールドにブロックが置いてあったら
                if (FieldData[j, i] != null)
                {
                    // 横一列のブロック数を一つ増やす
                    blockCount++;
                }
            }
            // 横一列にブロックが全部埋まっていたら
            if (blockCount >= 10)
            {　
                // ブロックの埋まった高さから一つずつ上に進む
                for (int k = i; k > -1; k--)
                {
                    // フィールドの一番左から一つずつ右に進む
                    for (int l = 0; l < 10; l++)
                    {
                        // 現在の高さがブロックの埋まった高さと同じ　かつ　フィールドにブロックが置いてあったら
                        if (k == i && FieldData[l, i] != null)
                        {
                            // 置いてあるブロックを削除する
                            Destroy(FieldData[l, i].gameObject);

                            // そのブロックのフィールドデータを空にする
                            FieldData[l, i] = null;

                        }
                        // 一番上の段より下　かつ　一つ上の段にブロックが置いてあったら　
                        if (k > 0 && FieldData[l,k - 1] != null)
                        {
                            // 置いてあるブロックを一つ下に移動
                            FieldData[l, k - 1].transform.Translate(0f, -1f, 0f, Space.World);

                            //　一つ上の段のデータを現在のデータに上書きする
                            FieldData[l,k] = FieldData[l, k - 1];

                            // 一つ上の段のフィールドデータを空にする
                            FieldData[l, k - 1] = null;
                        }
                    }
                }
                // 次に調べる段を一つ下に下げる
                i++;

                // 消した段数
                deleteCount++;
            }
            // 横一列のブロック数を初期化
            blockCount = 0;
        }
        // 4段消したら
        if (deleteCount >= 4)
        {
            // TETRIS（技）を画面に表示する
            _scoreScript.ActionDisplay(TETRIS);

            // 横一列をたくさん消したときの音
            _audioSource.PlayOneShot(_manyEraseSound);
        }
        else if (deleteCount >= 1)
        {
            // 横一列を消したときの音
            _audioSource.PlayOneShot(_eraseSound);
        }

        // Ｔスピンだったら
        if (isTspin)
        {
            // 消した段数によって分ける
            switch (deleteCount)
            {
                // 1段
                case DELETE_ONE:

                    // TSINGLEを画面に表示する
                    _scoreScript.ActionDisplay(TSINGLE);

                    break;

                // 2段
                case DELETE_TWO:

                    // TDOUBLEを画面に表示する
                    _scoreScript.ActionDisplay(TDOUBLE);

                    break;
                // 3段
                case DELETE_THREE:

                    // TTRIPLEを画面に表示する
                    _scoreScript.ActionDisplay(TTRIPLE);

                    break;
            }
        }
        //　消した段数を送る
        _scoreScript.ScoreDisplay(deleteCount);
    }

    /// <summary>
    /// <para>SetPlayerInfo</para>
    /// <para>操作しているミノを受け取る</para>
    /// </summary>
    /// <param name="player">操作しているミノ</param>
    public void SetPlayerInfo(GameObject player)
    {
        _playerMino = player;
    }
}
