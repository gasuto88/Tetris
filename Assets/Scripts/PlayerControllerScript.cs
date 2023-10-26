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

    [SerializeField,Header("�~�m�̗������Ŏ~�܂鎞��")]
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
    
    private void Start()
    {
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
        
        _defaultMinoLifeTime = _minoLifeTime;
    }

    public void PlayerController(GameControllerScript.GameTypeChangeMethod _gameTypeChangeMethod)
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
        if (_verticalInput > 0)
        {

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
                //Debug.LogError("�~�m���u���ꂽ");
                _gameTypeChangeMethod();               
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
                //Debug.LogError("�~�m���u���ꂽ");
                _gameTypeChangeMethod();            
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // �E��]
            PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            if ( BeforeMoving())
            {
                PutInside();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // ����]
            PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            if (BeforeMoving())
            {
                PutInside();
            }
        }

        // �f�o�b�N�p
        //for (int j = 0; j < 20; j++)
        //{
        //    string unti = "";
        //    for (int i = 0; i < 10; i++)
        //    {
        //        unti += _fieldDataScript.FieldData[j, i];
               
        //    }
        //    Debug.LogWarning(unti);
        //}


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

    private void PutInside()
    {
        foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);

            // �~�m���X�e�[�W�͈̔͊O��������
            if ( _posX <= -1)
            {
                PlayerMino.transform.Translate(1f, 0f, 0f, Space.World);
            }
            else if ( 10 <= _posX)
            {
                PlayerMino.transform.Translate(-1f, 0f, 0f, Space.World);
            }
            if(_posY <= -20)
            {
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);
            }
           
        }
    }

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
}
