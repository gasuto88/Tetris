using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;
    private float _verticalInput = default;

    private GameObject _playerMino = default;

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

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

    public void PlayerController(GameControllerScript.GameTypeChangeMethod _gameTypeChangeMethod)
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");    
        
        // 右入力されたとき
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            PlayerMino.transform.Translate(1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(-1f, 0f, 0f, Space.World);
            }
        }
        // 左入力されたとき
        else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            PlayerMino.transform.Translate(-1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(1f, 0f, 0f, Space.World);
            }
        }
        // 上入力されたとき
        if (_verticalInput > 0)
        {

        }
        // 下入力されたとき
        else if (_verticalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _inputTime = Time.time;

            if (BeforeMoving())
            {
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

                AddMinoToField();
                //Debug.LogError("ミノが置かれた");
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
                //Debug.LogError("ミノが置かれた");
                _gameTypeChangeMethod();            
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 右回転
            PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            if ( BeforeMoving())
            {
                PutInside();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // 左回転
            PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            if (BeforeMoving())
            {
                PutInside();
            }
        }

        // デバック用
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

        //    //// ミノが動いていたら
        //    //if (_playerMino.transform != _beforeTransform)
        //    //{
        //    //    Debug.LogError("呼ばれた");
        //    //    // ミノの生存時間を増やす
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

        //    // ミノの生存時間リセット
        //    _minoLifeTime = _defaultMinoLifeTime;

        //    // タイマーリセット
        //    _minoFloorTime = 0;

        //    // 次のミノを生成する処理に切り替える
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
            // ミノがステージの範囲外だったら
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

            // ミノがステージの範囲外だったら
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

            // フィールドに置いたミノを反映させる
            _fieldDataScript.FieldData[-_posY, _posX] = _children.gameObject;
        }
    }
}
