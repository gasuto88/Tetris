using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*----------------------------------------------------------

�X�V���@11��8���@18��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
public class PlayerControllerScript : MonoBehaviour
{
    #region �t�B�[���h�ϐ�

    // ���E����
    private float _horizontalInput = default;

    // �㉺����
    private float _verticalInput = default;

    // ����ł���~�m
    private GameObject _playerableMino = default;

    private readonly Vector3 _vectorRight = Vector3.right;

    private readonly Vector3 _vectorUp = Vector3.up;

    private readonly Vector3 _vectorForward = Vector3.forward;

    // �v���C���[���n�ʂɒ������Ƃ��̍��W
    private Vector3 _groundPosition = default;

    // ���͂���Ă���̌o�ߎ���
    private float _inputTime = 0f;

    [SerializeField, Header("���͎��̃N�[���^�C��")]
    private float _inputCoolTime = 0.1f;

    // ���E���́i��]�j
    private const int INPUT_LEFT = -1;
    private const int INPUT_RIGHT = 1;

    // �v���C���[���������Ă���̌o�ߎ���
    private float _fallTime = 0f;

    [SerializeField,Header("�~�m�������鎞��")]
    private float _fallCoolTime = 0.7f;

    // �v���C���[�����n���Ă���̌o�ߎ���
    private float _groundTime = 0f;

    // �v���C���[���n�ʂɂ��邩�ǂ���
    private bool isGround = false;

    [SerializeField,Header("�~�m�����ʂ܂ł̎���")]
    private float _deathTime = 0.3f;

    [SerializeField, Header("�~�m�����ʂ܂ł̎��ԁi�ő�j")]
    private float _maxDeathTime = 1.2f;

    [SerializeField,Header("�~�m�����ʂ܂ł̎��ԁi�ŏ��j")]
    private float _minDeathTime = 0.3f;

    [SerializeField,Header("�~�m�̎��ʂ܂ł̒ǉ�����")]
    private float _addDeathTime = 0.15f;

    // ���ʉ�-------------------------------------

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

    // -------------------------------------------

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
    // ����ł���~�m
    public GameObject PlayerableMino { get => _playerableMino; set => _playerableMino = value; }

    // ���n����
    public bool IsGround { get => isGround; set => isGround = value; }

    #endregion

    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
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
    /// <para>PlayerController</para>
    /// <para>�v���C���[�̋����𐧌䂷��</para>
    /// </summary>
    public void PlayerController()
    {
        //���͎擾
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        // �����͂��ꂽ��
        if(_horizontalInput < 0)
        {
            // ���ɂP�}�X�ړ�����
            HorizontalMove(INPUT_LEFT);
        }
        // �E���͂��ꂽ��
        else if(0 < _horizontalInput)
        {
            // �E�ɂP�}�X�ړ�����
            HorizontalMove(INPUT_RIGHT);
        }
        // ����͂��ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �~�m��n�ʂ܂ŗ��Ƃ�
            HardDrop();
        }
        // �����͂��ꂽ�Ƃ�
        if (_verticalInput < 0)
        {
            // ���ɂP�}�X�ړ�����
            SoftDrop();
        }

        // �v���C���[�̎�������
        if ((Time.time - _fallTime) > _fallCoolTime)
        {
            // ���ɂP�}�X�ړ�
            PlayerableMino.transform.Translate(0f, -1f, 0f,Space.World);

            _fallTime = Time.time;

            // �v���C���[���ǂɏd�Ȃ�����
            if (BeforeMoving(PlayerableMino))
            {
                // �P�}�X�߂�
                PlayerableMino.transform.Translate(0f, 1f, 0f, Space.World);

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
                _groundPosition.y = _playerableMino.transform.position.y;
            }

            //�^�C�}�[���Z
            _groundTime += Time.deltaTime;

            _deathTime = Mathf.Clamp(_deathTime,_minDeathTime,_maxDeathTime);

            // �v���C���[�̌��݂̍��W�����n�������W�����P�ȏ�Ⴉ������
            if (_groundPosition.y - _playerableMino.transform.position.y >= 1)
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
                UpdateMinoToField();

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
            _superRotationScript.SuperRotation(PlayerableMino, INPUT_LEFT);

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
            _superRotationScript.SuperRotation(PlayerableMino, INPUT_RIGHT);

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
            _minoControllerScript.HoldController(PlayerableMino);          
        }
    }
    /// <summary>
    /// <para>HorizontalMove</para>
    /// <para>���E�Ɉړ�����</para>
    /// </summary>
    /// <param name="input">���E�̓���</param>
    private void HorizontalMove(int input)
    {
        // ���Ԃ��o�߂�����
        if ((Time.time - _inputTime) > _inputCoolTime)
        {
            // �ړ������Đ�
            _audioSource.PlayOneShot(_moveSound);

            // �P�}�X�ړ�
            PlayerableMino.transform.Translate(_vectorRight * input, Space.World);

            // ���݂̎��Ԃ�ݒ�
            _inputTime = Time.time;

            // �ǂƏd�Ȃ�����
            if (BeforeMoving(PlayerableMino))
            {
                // �P�}�X�߂�
                PlayerableMino.transform.Translate(_vectorRight * -input, Space.World);
            }
        }     
    }
    /// <summary>
    /// <para>HardDrop</para>
    /// <para>�~�m��n�ʂ܂ŗ��Ƃ�</para>
    /// </summary>
    private void HardDrop()
    {
        // ���n��
        _audioSource.PlayOneShot(_groundSound);

        // ���n����܂ŉ��ɗ��Ƃ�
        GroundFall();

        // �u�����~�m���t�B�[���h�ɔ��f�����鏈��
        UpdateMinoToField();

        // �e�I�u�W�F�i��]��)�Ǝq�I�u�W�F�i�~�m�j�̐e�q�֌W����������
        CutParentMino();

        // �Q�[���̏�Ԃ�u�����~�m�����������ɐ؂�ւ���
        _gameControllerScript.GameType = GameControllerScript.GameState.MINO_ERASE;
    }
    /// <summary>
    /// <para>SoftDrop</para>
    /// <para>���ɂP�}�X�ړ�����</para>
    /// </summary>
    private void SoftDrop()
    {
        // ���Ԃ��o�߂�����
        if ((Time.time - _inputTime) > _inputCoolTime)
        {
            // ���ɂP�}�X�ړ�
            PlayerableMino.transform.Translate(0f, -1f, 0f, Space.World);

            // ���݂̎��Ԃ�ݒ�
            _inputTime = Time.time;

            // �ǂɏd�Ȃ�����
            if (BeforeMoving(PlayerableMino))
            {
                // ��ɂP�}�X�ړ�
                PlayerableMino.transform.Translate(0f, 1f, 0f, Space.World);

                // ���n�����ݒ�
                IsGround = true;
            }
        }
    }

    /// <summary>
    /// �v���C���[���ǂɏd�Ȃ��Ă��邩�𒲂ׂ鏈��
    /// </summary>
    /// <param name="mino">���ׂ�~�m</param>
    /// <returns>�v���C���[���ǂɏd�Ȃ��Ă��邩</returns>
    public bool BeforeMoving(GameObject mino)
    {
        // �~�m�̂��ׂẴu���b�N�𒲂ׂ�
        foreach(Transform children in mino.GetComponentInChildren<Transform>())
        {
            // ������
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);
         
            // �~�m���X�e�[�W�͈̔͊O��������
            if (posX <= -1 || 10 <= posX || posY <= -20)
            {
                return true;
            }
            // �u���Ă�~�m�ɏd�Ȃ�����
            if (posY < 0 && _fieldDataScript.FieldData[posX, -posY] != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// �u�����~�m���t�B�[���h�ɔ��f�����鏈��
    /// </summary>
    private void UpdateMinoToField()
    {
        // �~�m�̂��ׂẴu���b�N�𒲂ׂ�
        foreach (Transform _children in PlayerableMino.GetComponentInChildren<Transform>())
        {
            // ������
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
            
            // �t�B�[���h�̏�Ƀ~�m��u������ 
            if(_posY >= 0)
            {
                // �Q�[���I�[�o�[
                _gameControllerScript.GameOverScene();
                return;
            }
            // �u�����~�m���t�B�[���h�f�[�^�ɔ��f������
            _fieldDataScript.FieldData[_posX, -_posY] = _children.gameObject;

            // �t�B�[���h�f�[�^�ɒu�����~�m�̏��𑗂�
            _fieldDataScript.SetPlayerPosition(PlayerableMino);
        }
    }
    /// <summary>
    /// �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣������
    /// </summary>
    private void CutParentMino()
    {
        // �e�ɂ��Ă�q�I�u�W�F�N�g��؂藣��
        PlayerableMino.transform.DetachChildren();
    }
    /// <summary>
    /// �~�m�𒅒n����܂ŗ��Ƃ�����
    /// </summary>
    private void GroundFall()
    {
        // �v���C���[�����n����܂�
        while (!BeforeMoving(PlayerableMino))
        {
            // ���ɂP�}�X�ړ�
            PlayerableMino.transform.Translate(0f, -1f, 0f, Space.World);
        }
        // �P�}�X�߂�
        PlayerableMino.transform.Translate(0f, 1f, 0f, Space.World);
    }
}
