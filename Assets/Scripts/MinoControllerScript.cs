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

    private Transform[] _nextData = new Transform[5];

    private Transform[] _nextPositions = new Transform[5];

    private ICreateMino _iCreateMino = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;

        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;

        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        _nextPositionFour = GameObject.Find("NextPosition (4)").transform;

        _nextPositionFive = GameObject.Find("NextPosition (5)").transform;

        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree, _nextPositionFour, _nextPositionFive };

        _iCreateMino = GameObject.Find("MinoController").GetComponent<CreateMinoScript>();

    }
    private void Update()
    {
        AddMino();
        NextDisplay();
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

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (_nextData[j] == null)
                {

                    //_nextData[j] = _iCreateMino.NextMinoInstance(_nextPositions[]);

                    break;
                }

            }

        }
        
    }
}
