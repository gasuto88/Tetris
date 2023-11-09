/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{
    // �ʏ�̃~�m����]����X�N���v�g
    private NormalMinoRotationScript _normalMinoRotationScript = default;

    // I�~�m����]����X�N���v�g
    private IMinoRotationScript _iMinoRotationScript = default;

    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
    private void Start()
    {
        // NormalMinoRotationScript���擾
        _normalMinoRotationScript = GetComponent<NormalMinoRotationScript>();

        // IMinoRotationScript���擾
        _iMinoRotationScript = GetComponent<IMinoRotationScript>();
    }

    /// <summary>
    /// <para>SuperRotation</para>
    /// <para>�X�[�p�[���[�e�[�V����������</para>
    /// </summary>
    /// <param name="_playerMino">����ł���~�m</param>
    /// <param name="_input">���E�̓���</param>
    public void SuperRotation(GameObject _playerMino,int _input)
    {
        // �v���C���[��I�~�m��O�~�m�ȊO��������
        if (_playerMino.tag != "IMino" && _playerMino.tag != "OMino")
        {
            // �ʏ�̃~�m����]����
            _normalMinoRotationScript.NormalMinoRotation(_playerMino, _input);
        }
        // �v���C���[��I�~�m��������
        else if(_playerMino.tag == "IMino")
        {
            // I�~�m����]����
            _iMinoRotationScript.IMinoRotation(_playerMino,_input);
        }
    }
}
