using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;
    private float _verticalInput = default;

    // ����ł���~�m
    private GameObject _playerMino = default;

    [SerializeField, Header("���͎��̃N�[���^�C��")]
    private float _inputCoolTime = 0.1f;

    private float _inputTime = default;

    private float _fallTime = default;

    [SerializeField,Header("�~�m�������鎞��")]
    private float _fallCoolTime = 1f;

    // �~�m�����ɂӂ�Ă���̎���
    private float _minoFloorTime = default;

    private float _minMinoLifeTime = 1.1f;
    private float _maxMinoLifeTime = 3f;

    // �~�m�̐�������
    private float _minoLifeTime = 0.5f;

    private float _defaultMinoLifeTime = default;

    private float _addDeathTime = 0.5f;

    private Transform _beforeTransform = default;

    private FieldDataScript _fieldDataScript = default;

    private float _beforeTime = default;

    private float _beforeCoolTime = 0.3f;

    private MinoControllerScript _minoControllerScript = default;

    private GameControllerScript _gameControllerScript = default;

    private SuperRotationScript _superRotationScript = default;

    private float _groundTime = default;
    [SerializeField,Header("�~�m�����ʎ���")]
    private float _deathTime = default;

    private bool isGround = default;

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }
    public bool IsGround { get => isGround; set => isGround = value; }

    private void Start()
    {
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
        
        _defaultMinoLifeTime = _minoLifeTime;

        _minoControllerScript = GameObject.Find("MinoController").GetComponent<MinoControllerScript>();

        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        _superRotationScript = GetComponent<SuperRotationScript>();
    }

    public void PlayerController(
        GameControllerScript.MinoEraseChangeMethod _minoEraseMethod,GameControllerScript.MinoCreateMethod _minoCreateMethod)
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");    
        
        // �E���͂��ꂽ�Ƃ�
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
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
            // �v���C���[�����n����܂�
            while (!BeforeMoving(PlayerMino))
            {
                // ���ɂP�}�X�ړ�
                PlayerMino.transform.Translate(0f, -1f, 0f, Space.World);
            }
            // �P�}�X�߂�
            PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

            // �t�B�[���h�ɒu�����~�m�𔽉f������
            AddMinoToField();         

            // �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣��
            CutParentMino();
           
            _minoEraseMethod();
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
                
                    // �t�B�[���h�ɒu�����~�m�𔽉f������
                    AddMinoToField();

                    // �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣��
                    CutParentMino();
                    _minoEraseMethod();
                    return;
               
            }          
        }

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

                IsGround = true;
            }         
        }

        if (IsGround)
        {
            _groundTime += Time.deltaTime;

            if (_groundTime > _deathTime)
            {
                // �t�B�[���h�ɒu�����~�m�𔽉f������
                AddMinoToField();

                // �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣��
                CutParentMino();

                _minoEraseMethod();

                return;
            }
        }
        else if (!IsGround)
        {
            _groundTime = 0;
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //// �E��]
            //PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            //// �v���C���[���ǂɂ߂荞�񂾂�
            //if (BeforeMoving(PlayerMino))
            //{
            //    // �E��]
            //    PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            
            //}
            _superRotationScript.SuperRotation(PlayerMino, 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //// ����]
            //PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            //// �v���C���[���ǂɂ߂荞�񂾂�
            //if (BeforeMoving(PlayerMino))
            //{
            //    // �E��]
            //    PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            
            //}
            _superRotationScript.SuperRotation(PlayerMino, -1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            // �E��]
            PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            // �v���C���[���ǂɂ߂荞�񂾂�
            if (BeforeMoving(PlayerMino))
            {
                // �E��]
                PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);


            }
           
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // ����]
            PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            // �v���C���[���ǂɂ߂荞�񂾂�
            if (BeforeMoving(PlayerMino))
            {
                // �E��]
                PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);


            }
            
        }

        // �z�[���h
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _minoControllerScript.HoldController(PlayerMino,_minoCreateMethod);          
        }
    }
    /// <summary>
    /// �v���C���[���ǂɂ߂荞��ł邩�ǂ���
    /// </summary>
    /// <param name="_mino"></param>
    /// <returns></returns>
    public bool BeforeMoving(GameObject _mino)
    {
        foreach(Transform _children in _mino.GetComponentInChildren<Transform>())
        {          
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
         
            // �~�m���X�e�[�W�͈̔͊O��������
            if (_posX <= -1 || 10 <= _posX || _posY <= -20)
            {
                return true;
            }
            // �v���C���[���u���Ă�~�m�ɂ߂荞�񂾂�
            if (_posY < 0 && _fieldDataScript.FieldData[-_posY,_posX] != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// �t�B�[���h�f�[�^�ɒu�����~�m�𔽉f�����鏈��
    /// </summary>
    private void AddMinoToField()
    {
        foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);

            if(_posY >= 0)
            {

                //_gameControllerScript.GameType = GameControllerScript.GameState.END;
                SceneManager.LoadScene("GameOverScene");
                return;
            }
            // �t�B�[���h�ɒu�����~�m�𔽉f������
            _fieldDataScript.FieldData[-_posY, _posX] = _children.gameObject;
        }
    }
    /// <summary>
    /// �e�I�u�W�F�N�g�i��]��)�Ǝq�I�u�W�F�N�g�i�~�m�~�S�j��؂藣����
    /// �e����������
    /// </summary>
    private void CutParentMino()
    {
        PlayerMino.transform.DetachChildren();
        Destroy(PlayerMino);
    }
}
