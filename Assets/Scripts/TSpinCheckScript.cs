/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;

public class TSpinCheckScript : MonoBehaviour
{
    // フィールドを管理するスクリプト
    private FieldManagerScript _fieldManagerScript = default;

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // FieldManagerScriptを取得
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }

    /// <summary>
    /// <para>TSpinCheck</para>
    /// <para>Tスピン判定を調べる</para>
    /// </summary>
    /// <param name="_playerMino">操作できるミノ</param>
    /// <returns>Tスピンしているか</returns>
    public bool TSpinCheck(GameObject _playerMino)
    {
        // 整数化
        int _playerPosX = Mathf.RoundToInt(_playerMino.transform.position.x);
        int _playerPosY = Mathf.RoundToInt(_playerMino.transform.position.y);

        // ブロックの数
        int _blockCount = 0;
        
        // 回転軸から見て左上
        if (_playerPosX <= 0 || _fieldManagerScript.FieldData[_playerPosX - 1, -_playerPosY - 1])
        {
            _blockCount++;
        }
        // 回転軸から見て右上
        if (_playerPosX >= 9 || _fieldManagerScript.FieldData[_playerPosX + 1, - _playerPosY - 1])
        {
            _blockCount++;
        }
        // 回転軸から見て左下
        if (_playerPosX <= 0 || _playerPosY <= -19 || _fieldManagerScript.FieldData[_playerPosX - 1, -_playerPosY + 1])
        {
            _blockCount++;
        }
        // 回転軸から見て右下
        if (_playerPosX >= 9 || _playerPosY <= -19 || _fieldManagerScript.FieldData[_playerPosX + 1, -_playerPosY + 1])
        {           
            _blockCount++;
        }

        // ブロックが3個以上だったら
        if(_blockCount >= 3)
        {
            // Tスピンしている
            return true;
        }
        // Tスピンしていない
        return false;
    }
}
