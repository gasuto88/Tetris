using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{    
    [SerializeField, Header("���̓N�[���^�C��")]
    private float _inputCoolTime = 0.1f;

    private float _inputTime = default;

    private float _fallTime = default;

    private float _fallCoolTime = 1f;

    public void PlayerController(GameObject _playerMino)
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal");
        float _verticalInput = Input.GetAxisRaw("Vertical");

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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerMino.transform.Rotate(0f, 0f, 90f,Space.World);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            _playerMino.transform.Rotate(0f, 0f, -90f,Space.World);
        }
    }
}
