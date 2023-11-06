using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{
    private TMinoRotationScript _tMinoRotationScript = default;

    private NormalMinoRotationScript _normalMinoRotationScript = default;

    private IMinoRotationScript _iMinoRotationScript = default;

    private void Start()
    {
        _tMinoRotationScript = GetComponent<TMinoRotationScript>();

        _normalMinoRotationScript = GetComponent<NormalMinoRotationScript>();

        _iMinoRotationScript = GetComponent<IMinoRotationScript>();
    }

    public void SuperRotation(GameObject _playerMino,int _input)
    {
        // プレイヤーがTミノとIミノ以外だったら
        if (_playerMino.tag != "TMino" && _playerMino.tag != "IMino")
        {
            _normalMinoRotationScript.OtherMinoRotation(_playerMino, _input);
        }
        // プレイヤーがTミノだったら
        else if(_playerMino.tag == "TMino")
        {
            _tMinoRotationScript.TMinoRotation(_playerMino,_input);
        }
        // プレイヤーがIミノだったら
        else if(_playerMino.tag == "IMino")
        {
            _iMinoRotationScript.IMinoRotation(_playerMino,_input);
        }      
    }
}
