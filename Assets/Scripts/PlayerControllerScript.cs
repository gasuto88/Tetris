using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;
    private float _verticalInput = default;

    [SerializeField, Header("入力時のクールタイム")]
    private float _inputCoolTime = 0.1f;

    private float _inputTime = default;

    private float _fallTime = default;

    [SerializeField,Header("ミノの落下時で止まる時間")]
    private float _fallCoolTime = 1f;

    // ミノが床にふれてからの時間
    private float _minoFloorTime = default;

    private float _minMinoLifeTime = 1.1f;
    private float _maxMinoLifeTime = 3f;

    // ミノの生存時間
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
        
        // 右入力されたとき
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            _playerMino.transform.Translate(1f, 0f, 0f,Space.World);

            _inputTime = Time.time;
        }
        // 左入力されたとき
        else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            _playerMino.transform.Translate(-1f, 0f, 0f,Space.World);

            _inputTime = Time.time;
        }
        // 上入力されたとき
        if (_verticalInput > 0)
        {

        }
        // 下入力されたとき
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

            //// ミノが動いていたら
            //if (_playerMino.transform != _beforeTransform)
            //{
            //    Debug.LogError("呼ばれた");
            //    // ミノの生存時間を増やす
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

            // ミノの生存時間リセット
            _minoLifeTime = _defaultMinoLifeTime;

            // タイマーリセット
            _minoFloorTime = 0;

            // 次のミノを生成する処理に切り替える
            _gameTypeChangeMethod();
        }
        
    }
}
