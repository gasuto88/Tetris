using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSelectMinoScript : MonoBehaviour,IRandomSelectMino
{
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

    private int _randomNumber = default;

    private int _selectNumber = default;

    private Transform _minoWaitTransform = default;

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

    private MinoTable[] _minoTable = {
        MinoTable.IMINO, MinoTable.OMINO, MinoTable.SMINO,
        MinoTable.ZMINO, MinoTable.JMINO, MinoTable.LMINO, MinoTable.TMINO };

    List<GameObject> _minoList = new List<GameObject>();

    List<int> _numberList = new List<int>();

    List<GameObject> _ghostList = new List<GameObject>(); 

    public List<GameObject> MinoList { get => _minoList; set => _minoList = value; }
    public List<GameObject> GhostList { get => _ghostList; set => _ghostList = value; }

    private void Start()
    {
        _minoWaitTransform = GameObject.Find("MinoWaitPosition").transform;
    }

    /// <summary>
    /// ７種類のミノを重複なくリストに入れる
    /// </summary>
    public void RandomSelectMino()
    {
        // リストの中に0～7の数字を追加する
        for (int i = 0; i < 7; i++)
        {
            _numberList.Add(i);
        }

        for (int j = 0; j < 7; j++)
        {
            if (_numberList.Count > 0)
            {
                // 0～7の中からランダムに数字を選ぶ
                _randomNumber = Random.Range(0, _numberList.Count);

                _selectNumber = _numberList[_randomNumber];
                                
                // 選ばれた数字を削除する
                _numberList.RemoveAt(_randomNumber);
            }

            // 選ばれた数字のミノをリストに追加する
            switch (_minoTable[_selectNumber])
            {
                case MinoTable.IMINO:
                    MinoList.Add(Instantiate(_iMino,_minoWaitTransform.position,_minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_iMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;

                case MinoTable.OMINO:
                    MinoList.Add(Instantiate(_oMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_oMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;

                case MinoTable.SMINO:
                    MinoList.Add(Instantiate(_sMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_sMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;

                case MinoTable.ZMINO:
                    MinoList.Add(Instantiate(_zMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_zMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;

                case MinoTable.JMINO:
                    MinoList.Add(Instantiate(_jMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_jMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;

                case MinoTable.LMINO:
                    MinoList.Add(Instantiate(_lMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_lMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;

                case MinoTable.TMINO:
                    MinoList.Add(Instantiate(_tMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    GhostList.Add(Instantiate(_tMino, _minoWaitTransform.position, _minoWaitTransform.rotation));
                    break;
            }
        }
    }
}
