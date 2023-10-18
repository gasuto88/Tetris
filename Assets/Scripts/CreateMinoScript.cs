using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour ,ICreateMino
{
    private IRandomSelectMino _iRandomSelectMino = default;

    private IGameController _iGameController = default;

    private Transform _instanceMinoTransform = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _iGameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        _instanceMinoTransform = GameObject.Find("InstanceMinoPosition").transform;
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        NextMinoInstance();
    //    }
    //}

    /// <summary>
    /// Nextにあるミノを生成する
    /// </summary>
    public void NextMinoInstance()
    {
        // リストの先頭のミノを生成する
        _iGameController.PlayerMino = Instantiate(
            _iRandomSelectMino.MinoList[0],
            _instanceMinoTransform.position,
            _instanceMinoTransform.rotation);
         
        // リストの中から生成されたミノを削除する
        _iRandomSelectMino.MinoList.RemoveAt(0);
    }
}
