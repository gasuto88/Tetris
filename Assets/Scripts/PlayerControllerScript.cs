/*----------------------------------------------------------
�@PlayerControllerScript.cs

�@�X�V���@11��10��

�@����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;
/// <summary>
/// �v���C���[�𓮂���
/// </summary>
public class PlayerControllerScript : MonoBehaviour
{
    #region �t�B�[���h�ϐ�

    // ���E����
    private float _horizontalInput = default;
    // �㉺����
    private float _verticalInput = default;

    // ����ł���~�m
    private GameObject _playerableMino = default;    

    // ���E����
    private const int INPUT_LEFT = -1;
    private const int INPUT_RIGHT = 1;

    // ���͉񐔂̏��
    private const int INPUT_COUNT_MAX = 15;

    // �~�m�̐ݒu����
    private const float SET_UP_TIME = 0.5f;

    // �t�B�[���h�̏c���Ɖ���-------------------
    private const int MIN_FIELD_WIDTH = -1;
    private const int MAX_FIELD_WIDTH = 10;
    private const int MAX_FIELD_HEIGHT = -20;
    //----------------------------------------

    // ���͂��ꂽ��
    private int _inputCount = 0;

    // �v���C���[���n�ʂɒ������Ƃ��̍��W
    private Vector3 _groundPosition = default;

    // �v���C���[�̉ߋ��̍��W
    private Vector3 _oldPosition = default;

    // �v���C���[�̉ߋ��̉�]
    private Quaternion _oldRotation = default;

    // ���͂���Ă���̌o�ߎ���
    private float _inputTime = 0f;

    [SerializeField, Header("���͎��̃N�[���^�C��")]
    private float _inputCoolTime = 0.1f;  

    // �v���C���[���������Ă���̌o�ߎ���
    private float _fallTime = 0f;

    [SerializeField,Header("�~�m�������鎞��")]
    private float _fallCoolTime = 0.7f;

    // �v���C���[�����n���Ă���̌o�ߎ���
    private float _groundTime = 0f;

    // �v���C���[���n�ʂɂ��邩�ǂ���
    private bool isGround = false;

    // ���ʉ�-------------------------------------

    [Space(20),Header("���ʉ�")]

    private AudioSource _audioSource = default;

    [SerializeField,Header("�~�m�̉�]��")]
    private AudioClip _rotationSound = default;

    [SerializeField, Header("�~�m�̈ړ���")]
    private AudioClip _moveSound = default;

    [SerializeField, Header("�~�m��Hold��")]
    private AudioClip _holdSound = default;

    [SerializeField, Header("�~�m�̒��n��")]
    private AudioClip _groundSound = default;

    // -------------------------------------------

    // �t�B�[���h���Ǘ�����X�N���v�g
    private FieldManagerScript _fieldManagerScript = default;

    // �~�m���Ǘ�����X�N���v�g
    private MinoControllerScript _minoControllerScript = default;

    // �Q�[���̏�Ԃ��Ǘ�����X�N���v�g
    private GameControllerScript _gameControllerScript = default;

    // �X�[�p�[���[�e�[�V����������X�N���v�g
    private SuperRotationScript _superRotationScript = default;

    // T�X�s���𔻒肷��X�N���v�g
    private TSpinCheckScript _tSpinCheckScript = default;

    #endregion

    #region �v���p�e�B
    // ����ł���~�m
    public GameObject PlayerableMino { get => _playerableMino; set => _playerableMino = value; }

    // ���n����
    public bool IsGround { get => isGround; set => isGround = value; }

    #endregion

    /// <summary>
    /// �X�V�O����
    /// </summary>
    private void Start()
    {     
        // FieldDataScript���擾
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();

        // MinoControllerScript���擾
        _minoControllerScript = GetComponent<MinoControllerScript>();

        // GameControllerScript���擾
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // SuperRotationScript���擾
        _superRotationScript = GetComponent<SuperRotationScript>();

        // TSpinCheckScript���擾
        _tSpinCheckScript = GetComponent<TSpinCheckScript>();

        // AudioSource���擾
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// PlayerController
    /// �v���C���[�̋����𐧌䂷��
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
        // ����͂��ꂽ��
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // �~�m��n�ʂ܂ŗ��Ƃ�
            HardDrop();
        }
        // �����͂��ꂽ��
        if (_verticalInput < 0)
        {
            // ���ɂP�}�X�ړ�����
            SoftDrop();
        }

        // �~�m�����R����������
        NaturalFall();

        // �~�m�̐ݒu���������
        LockDown();

        // ����]
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotationMove(INPUT_LEFT);
        }
        // �E��]
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RotationMove(INPUT_RIGHT);
        }
        
        // SPACE�L�[�������ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �z�[���h
            Hold();   
        }
    }

    /// <summary>
    /// HorizontalMove
    /// �~�m�����E�Ɉړ�����
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
            PlayerableMino.transform.Translate(Vector3.right * input, Space.World);

            // ���݂̃^�C�}�[��ݒ�
            _inputTime = Time.time;

            // �ǂƏd�Ȃ�����
            if (CheckCollision(PlayerableMino))
            {
                // �P�}�X�߂�
                PlayerableMino.transform.Translate(Vector3.right * -input, Space.World);
            }
        }     
    }

    /// <summary>
    /// HardDro
    /// �~�m��������������
    /// </summary>
    private void HardDrop()
    {
        // ���n�����Đ�
        _audioSource.PlayOneShot(_groundSound);

        // ���n����܂ŉ��ɗ��Ƃ�
        GroundFall();

        // �u�����~�m���t�B�[���h�ɔ��f�����鏈��
        UpdateMinoToField();

        // �e�I�u�W�F�i��]��)�Ǝq�I�u�W�F�i�~�m�j�̐e�q�֌W����������
        CutParentMino();

        // �Q�[���̏�Ԃ��~�m�������Ă����ԂɕύX����
        _gameControllerScript.GameType = GameControllerScript.GameState.MINO_DELETE;
    }

    /// <summary>
    /// SoftDrop
    /// �~�m�����ɂP�}�X�ړ�����
    /// </summary>
    private void SoftDrop()
    {
        // ���Ԃ��o�߂�����
        if ((Time.time - _inputTime) > _inputCoolTime)
        {
            // ���ɂP�}�X�ړ�
            PlayerableMino.transform.Translate(-Vector3.up, Space.World);

            // ���݂̃^�C�}�[��ݒ�
            _inputTime = Time.time;

            // �ǂɏd�Ȃ�����
            if (CheckCollision(PlayerableMino))
            {
                // ��ɂP�}�X�ړ�
                PlayerableMino.transform.Translate(Vector3.up, Space.World);

                // ���n�����ݒ�
                IsGround = true;
            }
        }
    }

    /// <summary>
    /// NaturalFall
    /// �~�m�����R����������
    /// </summary>
    private void NaturalFall()
    {
        // ���Ԃ��o�߂�����
        if ((Time.time - _fallTime) > _fallCoolTime)
        {
            // ���ɂP�}�X�ړ�
            PlayerableMino.transform.Translate(-Vector3.up, Space.World);

            // ���݂̎��Ԃ�ݒ�
            _fallTime = Time.time;

            // �ǂɏd�Ȃ�����
            if (CheckCollision(PlayerableMino))
            {
                // �P�}�X�߂�
                PlayerableMino.transform.Translate(Vector3.up, Space.World);

                // ���n�����ݒ�
                IsGround = true;
            }
        }
    }

    /// <summary>
    /// RotationMove
    /// �~�m����]����
    /// </summary>
    /// <param name="input">���E����</param>
    private void RotationMove(int input)
    {
        // ��]�����Đ�
        _audioSource.PlayOneShot(_rotationSound);

        // �X�[�p�[���[�e�[�V���������Ă���鏈���i�v���C���[�~�m�A�����́j
        _superRotationScript.SuperRotation(PlayerableMino, input);
    }

    /// <summary>
    /// Hold
    /// �~�m��Hold����
    /// </summary>
    private void Hold()
    {
        // Hold�����Đ�
        _audioSource.PlayOneShot(_holdSound);

        // Hold�̒��g���o�����ꂷ��i����ł���~�m�AHold���j
        _minoControllerScript.HoldController(PlayerableMino,_holdSound);
    }

    /// <summary>
    /// LockDown
    /// �~�m�̐ݒu���������
    /// </summary>
    private void LockDown()
    {      
        // ���n���Ă���
        if (IsGround)
        {
            // ���n�����u��
            if (_groundTime <= 0)
            {
                // �n�ʂɒ��n�����Ƃ��̍��W����
                _groundPosition.y = _playerableMino.transform.position.y;

                // �v���C���[��X���W��ݒ�
                _oldPosition.x = _playerableMino.transform.position.x;

                // �v���C���[��Z�p�x��ݒ�
                _oldRotation.z = _playerableMino.transform.rotation.z;
            }

            //�^�C�}�[���Z
            _groundTime += Time.deltaTime;

            // �v���C���[�̌��݂̍��W�����n�������W�����P�ȏ�Ⴉ������
            if (_groundPosition.y > _playerableMino.transform.position.y)
            {
                // �^�C�}�[��������
                _groundTime = 0;

                // ���͉񐔂�������
                _inputCount = 0;

                // ���n�����������
                IsGround = false;
            }
            // ���͉񐔂�����𒴂��Ă��Ȃ�������
            if (_inputCount <= INPUT_COUNT_MAX)
            {
                // �~�m�����ɓ����Ă�����
                if (_oldPosition.x != _playerableMino.transform.position.x)
                {
                    // ���͉񐔂𑝂₷
                    _inputCount++;

                    // �^�C�}�[��������
                    _groundTime = 0;
                }
                // �~�m�̊p�x�������Ă�����
                if (_oldRotation.z != _playerableMino.transform.rotation.z)
                {
                    // ���͉񐔂𑝂₷
                    _inputCount++;

                    // �^�C�}�[��������
                    _groundTime = 0;
                }
            }
            // ���Ԃ��o�߂�����
            if (_groundTime >= SET_UP_TIME)
            {
                // ���n�����Đ�
                _audioSource.PlayOneShot(_groundSound);

                // ���n����܂ŉ��ɗ��Ƃ�
                GroundFall();

                // �u�����~�m���t�B�[���h�f�[�^�ɔ��f�����鏈��
                UpdateMinoToField();

                // �e�I�u�W�F�i��]��)�Ǝq�I�u�W�F�i�~�m�j�̐e�q�֌W����������
                CutParentMino();

                // �Q�[���̏�Ԃ��~�m�������Ă����ԂɕύX����
                _gameControllerScript.GameType = GameControllerScript.GameState.MINO_DELETE;

                return;
            }
            // �v���C���[��X���W��ݒ�
            _oldPosition.x = _playerableMino.transform.position.x;

            // �v���C���[��Z�p�x��ݒ�
            _oldRotation.z = _playerableMino.transform.rotation.z;
        }
        // ���n���Ă��Ȃ�
        else if (!IsGround)
        {
            // �^�C�}�[��������
            _groundTime = 0;
        }
    }

    /// <summary>
    /// CheckCollision
    /// �~�m���ǂɏd�Ȃ��Ă��邩���肷��
    /// </summary>
    /// <param name="mino">���ׂ�~�m</param>
    /// <returns>�ǂɏd�Ȃ��Ă��邩</returns>
    public bool CheckCollision(GameObject mino)
    {
        // �~�m�̂��ׂẴu���b�N�𒲂ׂ�
        foreach(Transform children in mino.GetComponentInChildren<Transform>())
        {
            // ������
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);
         
            // �~�m���X�e�[�W�͈̔͊O��������
            if (posX <= MIN_FIELD_WIDTH || MAX_FIELD_WIDTH <= posX || posY <= MAX_FIELD_HEIGHT)
            {
                // �d�Ȃ��Ă���
                return true;
            }
            // �u���Ă�~�m�ɏd�Ȃ�����
            if (posY < 0 && _fieldManagerScript.FieldData[posX, -posY] != null)
            {
                // �d�Ȃ��Ă���
                return true;
            }
        }
        // �d�Ȃ��Ă��Ȃ�
        return false;
    }

    /// <summary>
    /// UpdateMinoToField
    /// �u�����~�m���t�B�[���h�ɔ��f������
    /// </summary>
    private void UpdateMinoToField()
    {
        // �~�m�̂��ׂẴu���b�N�𒲂ׂ�
        foreach (Transform children in PlayerableMino.GetComponentInChildren<Transform>())
        {
            // ������
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);
            
            // �t�B�[���h�̏�Ƀ~�m��u������ 
            if(posY >= 0)
            {
                // �Q�[���I�[�o�[
                _gameControllerScript.GameOverScene();
                return;
            }
            // �u�����~�m���t�B�[���h�ɔ��f������
            _fieldManagerScript.FieldData[posX, -posY] = children.gameObject;            
        }

        // T�X�s���𔻒�
        _tSpinCheckScript.IsTSpin = _tSpinCheckScript.TSpinCheck(PlayerableMino);
    }

    /// <summary>
    /// CutParentMino
    /// �e�I�u�W�F�i��]��)�Ǝq�I�u�W�F�i�~�m�j�̐e�q�֌W����������
    /// </summary>
    private void CutParentMino()
    {
        // �e�q�֌W������
        PlayerableMino.transform.DetachChildren();
    }

    /// <summary>
    /// GroundFall
    /// �~�m��n�ʂ܂ŗ��Ƃ�
    /// </summary>
    private void GroundFall()
    {
        // �~�m���ǂɏd�Ȃ�܂�
        while (!CheckCollision(PlayerableMino))
        {
            // ���ɂP�}�X�ړ�
            PlayerableMino.transform.Translate(-Vector3.up, Space.World);
        }
        // ��ɂP�}�X�߂�
        PlayerableMino.transform.Translate(Vector3.up, Space.World);
    }
}
