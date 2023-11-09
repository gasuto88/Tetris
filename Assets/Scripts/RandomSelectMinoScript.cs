/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

public class RandomSelectMinoScript : MonoBehaviour
{
    // テトリスミノのプレハブ--------------
    [SerializeField]
    private GameObject _iMino = default;

    [SerializeField]
    private GameObject _oMino = default;

    [SerializeField]
    private GameObject _sMino = default;

    [SerializeField]
    private GameObject _zMino = default;

    [SerializeField]
    private GameObject _jMino = default;

    [SerializeField]
    private GameObject _lMino = default;

    [SerializeField]
    private GameObject _tMino = default;

    // -------------------------------------
    private int _randomNumber = default;

    private int _selectNumber = default;

    // ミノを保管する座標
    private Transform _minoStorageTransform = default;

    /// <summary>
    /// <para>テトリスミノのテーブル</para>
    /// </summary>
    private enum MinoTable
    {
        IMINO,
        OMINO,
        SMINO,
        ZMINO,
        JMINO,
        LMINO,
        TMINO
    }

    // 7種類のミノを格納
    private MinoTable[] _minoTable = {
        MinoTable.IMINO, MinoTable.OMINO, MinoTable.SMINO,
        MinoTable.ZMINO, MinoTable.JMINO, MinoTable.LMINO, MinoTable.TMINO };

    // ミノが出てくる順番を格納する
    List<GameObject> _minoList = new List<GameObject>();

    List<int> _numberList = new List<int>();

    // ゴーストミノが出てくる順番を格納する
    List<GameObject> _ghostList = new List<GameObject>();

    // ミノが出てくる順番を格納する
    public List<GameObject> MinoList { get => _minoList; set => _minoList = value; }
    // ゴーストミノが出てくる順番を格納する
    public List<GameObject> GhostList { get => _ghostList; set => _ghostList = value; }

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // ミノを保管する座標を取得
        _minoStorageTransform = GameObject.Find("MinoStoragePosition").transform;
    }

    /// <summary>
    /// <para>RandomSelectMino</para>
    /// <para>７種類のミノを重複なくリストに入れる</para>
    /// </summary>
    public void RandomSelectMino()
    {
        // リストの中に0～7の数字を追加する
        for (int i = 0; i < 7; i++)
        {
            _numberList.Add(i);
        }

        // 0～7の数字を全部作る
        for (int j = 0; j < 7; j++)
        {
            // リストに中身が入ってたいら
            if (_numberList.Count > 0)
            {
                // 0～7の中からランダムに数字を選ぶ
                _randomNumber = Random.Range(0, _numberList.Count);

                // 選んだ数字を設定
                _selectNumber = _numberList[_randomNumber];
                                
                // 選ばれた数字を削除する
                _numberList.RemoveAt(_randomNumber);
            }

            // 選ばれた数字のミノをリストに追加する
            switch (_minoTable[_selectNumber])
            {
                // Iミノ
                case MinoTable.IMINO:
                    MinoList.Add(Instantiate(_iMino,_minoStorageTransform.position,_minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_iMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;
                
                // Oミノ
                case MinoTable.OMINO:
                    MinoList.Add(Instantiate(_oMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_oMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // Sミノ
                case MinoTable.SMINO:
                    MinoList.Add(Instantiate(_sMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_sMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // Zミノ
                case MinoTable.ZMINO:
                    MinoList.Add(Instantiate(_zMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_zMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // Jミノ
                case MinoTable.JMINO:
                    MinoList.Add(Instantiate(_jMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_jMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // Lミノ
                case MinoTable.LMINO:
                    MinoList.Add(Instantiate(_lMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_lMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // Tミノ
                case MinoTable.TMINO:
                    MinoList.Add(Instantiate(_tMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_tMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;
            }
        }
    }
}
