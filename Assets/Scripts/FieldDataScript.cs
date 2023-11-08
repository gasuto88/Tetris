using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDataScript : MonoBehaviour
{
    // スコアを表示するスクリプト
    private ScoreScript _scoreScript = default;

    // フィールドデータ
    private GameObject[,] _fieldData = new GameObject[10,20];

    // フィールドの高さ
    private int _height = 19;

    // 技の名前--------------------------------------
    private const string TETRIS = "TETRIS";
    private const string TSINGLE = "TSPIN - SINGLE";
    private const string TDOUBLE = "TSPIN - DOUBLE";
    private const string TTRIPLE = "TSPIN - TRIPLE";
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

    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    public int Height { get => _height;}

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
    /// ミノが横一列埋まったら消す処理
    /// </summary>
    public void FieldMinoErase()
    {
        // 横一列のブロック数
        int blockCount = 0;
        
        // 消した段数
        int _eraseCount = 0;
        // 操作しているミノがTミノ　かつ　Tスピンだったら
        if (_playerMino.tag == "TMino" && _tspinCheckScript.TSpinCheck(_playerMino)) 
        {
            isTspin = true;
        }
        else
        {
            isTspin = false;
        }

        // フィールドの一番下から一つずつ上に進む
        for (int i = 19; i > -1; i--)
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
                _eraseCount++;
            }
            // 横一列のブロック数を初期化
            blockCount = 0;
        }
        // 4段消したら
        if (_eraseCount >= 4)
        {
            // TETRIS（技）を画面に表示する
            _scoreScript.ActionDisplay(TETRIS);

            // 横一列をたくさん消したときの音
            _audioSource.PlayOneShot(_manyEraseSound);
        }
        else if (_eraseCount >= 1)
        {
            // 横一列を消したときの音
            _audioSource.PlayOneShot(_eraseSound);
        }

        // Ｔスピンだったら
        if (isTspin)
        {  
            // 消した段数が一段だったら
            if (_eraseCount == 1)
            {
                // TSINGLE（技）を画面に表示する
                _scoreScript.ActionDisplay(TSINGLE);
            }
            // 消した段数が二段だったら
            else if (_eraseCount == 2)
            {
                // TDOUBLE（技）を画面に表示する
                _scoreScript.ActionDisplay(TDOUBLE);
            }
            // 消した段数が三段だったら
            else if (_eraseCount == 3)
            {
                // TTRIPLE（技）を画面に表示する
                _scoreScript.ActionDisplay(TTRIPLE);
            }
        }
        //　スコアを画面に表示
        _scoreScript.ScoreDisplay(_eraseCount);

        ////デバック用
        //for (int j = 0; j < 20; j++)
        //{
        //    string unti = "";
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (FieldData[j, i] != null)
        //        {
        //            unti += 1;
        //        }
        //        else
        //        {
        //            unti += 0;
        //        }
        //    }
        //    Debug.LogWarning(unti);
        //}
    }
    /// <summary>
    /// 操作しているミノを受け取る
    /// </summary>
    /// <param name="_player">操作しているミノ</param>
    public void SetPlayerInfo(GameObject _player)
    {
        _playerMino = _player;
    }
}
