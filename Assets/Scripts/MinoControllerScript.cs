using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoControllerScript : MonoBehaviour
{
    IRandomSelectMino _iRandomSelectMino = default;

    private Transform _nextPositionOne = default;

    private Transform _nextPositionTwo = default;

    private Transform _nextPositionThree = default;

    private Transform _nextPositionFour = default;

    private Transform _nextPositionFive = default;

    private Transform[] _nextPositions = new Transform[4];

    private GameObject _holdObject = default;

    private Transform _holdTransform = default;

    private GameObject _holdGhostObject = default;

    private ICreateMino _iCreateMino = default;

    private bool isHold = default;

    private int _holdCount = default;

    private GhostMinoScript _ghostMinoScript = default;

    private Transform _minoWaitTransform = default;
    public GameObject HoldObject { get => _holdObject; set => _holdObject = value; }

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;

        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;

        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        _iCreateMino = GetComponent<CreateMinoScript>();

        _ghostMinoScript = GetComponent<GhostMinoScript>();

        _holdTransform = GameObject.Find("HoldPosition").transform;

        _minoWaitTransform = GameObject.Find("MinoWaitPosition").transform;

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
        
        if(_iRandomSelectMino.MinoList.Count <= 7)
        {
            _iRandomSelectMino.RandomSelectMino();
        }
    }
    /// <summary>
    /// NEXTミノの表示
    /// </summary>
    public void NextDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            _iRandomSelectMino.MinoList[i].transform.position = _nextPositions[i].transform.position;
        }
    }
    /// <summary>
    /// ホールドの中身を出し入れする処理
    /// </summary>
    /// <param name="_holdMino"></param>
    /// <param name="_minoCreateMethod"></param>
    public void HoldController(GameObject _holdMino,GameControllerScript.MinoCreateMethod _minoCreateMethod)
    {

        if (_holdCount <= 0)
        {
            _holdCount = 2;

            if (HoldObject != null)
            {
                // ホールドに入ってるミノをリストの先頭に追加
                _iRandomSelectMino.MinoList.Insert(0, HoldObject);
                _iRandomSelectMino.GhostList.Insert(0, _holdGhostObject);
            }
            _holdObject = _holdMino;

            _holdGhostObject = _ghostMinoScript.GhostMino;

            _holdObject.transform.position = _holdTransform.position;

            _holdGhostObject.transform.position = _minoWaitTransform.position;
            
            _minoCreateMethod();
        }

        

    }

    public void HoldCount()
    {
        _holdCount--;
    }
}
