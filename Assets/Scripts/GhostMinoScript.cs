using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMinoScript : MonoBehaviour
{
    // �S�[�X�g�~�m
    private GameObject _ghostMino = default;

    //�@�S�[�X�g�~�m�����Ɉړ�������
    private int _heightCount = default;

    // �v���C���[�𓮂����X�N���v�g
    private PlayerControllerScript _playerControllerScript = default;

    // �t�B�[���h���Ǘ�����X�N���v�g
    private FieldDataScript _fieldDataScript = default;

    // �Q�[���̏�Ԃ��Ǘ�����X�N���v�g
    private GameControllerScript _gameControllerScript = default;

    public GameObject GhostMino { get => _ghostMino; set => _ghostMino = value; }

   
    private void Start()
    {
        // PlayerControllerScript���擾
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        // GameControllerScript���擾
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // FieldDataScript���擾
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
    }
    /// <summary>
    /// �S�[�X�g�~�m�̋���
    /// </summary>
    public void GhostMinoController()
    {
        // �S�[�X�g�~�m�̍��W���v���C���[�~�m�Ɠ����ɂ���
        GhostMino.transform.position = _playerControllerScript.PlayerableMino.transform.position;

        // �S�[�X�g�~�m�����Ɉړ������񐔂�������
        _heightCount = default;

        // �v���C���[���ǂɏd�Ȃ��ĂȂ��@���@�S�[�X�g�~�m�����Ɉړ������񐔂��t�B�[���h�̍�����菭�Ȃ��Ƃ�
        while (!_playerControllerScript.BeforeMoving(_ghostMino) &&
            _heightCount <= _fieldDataScript.Height)
        {
            // �S�[�X�g�~�m�����ɂP�}�X�ړ�
            _ghostMino.transform.Translate(0f, -1f, 0f, Space.World);

            // �S�[�X�g�~�m�����Ɉړ������񐔂��P���₷
            _heightCount++;
            
        }
        // �S�[�X�g�~�m����ɂP�}�X�ړ�
        _ghostMino.transform.Translate(0f, 1f, 0f, Space.World);

        // �S�[�X�g�~�m�̉�]���v���C���[�~�m�Ɠ����ɂ���
        GhostMino.transform.rotation = _playerControllerScript.PlayerableMino.transform.rotation;

    }
}
