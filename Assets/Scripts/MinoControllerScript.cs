using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinoControllerScript : MonoBehaviour
{
    #region �t�B�[���h�ϐ�

    // Next�̈�Ԗڂ̍��W
    private Transform _nextPositionOne = default;
    // Next�̓�Ԗڂ̍��W
    private Transform _nextPositionTwo = default;
    // Next�̎O�Ԗڂ̍��W
    private Transform _nextPositionThree = default;

    // Next�̍��W������z��
    private Transform[] _nextPositions = new Transform[4];

    // Hold�̃Q�[���I�u�W�F�N�g
    private GameObject _holdObject = default;

    // Hold�̍��W
    private Transform _holdTransform = default;

    // Hold�̃Q�[���I�u�W�F�N�g�i�S�[�X�g�~�m�p�j
    private GameObject _holdGhostObject = default;

    // Hold��������̃N�[���^�C��
    private int _holdCount = default;

    // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����X�N���v�g
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // �Q�[���̏�Ԃ��Ǘ�����X�N���v�g
    private GameControllerScript _gameControllerScript = default;

    // �S�[�X�g�~�m�𓮂����X�N���v�g
    private GhostMinoScript _ghostMinoScript = default;

    // �~�m��ۊǂ�����W
    private Transform _minoStorageTransform = default;

    #endregion

    private void Start()
    {
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        _randomSelectMinoScript = GetComponent<RandomSelectMinoScript>();

        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;

        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;

        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        _ghostMinoScript = GetComponent<GhostMinoScript>();

        _holdTransform = GameObject.Find("HoldPosition").transform;

        _minoStorageTransform = GameObject.Find("MinoStoragePosition").transform;



    }
    private void Update()
    {
        AddMino();       
    }
    /// <summary>
    /// �~�m�̌����Ǘ�����
    /// </summary>
    private void AddMino()
    {
        
        if(_randomSelectMinoScript.MinoList.Count <= 7)
        {
            _randomSelectMinoScript.RandomSelectMino();
        }
    }
    /// <summary>
    /// NEXT�~�m�̕\��
    /// </summary>
    public void NextDisplay()
    {
        for (int i = 0; i < 3; i++)
        {
            _randomSelectMinoScript.MinoList[i].transform.position = _nextPositions[i].transform.position;
        }
    }
    /// <summary>
    /// Hold�̒��g���o�����ꂷ�鏈��
    /// </summary>
    /// <param name="_holdMino"></param>
    /// <param name="_minoCreateMethod"></param>
    public void HoldController(GameObject _holdMino)
    {

        if (_holdCount <= 0)
        {
            _holdCount = 2;

            if (_holdObject != null)
            {
                // Hold�ɓ����Ă�~�m�����X�g�̐擪�ɒǉ�
                _randomSelectMinoScript.MinoList.Insert(0, _holdObject);
                _randomSelectMinoScript.GhostList.Insert(0, _holdGhostObject);
            }
            _holdObject = _holdMino;

            _holdGhostObject = _ghostMinoScript.GhostMino;

            _holdObject.transform.position = _holdTransform.position;         

            _holdGhostObject.transform.position = _minoStorageTransform.position;

            _holdObject.transform.rotation = default;
            _holdGhostObject.transform.rotation = default;

            _gameControllerScript.GameType = GameControllerScript.GameState.MINO_CREATE;
        }

        

    }

    public void HoldCount()
    {
        _holdCount--;
    }
}
