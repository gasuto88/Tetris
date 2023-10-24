using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;
    private float _verticalInput = default;

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

    public void PlayerController(GameObject _playerMino,GameControllerScript.GameTypeChangeMethod _gameTypeChangeMethod)
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerMino.transform.Rotate(0f, 0f, 90f, Space.World);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _playerMino.transform.Rotate(0f, 0f, -90f, Space.World);
        }       
        
        // �E���͂��ꂽ�Ƃ�
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            _playerMino.transform.Translate(1f, 0f, 0f,Space.World);

            _inputTime = Time.time;
        }
        // �����͂��ꂽ�Ƃ�
        else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            _playerMino.transform.Translate(-1f, 0f, 0f,Space.World);

            _inputTime = Time.time;
        }
        // ����͂��ꂽ�Ƃ�
        if (_verticalInput > 0)
        {

        }
        // �����͂��ꂽ�Ƃ�
        else if (_verticalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            _playerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _inputTime = Time.time;
        }

        if ((Time.time - _fallTime) > _fallCoolTime)
        {          
            _playerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _fallTime = Time.time;
        }

        foreach (Transform t in _playerMino.GetComponentInChildren<Transform>())
        {

            for (;0f >= t.position.x;)
            {
                _playerMino.transform.Translate(1f, 0f, 0f, Space.World);

            }
            for (; 11f <= t.position.x;)
            {
                _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
            }
            for (;-20f >= t.position.y ; )
            {
                _playerMino.transform.Translate(0f, 1f, 0f, Space.World);
            }
        }
        foreach(Transform t in _playerMino.GetComponentInChildren<Transform>())
        {
            if(_fieldDataScript.FieldData[(int)t.position.x,(int)t.position.y - 1] >= 1)
            {

            }
        }

        if (-17.5f >= _playerMino.transform.position.y)
        {
            _minoFloorTime += Time.deltaTime;

            //// �~�m�������Ă�����
            //if (_playerMino.transform != _beforeTransform)
            //{
            //    Debug.LogError("�Ă΂ꂽ");
            //    // �~�m�̐������Ԃ𑝂₷
            //    _minoLifeTime += _addDeathTime;
            //}
            //if ((Time.time - _beforeTime) > _beforeCoolTime)
            //{

            //    _beforeTransform.position = _playerMino.transform.position;
            //    _beforeTime = Time.time;
            //}
            _minoLifeTime = Mathf.Clamp(_minoLifeTime,_minMinoLifeTime,_maxMinoLifeTime);
        }

        if (_minoFloorTime > _minoLifeTime)
        {      
            //foreach(Transform t in _playerMino.GetComponentInChildren<Transform>())
            //{
            //    _fieldDataScript.FieldData[(int)t.position.x,(int)t.position.y] = 2;
            //}

            // �~�m�̐������ԃ��Z�b�g
            _minoLifeTime = _defaultMinoLifeTime;

            // �^�C�}�[���Z�b�g
            _minoFloorTime = 0;

            // ���̃~�m�𐶐����鏈���ɐ؂�ւ���
            _gameTypeChangeMethod();
        }
        
    }
}
