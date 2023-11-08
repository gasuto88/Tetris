using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour
{
    // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����X�N���v�g
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // �v���C���[�𓮂����X�N���v�g
    private PlayerControllerScript _playerControllerScript = default;

    // �S�[�X�g�~�m�𓮂����X�N���v�g
    private GhostMinoScript _ghostMinoScript = default;

    // �~�m�𐶐�������W
    private Transform _minoSpawnTransform = default;

    // O��I�~�m�𐶐�������W
    private Transform _oIMinoSpawnTransform = default;

    [SerializeField, Header("�S�[�X�g�~�m�̐F"),Range(0,1)]
    private float _alpha = 0;

    // �S�[�X�g�~�m�̐F
    private Color _ghostColor = default;

    private void Start()
    {
        // �S�[�X�g�̐F��ݒ�
        _ghostColor = new Color(1.0f, 1.0f, 1.0f, _alpha);

        // RandomSelectMinoScript���擾
        _randomSelectMinoScript = GetComponent<RandomSelectMinoScript>();

        // PlayerControllerScript���擾
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        // �~�m�𐶐�������W���擾
        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;

        // O��I�~�m�𐶐�������W���擾
        _oIMinoSpawnTransform = GameObject.Find("O_IMinoSpawnPosition").transform;

        // GhostMinoScript���擾
        _ghostMinoScript = GetComponent<GhostMinoScript>();    
    }
    
    /// <summary>
    /// Next�̐擪�̃~�m�����o��
    /// </summary>
    public void NextMinoTakeOut()
    {
        // �擪�̃~�m��O��I�~�m����Ȃ�������
        if (_randomSelectMinoScript.MinoList[0].tag != "OMino" && _randomSelectMinoScript.MinoList[0].tag != "IMino")
        {
            // �擪�̃~�m�𐶐����W�Ɉړ�����
            _randomSelectMinoScript.MinoList[0].transform.position = _minoSpawnTransform.position;
        }
        // �擪�̃~�m��O��I�~�m�̂Ƃ�
        else
        {
            // �擪�̃~�m�𐶐����W�Ɉړ�����
            _randomSelectMinoScript.MinoList[0].transform.position = _oIMinoSpawnTransform.position;
        }

        // �擪�̃~�m���v���C���[�~�m�ɐݒ肷�� 
        _playerControllerScript.PlayerableMino = _randomSelectMinoScript.MinoList[0];

        // �擪�̃~�m���S�[�X�g�~�m�ɐݒ肷��
        _ghostMinoScript.GhostMino = _randomSelectMinoScript.GhostList[0];

        // �S�[�X�g�~�m�Ƒ���~�m
        _ghostMinoScript.GhostMino.transform.position =
            _playerControllerScript.PlayerableMino.transform.position;

        // �S�[�X�g�~�m�̓����x�������Ĕ������Ă���
        foreach(Transform _children in _ghostMinoScript.GhostMino.GetComponentInChildren<Transform>())
        {
            _children.GetComponent<SpriteRenderer>().color = _ghostColor;
        }

        // ���X�g�̐擪�̃~�m���폜����
        _randomSelectMinoScript.MinoList.RemoveAt(0);
        _randomSelectMinoScript.GhostList.RemoveAt(0);
    }
}
