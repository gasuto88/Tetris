/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;

public class TSpinCheckScript : MonoBehaviour
{
    // �t�B�[���h���Ǘ�����X�N���v�g
    private FieldManagerScript _fieldManagerScript = default;

    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
    private void Start()
    {
        // FieldManagerScript���擾
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }

    /// <summary>
    /// <para>TSpinCheck</para>
    /// <para>T�X�s������𒲂ׂ�</para>
    /// </summary>
    /// <param name="_playerMino">����ł���~�m</param>
    /// <returns>T�X�s�����Ă��邩</returns>
    public bool TSpinCheck(GameObject _playerMino)
    {
        // ������
        int _playerPosX = Mathf.RoundToInt(_playerMino.transform.position.x);
        int _playerPosY = Mathf.RoundToInt(_playerMino.transform.position.y);

        // �u���b�N�̐�
        int _blockCount = 0;
        
        // ��]�����猩�č���
        if (_playerPosX <= 0 || _fieldManagerScript.FieldData[_playerPosX - 1, -_playerPosY - 1])
        {
            _blockCount++;
        }
        // ��]�����猩�ĉE��
        if (_playerPosX >= 9 || _fieldManagerScript.FieldData[_playerPosX + 1, - _playerPosY - 1])
        {
            _blockCount++;
        }
        // ��]�����猩�č���
        if (_playerPosX <= 0 || _playerPosY <= -19 || _fieldManagerScript.FieldData[_playerPosX - 1, -_playerPosY + 1])
        {
            _blockCount++;
        }
        // ��]�����猩�ĉE��
        if (_playerPosX >= 9 || _playerPosY <= -19 || _fieldManagerScript.FieldData[_playerPosX + 1, -_playerPosY + 1])
        {           
            _blockCount++;
        }

        // �u���b�N��3�ȏゾ������
        if(_blockCount >= 3)
        {
            // T�X�s�����Ă���
            return true;
        }
        // T�X�s�����Ă��Ȃ�
        return false;
    }
}
