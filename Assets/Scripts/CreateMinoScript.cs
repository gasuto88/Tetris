using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour ,ICreateMino
{
    private IRandomSelectMino _iRandomSelectMino = default;

    private PlayerControllerScript _playerControllerScript = default;

    private Transform _minoSpawnTransform = default;

    private Transform _oIMinoSpawnTransform = default;

    private void Start()
    {
        _iRandomSelectMino = GameObject.Find("MinoController").GetComponent<RandomSelectMinoScript>();

        _playerControllerScript = GameObject.Find("GameController").GetComponent<PlayerControllerScript>();

        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;

        _oIMinoSpawnTransform = GameObject.Find("O_IMinoSpawnPosition").transform;
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

        //// ���X�g�̐擪�̃~�m�𐶐�����
        //_playerControllerScript.PlayerMino = Instantiate(
        //    _iRandomSelectMino.MinoList[0],
        //    tempTransform.position,
        //    tempTransform.rotation
        //     );

        // ���X�g�̒����琶�����ꂽ�~�m���폜����
        _iRandomSelectMino.MinoList.RemoveAt(0);
    }
}
