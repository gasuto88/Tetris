using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    #region フィールド変数

    private float _horizontalInput = default;
    private float _verticalInput = default;

    // 操作できるミノ
    private GameObject _playerMino = default;

    // プレイヤーが地面に着いたときの座標
    private Vector3 _playerGroundPosition = default;

    // 入力されてからの経過時間
    private float _inputTime = default;

    [SerializeField, Header("入力時のクールタイム")]
    private float _inputCoolTime = 0.1f;

    // 左右入力（回転）
    private const int LEFT_INPUT = -1;
    private const int RIGHT_INPUT = 1;

    // プレイヤーが落下してからの経過時間
    private float _fallTime = default;

    [SerializeField,Header("ミノが落ちる時間")]
    private float _fallCoolTime = 0.7f;

    // プレイヤーが着地してからの経過時間
    private float _groundTime = default;

    // プレイヤーが地面にいるかどうか
    private bool isGround = default;

    [SerializeField,Header("ミノが死ぬまでの時間")]
    private float _deathTime = 0.3f;

    [SerializeField, Header("ミノが死ぬまでの時間（最大）")]
    private float _maxDeathTime = 1.2f;

    [SerializeField,Header("ミノが死ぬまでの時間（最小）")]
    private float _minDeathTime = 0.3f;

    [SerializeField,Header("ミノの死ぬまでの追加時間")]
    private float _addDeathTime = 0.15f;

    //------------------効果音--------------------

    [Space(20),Header("効果音")]

    private AudioSource _audioSource = default;

    [SerializeField,Header("ミノの回転音")]
    private AudioClip _rotationSound = default;

    [SerializeField, Header("ミノの移動音")]
    private AudioClip _moveSound = default;

    [SerializeField, Header("ミノのホールド音")]
    private AudioClip _holdSound = default;

    [SerializeField, Header("ミノの着地音")]
    private AudioClip _groundSound = default;

    //---------------------------------------------

    // フィールドを管理するスクリプト
    private FieldDataScript _fieldDataScript = default;

    // ミノを管理するスクリプト
    private MinoControllerScript _minoControllerScript = default;

    // ゲームの状態を管理するスクリプト
    private GameControllerScript _gameControllerScript = default;

    // スーパーローテーションをしてくれるスクリプト
    private SuperRotationScript _superRotationScript = default;

    #endregion

    #region プロパティ

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

    public bool IsGround { get => isGround; set => isGround = value; }

    #endregion

    private void Start()
    {
        // FieldDataScriptを取得
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();

        // MinoControllerScriptを取得
        _minoControllerScript = GetComponent<MinoControllerScript>();

        // GameControllerScriptを取得
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // SuperRotationScriptを取得
        _superRotationScript = GetComponent<SuperRotationScript>();

        // AudioSourceを取得
        _audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// プレイヤーの挙動
    /// </summary>
    public void PlayerController()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");    
        
        // 右入力されたとき
        if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
        {
            // プレイヤーの移動音を再生
            _audioSource.PlayOneShot(_moveSound);

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
            // プレイヤーの移動音を再生
            _audioSource.PlayOneShot(_moveSound);

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
            // プレイヤーの着地音
            _audioSource.PlayOneShot(_groundSound);

            // プレイヤーを着地するまで下に落とす
            GroundFall();

            // 置いたミノをフィールドデータに反映させる処理
            AddMinoToField();         

            // 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す
            CutParentMino();

            // ゲームの状態を置いたミノを消す処理に切り替える
            _gameControllerScript.GameType = GameControllerScript.GameState.MINO_ERASE;

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

                // プレイヤーが地面に着地した
                IsGround = true;
            }          
        }

        // プレイヤーの自動落下
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

                // プレイヤーが地面に着地した
                IsGround = true;
            }         
        }

        // プレイヤーが地面に着地したら
        if (IsGround)
        {
            if (_groundTime <= 0)
            {
                // プレイヤーが地面に着地したときの座標を代入
                _playerGroundPosition.y = _playerMino.transform.position.y;
            }

            _groundTime += Time.deltaTime;

            _deathTime = Mathf.Clamp(_deathTime,_minDeathTime,_maxDeathTime);

            // プレイヤーの現在の座標が着地した座標よりも１以上低かったら
            if (_playerGroundPosition.y - _playerMino.transform.position.y >= 1)
            {
                // タイマーリセット
                _groundTime = 0;

                IsGround = false;
            }

            // 一定時間地面に着地していたら
            if (_groundTime > _deathTime)
            {
                // プレイヤーの着地音
                _audioSource.PlayOneShot(_groundSound);

                // プレイヤーを着地するまで下に落とす
                GroundFall();

                // 置いたミノをフィールドデータに反映させる処理
                AddMinoToField();

                // 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す
                CutParentMino();

                // ゲームの状態を置いたミノを消す処理に切り替える
                _gameControllerScript.GameType = GameControllerScript.GameState.MINO_ERASE;

                return;
            }
        }
        else if (!IsGround)
        {
            _groundTime = 0;
        }

        // 左回転
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // プレイヤーの回転音
            _audioSource.PlayOneShot(_rotationSound);

            // スーパーローテーションをしてくれる処理（プレイヤーミノ、左入力）
            _superRotationScript.SuperRotation(PlayerMino, LEFT_INPUT);

            // プレイヤーが着地していたら
            if (isGround)
            {
                // プレイヤーが死ぬまでの時間を追加
                _deathTime += _addDeathTime;
            }
        }
        // 右回転
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // プレイヤーの回転音
            _audioSource.PlayOneShot(_rotationSound);

            // スーパーローテーションをしてくれる処理（プレイヤーミノ、右入力）
            _superRotationScript.SuperRotation(PlayerMino, RIGHT_INPUT);

            // プレイヤーが着地していたら
            if (isGround)
            {
                // プレイヤーが死ぬまでの時間を追加
                _deathTime += _addDeathTime;
            }
        }
        
        // ホールド
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            // プレイヤーのホールド音
            _audioSource.PlayOneShot(_holdSound);

            // ホールドの中身を出し入れする処理
            _minoControllerScript.HoldController(PlayerMino);          
        }
    }
    /// <summary>
    /// プレイヤーが壁にめり込んでるかを調べる処理
    /// </summary>
    /// <param name="_mino">調べるミノ</param>
    /// <returns>プレイヤーが壁にめり込んでるかどうか</returns>
    public bool BeforeMoving(GameObject _mino)
    {
        // ミノのすべてのブロックを調べる
        foreach(Transform _children in _mino.GetComponentInChildren<Transform>())
        {
            // 整数化
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
         
            // ミノがステージの範囲外だったら
            if (_posX <= -1 || 10 <= _posX || _posY <= -20)
            {
                return true;
            }
            // 置いてるミノにめり込んだら
            if (_posY < 0 && _fieldDataScript.FieldData[_posX, -_posY] != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 置いたミノをフィールドデータに反映させる処理
    /// </summary>
    private void AddMinoToField()
    {
        // ミノのすべてのブロックを調べる
        foreach (Transform _children in PlayerMino.GetComponentInChildren<Transform>())
        {
            // 整数化
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
            
            // フィールドの上にミノを置いたら 
            if(_posY >= 0)
            {
                // ゲームオーバー
                _gameControllerScript.GameOver();
                return;
            }
            // 置いたミノをフィールドデータに反映させる
            _fieldDataScript.FieldData[_posX, -_posY] = _children.gameObject;

            // フィールドデータに置いたミノの情報を送る
            _fieldDataScript.SetPlayerPosition(PlayerMino);
        }
    }
    /// <summary>
    /// 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す処理
    /// </summary>
    private void CutParentMino()
    {
        // 親についてる子オブジェクトを切り離す
        PlayerMino.transform.DetachChildren();
    }
    /// <summary>
    /// ミノを着地するまで落とす処理
    /// </summary>
    private void GroundFall()
    {
        // プレイヤーが着地するまで
        while (!BeforeMoving(PlayerMino))
        {
            // 下に１マス移動
            PlayerMino.transform.Translate(0f, -1f, 0f, Space.World);
        }
        // １マス戻す
        PlayerMino.transform.Translate(0f, 1f, 0f, Space.World);
    }
}
