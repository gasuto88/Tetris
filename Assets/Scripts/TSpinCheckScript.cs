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
        
        // ��]�����猩�č���
        if (_playerPosX <= 0 || _fieldDataScript.FieldData[-_playerPosY + 1,_playerPosX - 1])
        {
            Debug.LogWarning("����");
            _blockCount++;
        }
        // ��]�����猩�ĉE��
        if (_playerPosX >= 9 || _fieldDataScript.FieldData[-_playerPosY - 1, _playerPosX + 1])
        {
            Debug.LogWarning("�E��");
            _blockCount++;
        }
        // ��]�����猩�č���
        if (_playerPosX <= 0 || _playerPosY <= -19 || _fieldDataScript.FieldData[-_playerPosY - 1, _playerPosX - 1])
        {
            Debug.LogWarning("����");
            _blockCount++;
        }
        // ��]�����猩�ĉE��
        if (_playerPosX >= 9 || _playerPosY <= -19 || _fieldDataScript.FieldData[-_playerPosY + 1, _playerPosX + 1])
        {
            Debug.LogWarning("�E��");
            _blockCount++;
        }

        Debug.LogWarning(_blockCount);

        if(_blockCount >= 3)
        {
            return true;
        }
        return false;
    }
}
