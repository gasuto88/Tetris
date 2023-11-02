using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDataScript : MonoBehaviour
{
    // フィールドのオブジェクト
    // 0 空白
    // 1 動かないミノ
   
    private const int NO_BLOCK = 0;
    private const int STATIC_MINO = 1;

    private ScoreScript _scoreScript = default;

    // フィールドデータ
    private GameObject[,] _fieldData = new GameObject[20,10];

    private int _height = 19;

    private int _width = 9;

    private const string TETRIS = "TETRIS";

    private const string TSINGLE = "TSPIN - SINGLE";

    private const string TDOUBLE = "TSPIN - DOUBLE";

    private const string TTRIPLE = "TSPIN - TRIPLE";

    private bool isTspin = default;

    private TSpinCheckScript _tspinCheckScript = default;

    private GameObject _playerMino = default;

    private AudioSource _audioSource = default;

    [SerializeField,Header("横一列を消したときの音")]
    private AudioClip _eraseSound = default;

    [SerializeField, Header("横一列をたくさん消したときの音")]
    private AudioClip _manyEraseSound = default;
    //{
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0}

    //};

    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }

    private void Start()
    {
        _scoreScript = GameObject.Find("Canvas").GetComponent<ScoreScript>();

        _tspinCheckScript = GameObject.Find("MinoController").GetComponent<TSpinCheckScript>();

        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// ミノが横一列埋まったら消す処理
    /// </summary>
    public void FieldMinoErase()
    {
        int blockCount = 0;
        int _scoreCount = 0;
        int _eraseCount = 0;
        Debug.LogWarning(_playerMino);
        if (_playerMino.tag == "TMino" && _tspinCheckScript.TSpinCheck(_playerMino)) 
        {
            isTspin = true;
        }
        else
        {
            isTspin = false;
        }

        for (int i = 19; i > -1; i--)
        {
            for (int j = 0; j < 10; j++)
            {
                if (FieldData[i, j] != null)
                {
                    blockCount++;
                }
            }
            // 横一列揃っていたら
            if (blockCount >= 10)
            {
                for (int k = i; k > -1; k--)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        if (k == i && FieldData[i, l] != null)
                        {
                            Destroy(FieldData[i, l].gameObject);

                            FieldData[i, l] = null;

                        }
                        if (k > 0 && FieldData[k - 1, l] != null)
                        {
                            FieldData[k - 1, l].transform.Translate(0f, -1f, 0f, Space.World);

                            //　消す段に一個上の段を上書きする
                            FieldData[k, l] = FieldData[k - 1, l];

                            FieldData[k - 1, l] = null;
                        }
                    }
                }
                i++;

                _eraseCount++;
                Debug.LogError(_eraseCount);
                _scoreCount += 100;

            }
            blockCount = 0;
        }
        // 4段消したら
        if (_eraseCount >= 4)
        {
            _scoreScript.ActionDisplay(TETRIS);
            _audioSource.PlayOneShot(_manyEraseSound);
        }
        else if (_eraseCount >= 1)
        {
            _audioSource.PlayOneShot(_eraseSound);
        }

        if (isTspin)
        {
            Debug.LogWarning("消した段数　" + _eraseCount);
            if (_eraseCount == 1)
            {
                _scoreScript.ActionDisplay(TSINGLE);
            }
            else if (_eraseCount == 2)
            {
                _scoreScript.ActionDisplay(TDOUBLE);
            }
            else if (_eraseCount == 3)
            {
                _scoreScript.ActionDisplay(TTRIPLE);
            }
        }

            _scoreScript.ScoreDisplay(_scoreCount);
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

    public void SetPlayerPosition(GameObject _player)
    {
        _playerMino = _player;
    }
}
