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

    private GameObject[] _holdObject = new GameObject[1];

    private Transform _holdTransform = default;

    private ICreateMino _iCreateMino = default;

    private bool isHold = default;

    private int _holdCount = default;

    public GameObject[] HoldObject { get => _holdObject; set => _holdObject = value; }

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;

        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;

        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        _iCreateMino = GetComponent<CreateMinoScript>();

        _holdTransform = GameObject.Find("HoldPosition").transform;

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
        if (!isHold)
        {
            isHold = true;

            if (HoldObject[0] != null)
            {
                // ホールドに入ってるミノをリストの先頭に追加
                _iRandomSelectMino.MinoList.Insert(0, HoldObject[0]);
            }
            _holdObject[0] = _holdMino;

            _holdObject[0].transform.position = _holdTransform.position;

            _holdCount = 0;

            _minoCreateMethod();
        }
        else
        {
            if(_holdCount > 1)
            {
                isHold = false;
            }
        }
    }

    public void HoldCount()
    {
        _holdCount++;
    }
}
