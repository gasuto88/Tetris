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
    /// <param name="playerMino">����ł���~�m</param>
    /// <returns>T�X�s�����Ă��邩</returns>
    public bool TSpinCheck(GameObject playerMino)
    {
        // ������
        int playerPosX = Mathf.RoundToInt(playerMino.transform.position.x);
        int playerPosY = Mathf.RoundToInt(playerMino.transform.position.y);

        // �u���b�N�̐�
        int blockCount = 0;
        
        // ��]�����猩�č���
        if (playerPosX <= 0 || _fieldManagerScript.FieldData[playerPosX - 1, -playerPosY - 1])
        {
            blockCount++;
        }
        // ��]�����猩�ĉE��
        if (playerPosX >= 9 || _fieldManagerScript.FieldData[playerPosX + 1, - playerPosY - 1])
        {
            blockCount++;
        }
        // ��]�����猩�č���
        if (playerPosX <= 0 || playerPosY <= -19 || _fieldManagerScript.FieldData[playerPosX - 1, -playerPosY + 1])
        {
            blockCount++;
        }
        // ��]�����猩�ĉE��
        if (playerPosX >= 9 || playerPosY <= -19 || _fieldManagerScript.FieldData[playerPosX + 1, -playerPosY + 1])
        {           
            blockCount++;
        }

        // �u���b�N��3�ȏゾ������
        if(blockCount >= 3)
        {
            // T�X�s�����Ă���
            return true;
        }
        // T�X�s�����Ă��Ȃ�
        return false;
    }
}
