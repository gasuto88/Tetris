using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    #region �t�B�[���h�ϐ�

    private float _horizontalInput = default;
    private float _verticalInput = default;

    // ����ł���~�m
    private GameObject _playerMino = default;

    // �v���C���[���n�ʂɒ������Ƃ��̍��W
    private Vector3 _playerGroundPosition = default;

    // ���͂���Ă���̌o�ߎ���
    private float _inputTime = default;

    [SerializeField, Header("���͎��̃N�[���^�C��")]
    private float _inputCoolTime = 0.1f;

    // ���E���́i��]�j
    private const int LEFT_INPUT = -1;
    private const int RIGHT_INPUT = 1;

    // �v���C���[���������Ă���̌o�ߎ���
    private float _fallTime = default;

    [SerializeField,Header("�~�m�������鎞��")]
    private float _fallCoolTime = 0.7f;

    // �v���C���[�����n���Ă���̌o�ߎ���
    private float _groundTime = default;

    // �v���C���[���n�ʂɂ��邩�ǂ���
    private bool isGround = default;

    [SerializeField,Header("�~�m�����ʂ܂ł̎���")]
    private float _deathTime = 0.3f;

    [SerializeField, Header("�~�m�����ʂ܂ł̎��ԁi�ő�j")]
    private float _maxDeathTime = 1.2f;

    [SerializeField,Header("�~�m�����ʂ܂ł̎��ԁi�ŏ��j")]
    private float _minDeathTime = 0.3f;

    [SerializeField,Header("�~�m�̎��ʂ܂ł̒ǉ�����")]
    private float _addDeathTime = 0.15f;

    //------------------���ʉ�--------------------

    [Space(20),Header("���ʉ�")]

    private AudioSource _audioSource = default;

    [SerializeField,Header("�~�m�̉�]��")]
    private AudioClip _rotationSound = default;

    [SerializeField, Header("�~�m�̈ړ���")]
    private AudioClip _moveSound = default;

    [SerializeField, Header("�~�m�̃z�[���h��")]
    private AudioClip _holdSound = default;

    [SerializeField, Header("�~�m�̒��n��")]
    private AudioClip _groundSound = default;

    //---------------------------------------------

    // �t�B�[���h���Ǘ�����X�N���v�g
    private FieldDataScript _fieldDataScript = default;

    // �~�m���Ǘ�����X�N���v�g
    private MinoControllerScript _minoControllerScript = default;

    // �Q�[���̏�Ԃ��Ǘ�����X�N���v�g
    private GameControllerScript _gameControllerScript = default;

    // �X�[�p�[���[�e�[�V���������Ă����X�N���v�g
    private SuperRotationScript _superRotationScript = default;

    #endregion

    #region �v���p�e�B

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

    public bool IsGround { get => isGround; set => isGround = value; }

    #endregion

    private void Start()
    {
        // FieldDataScript���擾
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();

        // MinoControllerScript���擾
        _minoControllerScript = GetComponent<MinoControllerScript>();

        // GameControllerScript���擾
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // SuperRotationScript���擾
        _superRotationScript = GetComponent<SuperRotationScript>();

        // AudioSource���擾
        _audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// �v���C���[�̋���
    /// </summary>
    public void PlayerController()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");    
        
        // �E���͂��ꂽ�Ƃ�
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // �v���C���[�̈ړ������Đ�
            _audioSource.PlayOneShot(_moveSound);

            // �E�ɂP�}�X�ړ�
            PlayerMino.transform.Translate(1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            // �v���C���[���ǂɂ߂荞�񂾂�
            if (BeforeMoving(PlayerMino))
            {
                // �P�}�X�߂�
                PlayerMino.transform.Translate(-1f, 0f, 0f, Space.World);
            }
        }
        // �����͂��ꂽ�Ƃ�
        else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // �v���C���[�̈ړ������Đ�
            _audioSource.PlayOneShot(_moveSound);

            // ���ɂP�}�X�ړ�
            PlayerMino.transform.Translate(-1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            // �v���C���[���ǂɂ߂荞�񂾂�
            if (BeforeMoving(PlayerMino))
            {
                // �P�}�X�߂�
                PlayerMino.transform.Translate(1f, 0f, 0f, Space.World);
            }
        }
        // ����͂��ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �v���C���[�̒��n��
            _audioSource.PlayOneShot(_groundSound);

            // �v���C���[�𒅒n����܂ŉ��ɗ��Ƃ�
            GroundFall();

            // �u�����~�m���t�B�[���h�f�[�^�ɔ��f�����鏈��
            AddMinoToField();         

            // �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣��
            CutParentMino();

            // �Q�[���̏�Ԃ�u�����~�m�����������ɐ؂�ւ���
            _gameControllerScript.GameType = GameControllerScript.GameState.MINO_ERASE;

            return;
        }
        // �����͂��ꂽ�Ƃ�
        else if (_verticalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // ���ɂP�}�X�ړ�
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _inputTime = Time.time;

            // �v���C���[���ǂɂ߂荞�񂾂�
            if (BeforeMoving(PlayerMino))
            {
                // ��ɂP�}�X�ړ�
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

                // �v���C���[���n�ʂɒ��n����
                IsGround = true;
            }          
        }

        // �v���C���[�̎�������
        if ((Time.time - _fallTime) > _fallCoolTime)
        {
            // ���ɂP�}�X�ړ�
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _fallTime = Time.time;

            // �v���C���[���ǂɂ߂荞�񂾂�
            if (BeforeMoving(PlayerMino))
            {
                // �P�}�X�߂�
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

                // �v���C���[���n�ʂɒ��n����
                IsGround = true;
            }         
        }

        // �v���C���[���n�ʂɒ��n������
        if (IsGround)
        {
            if (_groundTime <= 0)
            {
                // �v���C���[���n�ʂɒ��n�����Ƃ��̍��W����
                _playerGroundPosition.y = _playerMino.transform.position.y;
            }

            _groundTime += Time.deltaTime;

            _deathTime = Mathf.Clamp(_deathTime,_minDeathTime,_maxDeathTime);

            // �v���C���[�̌��݂̍��W�����n�������W�����P�ȏ�Ⴉ������
            if (_playerGroundPosition.y - _playerMino.transform.position.y >= 1)
            {
                // �^�C�}�[���Z�b�g
                _groundTime = 0;

                IsGround = false;
            }

            // ��莞�Ԓn�ʂɒ��n���Ă�����
            if (_groundTime > _deathTime)
            {
                // �v���C���[�̒��n��
                _audioSource.PlayOneShot(_groundSound);

                // �v���C���[�𒅒n����܂ŉ��ɗ��Ƃ�
                GroundFall();

                // �u�����~�m���t�B�[���h�f�[�^�ɔ��f�����鏈��
                AddMinoToField();

                // �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣��
                CutParentMino();

                // �Q�[���̏�Ԃ�u�����~�m�����������ɐ؂�ւ���
                _gameControllerScript.GameType = GameControllerScript.GameState.MINO_ERASE;

                return;
            }
        }
        else if (!IsGround)
        {
            _groundTime = 0;
        }

        // ����]
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // �v���C���[�̉�]��
            _audioSource.PlayOneShot(_rotationSound);

            // �X�[�p�[���[�e�[�V���������Ă���鏈���i�v���C���[�~�m�A�����́j
            _superRotationScript.SuperRotation(PlayerMino, LEFT_INPUT);

            // �v���C���[�����n���Ă�����
            if (isGround)
            {
                // �v���C���[�����ʂ܂ł̎��Ԃ�ǉ�
                _deathTime += _addDeathTime;
            }
        }
        // �E��]
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // �v���C���[�̉�]��
            _audioSource.PlayOneShot(_rotationSound);

            // �X�[�p�[���[�e�[�V���������Ă���鏈���i�v���C���[�~�m�A�E���́j
            _superRotationScript.SuperRotation(PlayerMino, RIGHT_INPUT);

            // �v���C���[�����n���Ă�����
            if (isGround)
            {
                // �v���C���[�����ʂ܂ł̎��Ԃ�ǉ�
                _deathTime += _addDeathTime;
            }
        }
        
        // �z�[���h
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // �v���C���[�̃z�[���h��
            _audioSource.PlayOneShot(_holdSound);

            // �z�[���h�̒��g���o�����ꂷ�鏈��
            _minoControllerScript.HoldController(PlayerMino);          
        }
    }
    /// <summary>
    /// �v���C���[���ǂɂ߂荞��ł邩�𒲂ׂ鏈��
    /// </summary>
    /// <param name="_mino">���ׂ�~�m</param>
    /// <returns>�v���C���[���ǂɂ߂荞��ł邩�ǂ���</returns>
    public bool BeforeMoving(GameObject _mino)
    {
        // �~�m�̂��ׂẴu���b�N�𒲂ׂ�
        foreach(Transform _children in _mino.GetComponentInChildren<Transform>())
        {
            // ������
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
         
            // �~�m���X�e�[�W�͈̔͊O��������
            if (_posX <= -1 || 10 <= _posX || _posY <= -20)
            {
                return true;
            }
            // �u���Ă�~�m�ɂ߂荞�񂾂�
            if (_posY < 0 && _fieldDataScript.FieldData[_posX, -_posY] != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// �u�����~�m���t�B�[���h�f�[�^�ɔ��f�����鏈��
    /// </summary>
    private void AddMinoToField()
    {
        // �~�m�̂��ׂẴu���b�N�𒲂ׂ�
        foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            // ������
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
            
            // �t�B�[���h�̏�Ƀ~�m��u������ 
            if(_posY >= 0)
            {
                // �Q�[���I�[�o�[
                _gameControllerScript.GameOver();
                return;
            }
            // �u�����~�m���t�B�[���h�f�[�^�ɔ��f������
            _fieldDataScript.FieldData[_posX, -_posY] = _children.gameObject;

            // �t�B�[���h�f�[�^�ɒu�����~�m�̏��𑗂�
            _fieldDataScript.SetPlayerPosition(PlayerMino);
        }
    }
    /// <summary>
    /// �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣������
    /// </summary>
    private void CutParentMino()
    {
        // �e�ɂ��Ă�q�I�u�W�F�N�g��؂藣��
        PlayerMino.transform.DetachChildren();
    }
    /// <summary>
    /// �~�m�𒅒n����܂ŗ��Ƃ�����
    /// </summary>
    private void GroundFall()
    {
        // �v���C���[�����n����܂�
        while (!BeforeMoving(PlayerMino))
        {
            // ���ɂP�}�X�ړ�
            PlayerMino.transform.Translate(0f, -1f, 0f, Space.World);
        }
        // �P�}�X�߂�
        PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);
    }
}
