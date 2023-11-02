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
        
        // âÒì]é≤Ç©ÇÁå©Çƒç∂è„
        if (_playerPosX <= 0 || _fieldDataScript.FieldData[-_playerPosY + 1,_playerPosX - 1])
        {
            Debug.LogWarning("ç∂è„");
            _blockCount++;
        }
        // âÒì]é≤Ç©ÇÁå©ÇƒâEè„
        if (_playerPosX >= 9 || _fieldDataScript.FieldData[-_playerPosY - 1, _playerPosX + 1])
        {
            Debug.LogWarning("âEè„");
            _blockCount++;
        }
        // âÒì]é≤Ç©ÇÁå©Çƒç∂â∫
        if (_playerPosX <= 0 || _playerPosY <= -19 || _fieldDataScript.FieldData[-_playerPosY - 1, _playerPosX - 1])
        {
            Debug.LogWarning("ç∂â∫");
            _blockCount++;
        }
        // âÒì]é≤Ç©ÇÁå©ÇƒâEâ∫
        if (_playerPosX >= 9 || _playerPosY <= -19 || _fieldDataScript.FieldData[-_playerPosY + 1, _playerPosX + 1])
        {
            Debug.LogWarning("âEâ∫");
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
