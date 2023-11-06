using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{
    private NormalMinoRotationScript _normalMinoRotationScript = default;

    private IMinoRotationScript _iMinoRotationScript = default;

    private void Start()
    {
        _normalMinoRotationScript = GetComponent<NormalMinoRotationScript>();

        _iMinoRotationScript = GetComponent<IMinoRotationScript>();
    }

    public void SuperRotation(GameObject _playerMino,int _input)
    {
        // プレイヤーがTミノとIミノ以外だったら
        if (_playerMino.tag != "IMino")
        {
            _normalMinoRotationScript.NormalMinoRotation(_playerMino, _input);
        }
        // プレイヤーがIミノだったら
        else if(_playerMino.tag == "IMino")
        {
            _iMinoRotationScript.IMinoRotation(_playerMino,_input);
        }      
    }
}
