using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TSpinCheckScript : MonoBehaviour
{
    private FieldDataScript _fieldDataScript = default;
    private void Start()
    {
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
    }

    public bool TSpinCheck(GameObject _playerMino)
    {
        int _playerPosX = Mathf.RoundToInt(_playerMino.transform.position.x);
        int _playerPosY = Mathf.RoundToInt(_playerMino.transform.position.y);
        int _blockCount = 0;
        
        // 回転軸から見て左上
        if (_playerPosX <= 0 || _fieldDataScript.FieldData[_playerPosX - 1, -_playerPosY - 1])
        {
            _blockCount++;
        }
        // 回転軸から見て右上
        if (_playerPosX >= 9 || _fieldDataScript.FieldData[_playerPosX + 1, - _playerPosY - 1])
        {
            _blockCount++;
        }
        // 回転軸から見て左下
        if (_playerPosX <= 0 || _playerPosY <= -19 || _fieldDataScript.FieldData[_playerPosX - 1, -_playerPosY + 1])
        {
            _blockCount++;
        }
        // 回転軸から見て右下
        if (_playerPosX >= 9 || _playerPosY <= -19 || _fieldDataScript.FieldData[_playerPosX + 1, -_playerPosY + 1])
        {           
            _blockCount++;
        }

        // 3個以上だったら
        if(_blockCount >= 3)
        {
            return true;
        }
        return false;
    }
}
