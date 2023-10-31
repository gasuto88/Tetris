using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour ,ICreateMino
{
    private IRandomSelectMino _iRandomSelectMino = default;

    private PlayerControllerScript _playerControllerScript = default;

    private Transform _minoSpawnTransform = default;

    private Transform _oIMinoSpawnTransform = default;

    private GhostMinoScript _ghostMinoScript = default;

    private SpriteRenderer _spriteRenderer = default;

    private Color _ghostColor = new Color(1.0f,1.0f,1.0f,0.3f);

    private void Start()
    {
        _iRandomSelectMino = GetComponent<RandomSelectMinoScript>();

        _playerControllerScript = GetComponent<PlayerControllerScript>();

        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;

        _oIMinoSpawnTransform = GameObject.Find("O_IMinoSpawnPosition").transform;

        _ghostMinoScript = GetComponent<GhostMinoScript>();

        
    }
    
    /// <summary>
    /// Next�ɂ���~�m�𐶐�����
    /// </summary>
    public void NextMinoInstance()
    {   
        // OMino��IMino����Ȃ�������
        if (_iRandomSelectMino.MinoList[0].tag != "OMino" && _iRandomSelectMino.MinoList[0].tag != "IMino")
        {
            _iRandomSelectMino.MinoList[0].transform.position = _minoSpawnTransform.position;
        }
        // OMino��IMIno�̂Ƃ�
        else
        {
            _iRandomSelectMino.MinoList[0].transform.position = _oIMinoSpawnTransform.position;
        }

        _playerControllerScript.PlayerMino = _iRandomSelectMino.MinoList[0];

        _ghostMinoScript.GhostMino = _iRandomSelectMino.GhostList[0];

        _ghostMinoScript.GhostMino.transform.position =
            _playerControllerScript.PlayerMino.transform.position;

        // �S�[�X�g�~�m�̓����x���グ�Ă���
        foreach(Transform _children in _ghostMinoScript.GhostMino.GetComponentInChildren<Transform>())
        {
            _children.GetComponent<SpriteRenderer>().color = _ghostColor;
        }
        // ���X�g�̐擪�̃~�m�𐶐�����
        //_playerControllerScript.PlayerMino = Instantiate(
        //    _iRandomSelectMino.MinoList[0],
        //    tempTransform.position,
        //    tempTransform.rotation
        //     );

        // ���X�g�̒����琶�����ꂽ�~�m���폜����
        _iRandomSelectMino.MinoList.RemoveAt(0);
        _iRandomSelectMino.GhostList.RemoveAt(0);
    }
}
