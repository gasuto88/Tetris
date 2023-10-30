using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour ,ICreateMino
{
    private IRandomSelectMino _iRandomSelectMino = default;

    private PlayerControllerScript _playerControllerScript = default;

    private Transform _minoSpawnTransform = default;

    private Transform _oIMinoSpawnTransform = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _playerControllerScript = GameObject.Find("GameController").GetComponent<PlayerControllerScript>();

        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;

        _oIMinoSpawnTransform = GameObject.Find("O_IMinoSpawnPosition").transform;
    }
   
    /// <summary>
    /// Nextにあるミノを生成する
    /// </summary>
    public void NextMinoInstance()
    {
        
        // OMinoとIMinoじゃなかったら
        if (_iRandomSelectMino.MinoList[0].tag != "OMino" && _iRandomSelectMino.MinoList[0].tag != "IMino")
        {
            _iRandomSelectMino.MinoList[0].transform.position = _minoSpawnTransform.position;

        }
        // OMinoとIMInoのとき
        else
        {
            _iRandomSelectMino.MinoList[0].transform.position = _oIMinoSpawnTransform.position;
        }

        _playerControllerScript.PlayerMino = _iRandomSelectMino.MinoList[0];

        //// リストの先頭のミノを生成する
        //_playerControllerScript.PlayerMino = Instantiate(
        //    _iRandomSelectMino.MinoList[0],
        //    tempTransform.position,
        //    tempTransform.rotation
        //     );

        // リストの中から生成されたミノを削除する
        _iRandomSelectMino.MinoList.RemoveAt(0);
    }
}
