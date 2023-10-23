using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;

    private float _verticalInput = default;

    [SerializeField, Header("���̓N�[���^�C��")]
    private float _inputCoolTime = 0.1f;

    private float _inputTime = default;

    private float _fallTime = default;

    private float _fallCoolTime = 1f;

    private bool isLeft = default;

    private bool isRight = default;

    private bool isDown = default;

    private float _floorTime = default;

    private FieldDataScript _fieldDataScript = default;

    private void Start()
    {
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
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
            for (; 0f >= t.position.x;)
            {
                _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
            }
            for (; 11f <= t.position.x;)
            {
                _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);               
            }
            for (; -20f >= t.position.y;)
            {
                _playerMino.transform.Translate(0f, 1f, 0f, Space.World);              
            }
        }
        if (-18f >= _playerMino.transform.position.y)
        {
            _floorTime += Time.deltaTime;
        }
        if (_floorTime > 1f)
        {
            _floorTime = 0;
            _gameTypeChangeMethod();
        }
    }
}
