/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;

public class IMinoRotationScript : MonoBehaviour
{
    // �v���C���[�𓮂����X�N���v�g
    private PlayerControllerScript _playerControllerScript = default;

    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
    private void Start()
    {
        // PlayerControllerScript���擾
        _playerControllerScript = GetComponent<PlayerControllerScript>();
    }

    /// <summary>
    /// <para>IMinoRotation</para>
    /// <para>I�~�m�̃X�[�p�[���[�e�[�V����������</para>
    /// </summary>
    /// <param name="_playerMino">����ł���~�m</param>
    /// <param name="_input">���E�̓���</param>
    public void IMinoRotation(GameObject _playerMino, int _input)
    {
        // ����ł���~�m�̍��W��ۊ�
        Vector3 _playerPositionTemp = _playerMino.transform.position;

        // ����ł���~�m�̊p�x��ۊ�
        Quaternion _playerRotationTemp = _playerMino.transform.rotation;

        // �~�m������ނ��Ă���Ƃ�
        if (_playerMino.transform.rotation.eulerAngles.z == 0)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // ��P����---------------------------------------------------------
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��Q����---------------------------------------------------------
                    case 2:
                        // �����͂����Ă�����
                        if(_input < 0)
                        {
                            _playerMino.transform.Translate(-2f, 0f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        }                        
                        break;
                    // -----------------------------------------------------------------

                    // ��R����---------------------------------------------------------
                    case 3:
                        _playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��S����---------------------------------------------------------
                    case 4:
                        _playerMino.transform.Translate(-3f, -_input, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��T����---------------------------------------------------------
                    case 5:
                        _playerMino.transform.Translate(3f, _input * 3f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // �ŏI����---------------------------------------------------------
                    case 6:
                        // �~�m�������̏�Ԃɖ߂�
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------

                }
                // �~�m���u���b�N�ɏd�Ȃ��Ă��Ȃ����
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    // �������I������
                    break;
                }
            }
        }
        // �~�m�������ނ��Ă���Ƃ�
        else if (_playerMino.transform.rotation.eulerAngles.z == 90)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // ��P����---------------------------------------------------------
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��Q����---------------------------------------------------------
                    case 2:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-2f, 0f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // ��R����---------------------------------------------------------
                    case 3:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // ��S����---------------------------------------------------------
                    case 4:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(0f, -2f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        }

                        break;
                    // -----------------------------------------------------------------

                    // ��T����---------------------------------------------------------
                    case 5:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-3f, 3f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, 3f, 0f, Space.World);
                        }

                        break;
                    // -----------------------------------------------------------------

                    //�ŏI����---------------------------------------------------------
                    case 6:
                        // �~�m�������̏�Ԃɖ߂�
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------

                }
                // �~�m���u���b�N�ɏd�Ȃ��Ă��Ȃ����
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    // �������I������
                    break;
                }
            }
        }
        // �~�m�������ނ��Ă���Ƃ�
        else if (_playerMino.transform.rotation.eulerAngles.z == 180)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // ��P����---------------------------------------------------------
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��Q����---------------------------------------------------------
                    case 2:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(2f, 0f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        }
                        
                        break;
                    // -----------------------------------------------------------------

                    // ��R����---------------------------------------------------------
                    case 3:
                        _playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 4:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, 1f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, -2f, 0f, Space.World);
                        }                       
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 5:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-3f, 1f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, 3f, 0f, Space.World);
                        }
                        break;
                    // ----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 6:
                        // �~�m�������̏�Ԃɖ߂�
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------
                }
                // �~�m���u���b�N�ɏd�Ȃ��Ă��Ȃ����
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    // �������I������
                    break;
                }
            }
        }
        // �~�m���E���ނ��Ă���Ƃ�
        else if (_playerMino.transform.rotation.eulerAngles.z == 270)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // ��P����---------------------------------------------------------
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 2:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(2f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 3:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 4:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-3f, 2f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, 1f, 0f, Space.World);
                        }
                        
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 5:
                        // �����͂����Ă�����
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, -3f, 0f, Space.World);
                        }
                        // �E���͂����Ă�����
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-3f, -3f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // ��P����---------------------------------------------------------
                    case 6:
                        // �~�m�������̏�Ԃɖ߂�
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------

                }
                // �~�m���u���b�N�ɏd�Ȃ��Ă��Ȃ����
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    // �������I������
                    break;
                }
            }
        }
    }
}
