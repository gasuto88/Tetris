using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour ,ICreateMino
{
    private IRandomSelectMino _iRandomSelectMino = default;

    private IGameController _iGameController = default;

    private Transform _minoSpawnTransform = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _iGameController = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;
    }
   
    /// <summary>
    /// Nextにあるミノを生成する
    /// </summary>
    public void NextMinoInstance()
    {
        if (_iRandomSelectMino.MinoList[0].name == "OMino" || _iRandomSelectMino.MinoList[0].name == "IMino")
        {
            
        }

        // リストの先頭のミノを生成する
        _iGameController.PlayerMino = Instantiate(
            _iRandomSelectMino.MinoList[0],
            _minoSpawnTransform.position,
            _minoSpawnTransform.rotation
             );
        

        // リストの中から生成されたミノを削除する
        _iRandomSelectMino.MinoList.RemoveAt(0);
    }
}
