using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    private float _horizontalInput = default;
    private float _verticalInput = default;

    // 操作できるミノ
    private GameObject _playerMino = default;

    [SerializeField, Header("入力時のクールタイム")]
    private float _inputCoolTime = 0.1f;

    private float _inputTime = default;

    private float _fallTime = default;

    [SerializeField,Header("ミノが落ちる時間")]
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

    private MinoControllerScript _minoControllerScript = default;

    private GameControllerScript _gameControllerScript = default;

    private SuperRotationScript _superRotationScript = default;

    private float _groundTime = default;
    [SerializeField,Header("ミノが死ぬ時間")]
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
        
        // 右入力されたとき
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // 右に１マス移動
            PlayerMino.transform.Translate(1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            // プレイヤーが壁にめり込んだら
            if (BeforeMoving(PlayerMino))
            {
                // １マス戻す
                PlayerMino.transform.Translate(-1f, 0f, 0f, Space.World);
            }
        }
        // 左入力されたとき
        else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // 左に１マス移動
            PlayerMino.transform.Translate(-1f, 0f, 0f,Space.World);

            _inputTime = Time.time;

            // プレイヤーが壁にめり込んだら
            if (BeforeMoving(PlayerMino))
            {
                // １マス戻す
                PlayerMino.transform.Translate(1f, 0f, 0f, Space.World);
            }
        }
        // 上入力されたとき
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // プレイヤーが着地するまで
            while (!BeforeMoving(PlayerMino))
            {
                // 下に１マス移動
                PlayerMino.transform.Translate(0f, -1f, 0f, Space.World);
            }
            // １マス戻す
            PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

            // フィールドに置いたミノを反映させる
            AddMinoToField();         

            // 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す
            CutParentMino();
           
            _minoEraseMethod();
            return;
        }
        // 下入力されたとき
        else if (_verticalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // 下に１マス移動
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _inputTime = Time.time;

            // プレイヤーが壁にめり込んだら
            if (BeforeMoving(PlayerMino))
            {
                // 上に１マス移動
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);
                
                    // フィールドに置いたミノを反映させる
                    AddMinoToField();

                    // 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す
                    CutParentMino();
                    _minoEraseMethod();
                    return;
               
            }          
        }

        if ((Time.time - _fallTime) > _fallCoolTime)
        {
            // 下に１マス移動
            PlayerMino.transform.Translate(0f, -1f, 0f,Space.World);

            _fallTime = Time.time;

            // プレイヤーが壁にめり込んだら
            if (BeforeMoving(PlayerMino))
            {
                // １マス戻す
                PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);

                IsGround = true;
            }         
        }

        if (IsGround)
        {
            _groundTime += Time.deltaTime;

            if (_groundTime > _deathTime)
            {
                // フィールドに置いたミノを反映させる
                AddMinoToField();

                // 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す
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
            //// 右回転
            //PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            //// プレイヤーが壁にめり込んだら
            //if (BeforeMoving(PlayerMino))
            //{
            //    // 右回転
            //    PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            
            //}
            _superRotationScript.SuperRotation(PlayerMino, 1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //// 左回転
            //PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            //// プレイヤーが壁にめり込んだら
            //if (BeforeMoving(PlayerMino))
            //{
            //    // 右回転
            //    PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            
            //}
            _superRotationScript.SuperRotation(PlayerMino, -1);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            // 右回転
            PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);

            // プレイヤーが壁にめり込んだら
            if (BeforeMoving(PlayerMino))
            {
                // 右回転
                PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);


            }
           
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // 左回転
            PlayerMino.transform.Rotate(0f, 0f, -90f, Space.World);

            // プレイヤーが壁にめり込んだら
            if (BeforeMoving(PlayerMino))
            {
                // 右回転
                PlayerMino.transform.Rotate(0f, 0f, 90f, Space.World);


            }
            
        }

        // ホールド
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _minoControllerScript.HoldController(PlayerMino,_minoCreateMethod);          
        }
    }
    /// <summary>
    /// プレイヤーが壁にめり込んでるかどうか
    /// </summary>
    /// <param name="_mino"></param>
    /// <returns></returns>
    public bool BeforeMoving(GameObject _mino)
    {
        foreach(Transform _children in _mino.GetComponentInChildren<Transform>())
        {          
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
         
            // ミノがステージの範囲外だったら
            if (_posX <= -1 || 10 <= _posX || _posY <= -20)
            {
                return true;
            }
            // プレイヤーが置いてるミノにめり込んだら
            if (_posY < 0 && _fieldDataScript.FieldData[-_posY,_posX] != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// フィールドデータに置いたミノを反映させる処理
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
            // フィールドに置いたミノを反映させる
            _fieldDataScript.FieldData[-_posY, _posX] = _children.gameObject;
        }
    }
    /// <summary>
    /// 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離して
    /// 親を消す処理
    /// </summary>
    private void CutParentMino()
    {
        PlayerMino.transform.DetachChildren();
        Destroy(PlayerMino);
    }
}
