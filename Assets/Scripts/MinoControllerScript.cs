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

    private Transform[] _nextData = new Transform[3];

    private Transform[] _nextPositions = new Transform[4];

    private ICreateMino _iCreateMino = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;

        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;

        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        _iCreateMino = GameObject.Find("MinoController").GetComponent<CreateMinoScript>();

    }
    private void Update()
    {
        AddMino();       
    }
    /// <summary>
    /// É~ÉmÇÃå¬êîÇä«óùÇ∑ÇÈ
    /// </summary>
    private void AddMino()
    {
        
        if(_iRandomSelectMino.MinoList.Count <= 7)
        {
            _iRandomSelectMino.RandomSelectMino();
        }
    }

    private void NextDisplay()
    {
        //for (int i = 2; i > -1; i--)
        //{
        //    if ()
        //    {

        //    }
        //    _iRandomSelectMino.MinoList[0].transform.position = _nextPositions[i].transform.position;
        //}
    }
}
