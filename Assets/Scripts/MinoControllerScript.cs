/*----------------------------------------------------------
MinoControllerScript.cs

 �X�V���@11��9��

 ����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;
/// <summary>
/// // �~�m���Ǘ�����
/// </summary>
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
    private int _holdCoolTime = default;

    // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����X�N���v�g
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // �Q�[���̏�Ԃ��Ǘ�����X�N���v�g
    private GameControllerScript _gameControllerScript = default;

    // �S�[�X�g�~�m�𓮂����X�N���v�g
    private GhostMinoScript _ghostMinoScript = default;

    // �~�m��ۊǂ�����W
    private Transform _minoStorageTransform = default;

    private AudioSource _audioSource = default;

    #endregion

    /// <summary>
    /// �X�V�O����
    /// </summary>
    private void Start()
    {
        // GameControllerScript���擾
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // RandomSelectMinoScript���擾
        _randomSelectMinoScript = GetComponent<RandomSelectMinoScript>();

        // Next�̍��W���擾
        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;
        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;
        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        // Next�̍��W��z��ɓ����
        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        // GhostMinoScript���擾
        _ghostMinoScript = GetComponent<GhostMinoScript>();

        // Hold�̍��W���擾
        _holdTransform = GameObject.Find("HoldPosition").transform;

        // �~�m��ۊǂ�����W���擾
        _minoStorageTransform = GameObject.Find("MinoStoragePosition").transform;

        // AudioSource���擾
        _audioSource = GetComponent<AudioSource>();

    }
    /// <summary>
    /// �X�V����
    /// </summary>
    private void Update()
    {
        AddMino();       
    }
    /// <summary>
    /// AddMino
    /// ���X�g�̃~�m�̌��𑝂₷
    /// </summary>
    private void AddMino()
    {    
        // ���X�g�̃~�m��7�ȉ��ɂȂ�����
        if(_randomSelectMinoScript.MinoList.Count <= 7)
        {
            // �����_���̃~�m�e�[�u�����쐬����
            _randomSelectMinoScript.RandomSelectMino();
        }
    }
    /// <summary>
    /// NextDisplay
    /// NEXT�~�m�̕\��
    /// </summary>
    public void NextDisplay()
    {
        // 3��Next�Ƀ~�m���ړ�������
        for (int i = 0; i < 3; i++)
        {
            _randomSelectMinoScript.MinoList[i].transform.position = _nextPositions[i].transform.position;
        }
    }
    /// <summary>
    /// HoldController
    /// Hold�̒��g���o�����ꂷ�鏈��
    /// </summary>
    /// <param name="playerableMino">����ł���~�m</param>
    public void HoldController(GameObject playerableMino,AudioClip holdSound)
    {
        // �N�[���^�C����0��������
        if (_holdCoolTime <= 0)
        {
            // Hold�����Đ�
            _audioSource.PlayOneShot(holdSound);

            // �N�[���^�C����ݒ� 
            _holdCoolTime = 2;

            // Hold�Ƀ~�m�������Ă�����
            if (_holdObject != null)
            {
                // Hold�ɓ����Ă�~�m�����X�g�̐擪�ɒǉ�
                _randomSelectMinoScript.MinoList.Insert(0, _holdObject);
                _randomSelectMinoScript.GhostList.Insert(0, _holdGhostObject);
            }

            // ����ł���~�m��ݒ�
            _holdObject = playerableMino;

            // �S�[�X�g�~�m��ݒ�
            _holdGhostObject = _ghostMinoScript.GhostMino;

            // Hold�ɓ����Ă�~�m��Hold�̍��W�Ɉړ�����
            _holdObject.transform.position = _holdTransform.position;         

            // �S�[�X�g�~�m��ۊǂ�����W�ɐݒ肷��
            _holdGhostObject.transform.position = _minoStorageTransform.position;

            // Hold�̃~�m�̊p�x��������
            _holdObject.transform.rotation = default;
            _holdGhostObject.transform.rotation = default;

            // �Q�[����Ԃ��~�m�𐶐����Ă����ԂɕύX����
            _gameControllerScript.GameType = GameControllerScript.GameState.MINO_CREATE;
        }

        

    }
    /// <summary>
    /// DownCoolTime
    /// Hold�̃N�[���^�C�������炷
    /// </summary>
    public void DownCoolTime()
    {
        _holdCoolTime--;
    }
}
