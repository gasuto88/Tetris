using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;
    private float _verticalInput = default;

    private GameObject _playerMino = default;

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

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
    
    private void Start()
    {
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
        
        _defaultMinoLifeTime = _minoLifeTime;

        _minoControllerScript = GameObject.Find("MinoController").GetComponent<MinoControllerScript>();
    }

    public void PlayerController(
        GameControllerScript.MinoEraseChangeMethod _minoEraseMethod,GameControllerScript.MinoCreateMethod _minoCreateMethod)
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");    
        
        // �E���͂��ꂽ�Ƃ�
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            PlayerMino.transform.Translate(1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(-1f, 0f, 0f, Space.World);
            }
        }
        // �����͂��ꂽ�Ƃ�
        else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            PlayerMino.transform.Translate(-1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(1f, 0f, 0f, Space.World);
            }
        }
        // ����͂��ꂽ�Ƃ�
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            while (!BeforeMoving())
            {
                PlayerMino.transform.Translate(0f, -1f, 0f, Space.World);
            }
            PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

            AddMinoToField();         

            CutParentMino();
            _minoEraseMethod();
        }
        // �����͂��ꂽ�Ƃ�
        else if (_verticalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _inputTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

                AddMinoToField();
                
                CutParentMino();
                _minoEraseMethod();               
            }
        }

        if ((Time.time - _fallTime) > _fallCoolTime)
        {          
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _fallTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

                AddMinoToField();
                
                CutParentMino();
                _minoEraseMethod();            
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // �E��]
            PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            if ( BeforeMoving())
            {
                PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);              
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // ����]
            PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            if (BeforeMoving())
            {
                PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);              
            }
        }

        // �z�[���h
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _minoControllerScript.HoldController(PlayerMino,_minoCreateMethod);          
        }

     
        //if (-17.5f >= PlayerMino.transform.position.y)
        //{
        //    _minoFloorTime += Time.deltaTime;

        //    //// �~�m�������Ă�����
        //    //if (_playerMino.transform != _beforeTransform)
        //    //{
        //    //    Debug.LogError("�Ă΂ꂽ");
        //    //    // �~�m�̐������Ԃ𑝂₷
        //    //    _minoLifeTime += _addDeathTime;
        //    //}
        //    //if ((Time.time - _beforeTime) > _beforeCoolTime)
        //    //{

        //    //    _beforeTransform.position = _playerMino.transform.position;
        //    //    _beforeTime = Time.time;
        //    //}
        //    _minoLifeTime = Mathf.Clamp(_minoLifeTime, _minMinoLifeTime, _maxMinoLifeTime);
        //}

        //if (_minoFloorTime > _minoLifeTime)
        //{
        //    //foreach(Transform t in _playerMino.GetComponentInChildren<Transform>())
        //    //{
        //    //    _fieldDataScript.FieldData[(int)t.position.x,(int)t.position.y] = 2;
        //    //}

        //    // �~�m�̐������ԃ��Z�b�g
        //    _minoLifeTime = _defaultMinoLifeTime;

        //    // �^�C�}�[���Z�b�g
        //    _minoFloorTime = 0;

        //    // ���̃~�m�𐶐����鏈���ɐ؂�ւ���
        //    _gameTypeChangeMethod();
        //}

    }

    private bool BeforeMoving()
    {
        foreach(Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            Debug.Log("X "+_children.transform.position.x +" Y "+ _children.transform.position.y);
            
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);

            Debug.Log(_posX + "" + _posY);
            // �~�m���X�e�[�W�͈̔͊O��������
            if (_posX <= -1 || 10 <= _posX || _posY <= -20)
            {
                return true;
            }

            if (_posY <= 0 && _fieldDataScript.FieldData[-_posY,_posX] != null)
            {
                return true;
            }
        }
        return false;
    }

    private int FloorLocation()
    {
        int _leastPosY = default;
        foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            int _playerPosY = Mathf.RoundToInt(PlayerMino.transform.position.y);
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
            

            for (int i = _posY;i > -20;i--)
            {
                if (_fieldDataScript.FieldData[-i, _posX] != null && _leastPosY < (i + 1) - (_playerPosY - _posY))
                {
                    _leastPosY = (i + 1) - (_playerPosY - _posY);
                }                              
            }

            //if (_leastPosY == 0)
            //{
            //    _leastPosY = -_fieldDataScript.Height + (_posY - (_playerPosY - _posY));
            //}          
        }
       
        return _leastPosY;
    }

    //private void PutInside()
    //{
    //    foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
    //    {
    //        int _posX = Mathf.RoundToInt(_children.transform.position.x);
    //        int _posY = Mathf.RoundToInt(_children.transform.position.y);

    //        // �~�m���X�e�[�W�͈̔͊O��������
    //        if ( _posX <= -1)
    //        {
    //            PlayerMino.transform.Translate(1f, 0f, 0f, Space.World);
    //        }
    //        else if ( 10 <= _posX)
    //        {
    //            PlayerMino.transform.Translate(-1f, 0f, 0f, Space.World);
    //        }
    //        if(_posY <= -20)
    //        {
    //            PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);
    //        }

    //    }
    //}

    /// <summary>
    /// �t�B�[���h�f�[�^�ɒu�����~�m�𔽉f�����鏈��
    /// </summary>
    private void AddMinoToField()
    {
        foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);

            // �t�B�[���h�ɒu�����~�m�𔽉f������
            _fieldDataScript.FieldData[-_posY, _posX] = _children.gameObject;
        }
    }
    /// <summary>
    /// �~�m�̐e�I�u�W�F�N�g�i��]���j����������
    /// </summary>
    private void CutParentMino()
    {
        PlayerMino.transform.DetachChildren();
        Destroy(PlayerMino);
    }
}
