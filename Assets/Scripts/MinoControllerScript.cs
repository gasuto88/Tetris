using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoControllerScript : MonoBehaviour
{
    #region フィールド変数

    // Nextの一番目の座標
    private Transform _nextPositionOne = default;
    // Nextの二番目の座標
    private Transform _nextPositionTwo = default;
    // Nextの三番目の座標
    private Transform _nextPositionThree = default;

    // Nextの座標を入れる配列
    private Transform[] _nextPositions = new Transform[4];

    // Holdのゲームオブジェクト
    private GameObject _holdObject = default;

    // Holdの座標
    private Transform _holdTransform = default;

    // Holdのゲームオブジェクト（ゴーストミノ用）
    private GameObject _holdGhostObject = default;

    // Holdをした後のクールタイム
    private int _holdCount = default;

    // ランダム化された7種類のミノテーブルを作成するスクリプト
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // ゲームの状態を管理するスクリプト
    private GameControllerScript _gameControllerScript = default;

    // ゴーストミノを動かすスクリプト
    private GhostMinoScript _ghostMinoScript = default;

    // ミノを保管する座標
    private Transform _minoStorageTransform = default;

    #endregion

    private void Start()
    {
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        _randomSelectMinoScript = GetComponent<RandomSelectMinoScript>();

        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;

        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;

        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        _ghostMinoScript = GetComponent<GhostMinoScript>();

        _holdTransform = GameObject.Find("HoldPosition").transform;

        _minoStorageTransform = GameObject.Find("MinoStoragePosition").transform;



    }
    private void Update()
    {
        AddMino();       
    }
    /// <summary>
    /// ミノの個数を管理する
    /// </summary>
    private void AddMino()
    {
        
        if(_randomSelectMinoScript.MinoList.Count <= 7)
        {
            _randomSelectMinoScript.RandomSelectMino();
        }
    }
    /// <summary>
    /// NEXTミノの表示
    /// </summary>
    public void NextDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            _randomSelectMinoScript.MinoList[i].transform.position = _nextPositions[i].transform.position;
        }
    }
    /// <summary>
    /// Holdの中身を出し入れする処理
    /// </summary>
    /// <param name="_holdMino"></param>
    /// <param name="_minoCreateMethod"></param>
    public void HoldController(GameObject _holdMino)
    {

        if (_holdCount <= 0)
        {
            _holdCount = 2;

            if (_holdObject != null)
            {
                // Holdに入ってるミノをリストの先頭に追加
                _randomSelectMinoScript.MinoList.Insert(0, _holdObject);
                _randomSelectMinoScript.GhostList.Insert(0, _holdGhostObject);
            }
            _holdObject = _holdMino;

            _holdGhostObject = _ghostMinoScript.GhostMino;

            _holdObject.transform.position = _holdTransform.position;         

            _holdGhostObject.transform.position = _minoStorageTransform.position;

            _holdObject.transform.rotation = default;
            _holdGhostObject.transform.rotation = default;

            _gameControllerScript.GameType = GameControllerScript.GameState.MINO_CREATE;
        }

        

    }

    public void HoldCount()
    {
        _holdCount--;
    }
}
