using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{

    private TMinoRotationScript _tMinoRotationScript = default;

    private void Start()
    {
        _tMinoRotationScript = GetComponent<TMinoRotationScript>();
    }

    public void SuperRotation(GameObject _playerMino,int _input)
    {
        // プレイヤーがTミノとIミノ以外だったら
        if (_playerMino.tag != "TMino" && _playerMino.tag != "IMino")
        {

        }
        // プレイヤーがTミノだったら
        else if(_playerMino.tag == "TMino")
        {
            _tMinoRotationScript.TMinoRotation(_playerMino,_input);
        }
        // プレイヤーがIミノだったら
        else if(_playerMino.tag == "IMino")
        {

        }      
    }
}
