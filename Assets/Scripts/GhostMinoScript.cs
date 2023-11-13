/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
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
    private FieldManagerScript _fieldDataScript = default;

    // �Q�[���̏�Ԃ��Ǘ�����X�N���v�g
    private GameControllerScript _gameControllerScript = default;

    // �v���C���[�̈ړ�����
    private readonly Vector3 _vectorRight = Vector3.right;
    private readonly Vector3 _vectorUp = Vector3.up;
    private readonly Vector3 _vectorForward = Vector3.forward;

    // �S�[�X�g�~�m
    public GameObject GhostMino { get => _ghostMino; set => _ghostMino = value; }

   /// <summary>
   /// �X�V�O����
   /// </summary>
    private void Start()
    {
        // PlayerControllerScript���擾
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        // GameControllerScript���擾
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // FieldDataScript���擾
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }
    /// <summary>
    /// GhostMinoMove
    /// �S�[�X�g�~�m�𓮂���
    /// </summary>
    public void GhostMinoMove()
    {
        // �v���C���[�̍��W��ݒ�
        GhostMino.transform.position = _playerControllerScript.PlayerableMino.transform.position;

        // ���Ɉړ������񐔂�������
        _heightCount = default;

        // �v���C���[���ǂɏd�Ȃ��ĂȂ��@���@���Ɉړ������񐔂��t�B�[���h�̍�����菭�Ȃ��Ƃ�
        while (!_playerControllerScript.CheckCollision(_ghostMino) &&
            _heightCount <= _fieldDataScript.Height)
        {
            // �S�[�X�g�~�m�����ɂP�}�X�ړ�
            _ghostMino.transform.Translate(-_vectorUp, Space.World);

            //���Ɉړ������񐔂����Z
            _heightCount++;
            
        }
        // �S�[�X�g�~�m����ɂP�}�X�ړ�
        _ghostMino.transform.Translate(_vectorUp, Space.World);

        // �v���C���[�̊p�x��ݒ�
        GhostMino.transform.rotation = _playerControllerScript.PlayerableMino.transform.rotation;

    }
}
