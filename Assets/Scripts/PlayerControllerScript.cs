/*----------------------------------------------------------
　PlayerControllerScript.cs

　更新日　11月10日

　制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;
/// <summary>
/// プレイヤーを動かす
/// </summary>
public class PlayerControllerScript : MonoBehaviour
{
    #region フィールド変数

    // 左右入力
    private float _horizontalInput = default;
    // 上下入力
    private float _verticalInput = default;

    // 操作できるミノ
    private GameObject _playerableMino = default;    

    // 左右入力
    private const int INPUT_LEFT = -1;
    private const int INPUT_RIGHT = 1;

    // 入力回数の上限
    private const int INPUT_COUNT_MAX = 15;

    // ミノの設置時間
    private const float SET_UP_TIME = 0.5f;

    // フィールドの縦幅と横幅-------------------
    private const int MIN_FIELD_WIDTH = -1;
    private const int MAX_FIELD_WIDTH = 10;
    private const int MAX_FIELD_HEIGHT = -20;
    //----------------------------------------

    // 入力された回数
    private int _inputCount = 0;

    // プレイヤーが地面に着いたときの座標
    private Vector3 _groundPosition = default;

    // プレイヤーの過去の座標
    private Vector3 _oldPosition = default;

    // プレイヤーの過去の回転
    private Quaternion _oldRotation = default;

    // 入力されてからの経過時間
    private float _inputTime = 0f;

    [SerializeField, Header("入力時のクールタイム")]
    private float _inputCoolTime = 0.1f;  

    // プレイヤーが落下してからの経過時間
    private float _fallTime = 0f;

    [SerializeField,Header("ミノが落ちる時間")]
    private float _fallCoolTime = 0.7f;

    // プレイヤーが着地してからの経過時間
    private float _groundTime = 0f;

    // プレイヤーが地面にいるかどうか
    private bool isGround = false;

    // 効果音-------------------------------------

    [Space(20),Header("効果音")]

    private AudioSource _audioSource = default;

    [SerializeField,Header("ミノの回転音")]
    private AudioClip _rotationSound = default;

    [SerializeField, Header("ミノの移動音")]
    private AudioClip _moveSound = default;

    [SerializeField, Header("ミノのHold音")]
    private AudioClip _holdSound = default;

    [SerializeField, Header("ミノの着地音")]
    private AudioClip _groundSound = default;

    // -------------------------------------------

    // フィールドを管理するスクリプト
    private FieldManagerScript _fieldManagerScript = default;

    // ミノを管理するスクリプト
    private MinoControllerScript _minoControllerScript = default;

    // ゲームの状態を管理するスクリプト
    private GameControllerScript _gameControllerScript = default;

    // スーパーローテーションをするスクリプト
    private SuperRotationScript _superRotationScript = default;

    // Tスピンを判定するスクリプト
    private TSpinCheckScript _tSpinCheckScript = default;

    #endregion

    #region プロパティ
    // 操作できるミノ
    public GameObject PlayerableMino { get => _playerableMino; set => _playerableMino = value; }

    // 着地判定
    public bool IsGround { get => isGround; set => isGround = value; }

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {     
        // FieldDataScriptを取得
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();

        // MinoControllerScriptを取得
        _minoControllerScript = GetComponent<MinoControllerScript>();

        // GameControllerScriptを取得
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // SuperRotationScriptを取得
        _superRotationScript = GetComponent<SuperRotationScript>();

        // TSpinCheckScriptを取得
        _tSpinCheckScript = GetComponent<TSpinCheckScript>();

        // AudioSourceを取得
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// PlayerController
    /// プレイヤーの挙動を制御する
    /// </summary>
    public void PlayerController()
    {      
        //入力取得
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
        
        // 左入力されたら
        if(_horizontalInput < 0)
        {
            // 左に１マス移動する
            HorizontalMove(INPUT_LEFT);
        }
        // 右入力されたら
        else if(0 < _horizontalInput)
        {
            // 右に１マス移動する
            HorizontalMove(INPUT_RIGHT);
        }
        // 上入力されたら
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ミノを地面まで落とす
            HardDrop();
        }
        // 下入力されたら
        if (_verticalInput < 0)
        {
            // 下に１マス移動する
            SoftDrop();
        }

        // ミノを自然落下させる
        NaturalFall();

        // ミノの設置判定をする
        LockDown();

        // 左回転
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RotationMove(INPUT_LEFT);
        }
        // 右回転
        else if (Input.GetKeyDown(KeyCode.E))
        {
            RotationMove(INPUT_RIGHT);
        }
        
        // SPACEキーが押されたら
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // ホールド
            Hold();   
        }
    }

    /// <summary>
    /// HorizontalMove
    /// ミノを左右に移動する
    /// </summary>
    /// <param name="input">左右の入力</param>
    private void HorizontalMove(int input)
    {
        // 時間が経過したら
        if ((Time.time - _inputTime) > _inputCoolTime)
        {
            // 移動音を再生
            _audioSource.PlayOneShot(_moveSound);

            // １マス移動
            PlayerableMino.transform.Translate(Vector3.right * input, Space.World);

            // 現在のタイマーを設定
            _inputTime = Time.time;

            // 壁と重なったら
            if (CheckCollision(PlayerableMino))
            {
                // １マス戻す
                PlayerableMino.transform.Translate(Vector3.right * -input, Space.World);
            }
        }     
    }

    /// <summary>
    /// HardDro
    /// ミノを高速落下する
    /// </summary>
    private void HardDrop()
    {
        // 着地音を再生
        _audioSource.PlayOneShot(_groundSound);

        // 着地するまで下に落とす
        GroundFall();

        // 置いたミノをフィールドに反映させる処理
        UpdateMinoToField();

        // 親オブジェ（回転軸)と子オブジェ（ミノ）の親子関係を解除する
        CutParentMino();

        // ゲームの状態をミノを消している状態に変更する
        _gameControllerScript.GameType = GameControllerScript.GameState.MINO_DELETE;
    }

    /// <summary>
    /// SoftDrop
    /// ミノを下に１マス移動する
    /// </summary>
    private void SoftDrop()
    {
        // 時間が経過したら
        if ((Time.time - _inputTime) > _inputCoolTime)
        {
            // 下に１マス移動
            PlayerableMino.transform.Translate(-Vector3.up, Space.World);

            // 現在のタイマーを設定
            _inputTime = Time.time;

            // 壁に重なったら
            if (CheckCollision(PlayerableMino))
            {
                // 上に１マス移動
                PlayerableMino.transform.Translate(Vector3.up, Space.World);

                // 着地判定を設定
                IsGround = true;
            }
        }
    }

    /// <summary>
    /// NaturalFall
    /// ミノを自然落下させる
    /// </summary>
    private void NaturalFall()
    {
        // 時間が経過したら
        if ((Time.time - _fallTime) > _fallCoolTime)
        {
            // 下に１マス移動
            PlayerableMino.transform.Translate(-Vector3.up, Space.World);

            // 現在の時間を設定
            _fallTime = Time.time;

            // 壁に重なったら
            if (CheckCollision(PlayerableMino))
            {
                // １マス戻す
                PlayerableMino.transform.Translate(Vector3.up, Space.World);

                // 着地判定を設定
                IsGround = true;
            }
        }
    }

    /// <summary>
    /// RotationMove
    /// ミノを回転する
    /// </summary>
    /// <param name="input">左右入力</param>
    private void RotationMove(int input)
    {
        // 回転音を再生
        _audioSource.PlayOneShot(_rotationSound);

        // スーパーローテーションをしてくれる処理（プレイヤーミノ、左入力）
        _superRotationScript.SuperRotation(PlayerableMino, input);
    }

    /// <summary>
    /// Hold
    /// ミノをHoldする
    /// </summary>
    private void Hold()
    {
        // Hold音を再生
        _audioSource.PlayOneShot(_holdSound);

        // Holdの中身を出し入れする（操作できるミノ、Hold音）
        _minoControllerScript.HoldController(PlayerableMino,_holdSound);
    }

    /// <summary>
    /// LockDown
    /// ミノの設置判定をする
    /// </summary>
    private void LockDown()
    {      
        // 着地している
        if (IsGround)
        {
            // 着地した瞬間
            if (_groundTime <= 0)
            {
                // 地面に着地したときの座標を代入
                _groundPosition.y = _playerableMino.transform.position.y;

                // プレイヤーのX座標を設定
                _oldPosition.x = _playerableMino.transform.position.x;

                // プレイヤーのZ角度を設定
                _oldRotation.z = _playerableMino.transform.rotation.z;
            }

            //タイマー加算
            _groundTime += Time.deltaTime;

            // プレイヤーの現在の座標が着地した座標よりも１以上低かったら
            if (_groundPosition.y > _playerableMino.transform.position.y)
            {
                // タイマーを初期化
                _groundTime = 0;

                // 入力回数を初期化
                _inputCount = 0;

                // 着地判定を初期化
                IsGround = false;
            }
            // 入力回数が上限を超えていなかったら
            if (_inputCount <= INPUT_COUNT_MAX)
            {
                // ミノが横に動いていたら
                if (_oldPosition.x != _playerableMino.transform.position.x)
                {
                    // 入力回数を増やす
                    _inputCount++;

                    // タイマーを初期化
                    _groundTime = 0;
                }
                // ミノの角度が動いていたら
                if (_oldRotation.z != _playerableMino.transform.rotation.z)
                {
                    // 入力回数を増やす
                    _inputCount++;

                    // タイマーを初期化
                    _groundTime = 0;
                }
            }
            // 時間が経過したら
            if (_groundTime >= SET_UP_TIME)
            {
                // 着地音を再生
                _audioSource.PlayOneShot(_groundSound);

                // 着地するまで下に落とす
                GroundFall();

                // 置いたミノをフィールドデータに反映させる処理
                UpdateMinoToField();

                // 親オブジェ（回転軸)と子オブジェ（ミノ）の親子関係を解除する
                CutParentMino();

                // ゲームの状態をミノを消している状態に変更する
                _gameControllerScript.GameType = GameControllerScript.GameState.MINO_DELETE;

                return;
            }
            // プレイヤーのX座標を設定
            _oldPosition.x = _playerableMino.transform.position.x;

            // プレイヤーのZ角度を設定
            _oldRotation.z = _playerableMino.transform.rotation.z;
        }
        // 着地していない
        else if (!IsGround)
        {
            // タイマーを初期化
            _groundTime = 0;
        }
    }

    /// <summary>
    /// CheckCollision
    /// ミノが壁に重なっているか判定する
    /// </summary>
    /// <param name="mino">調べるミノ</param>
    /// <returns>壁に重なっているか</returns>
    public bool CheckCollision(GameObject mino)
    {
        // ミノのすべてのブロックを調べる
        foreach(Transform children in mino.GetComponentInChildren<Transform>())
        {
            // 整数化
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);
         
            // ミノがステージの範囲外だったら
            if (posX <= MIN_FIELD_WIDTH || MAX_FIELD_WIDTH <= posX || posY <= MAX_FIELD_HEIGHT)
            {
                // 重なっている
                return true;
            }
            // 置いてるミノに重なったら
            if (posY < 0 && _fieldManagerScript.FieldData[posX, -posY] != null)
            {
                // 重なっている
                return true;
            }
        }
        // 重なっていない
        return false;
    }

    /// <summary>
    /// UpdateMinoToField
    /// 置いたミノをフィールドに反映させる
    /// </summary>
    private void UpdateMinoToField()
    {
        // ミノのすべてのブロックを調べる
        foreach (Transform children in PlayerableMino.GetComponentInChildren<Transform>())
        {
            // 整数化
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);
            
            // フィールドの上にミノを置いたら 
            if(posY >= 0)
            {
                // ゲームオーバー
                _gameControllerScript.GameOverScene();
                return;
            }
            // 置いたミノをフィールドに反映させる
            _fieldManagerScript.FieldData[posX, -posY] = children.gameObject;            
        }

        // Tスピンを判定
        _tSpinCheckScript.IsTSpin = _tSpinCheckScript.TSpinCheck(PlayerableMino);
    }

    /// <summary>
    /// CutParentMino
    /// 親オブジェ（回転軸)と子オブジェ（ミノ）の親子関係を解除する
    /// </summary>
    private void CutParentMino()
    {
        // 親子関係を解除
        PlayerableMino.transform.DetachChildren();
    }

    /// <summary>
    /// GroundFall
    /// ミノを地面まで落とす
    /// </summary>
    private void GroundFall()
    {
        // ミノが壁に重なるまで
        while (!CheckCollision(PlayerableMino))
        {
            // 下に１マス移動
            PlayerableMino.transform.Translate(-Vector3.up, Space.World);
        }
        // 上に１マス戻す
        PlayerableMino.transform.Translate(Vector3.up, Space.World);
    }
}
