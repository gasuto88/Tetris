using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*----------------------------------------------------------

更新日　11月8日　18時

制作者　本木　大地
----------------------------------------------------------*/
public class PlayerControllerScript : MonoBehaviour
{
    #region フィールド変数

    // 左右入力
    private float _horizontalInput = default;

    // 上下入力
    private float _verticalInput = default;

    // 操作できるミノ
    private GameObject _playerableMino = default;

    private readonly Vector3 _vectorRight = Vector3.right;

    private readonly Vector3 _vectorUp = Vector3.up;

    private readonly Vector3 _vectorForward = Vector3.forward;

    // プレイヤーが地面に着いたときの座標
    private Vector3 _groundPosition = default;

    // 入力されてからの経過時間
    private float _inputTime = 0f;

    [SerializeField, Header("入力時のクールタイム")]
    private float _inputCoolTime = 0.1f;

    // 左右入力（回転）
    private const int INPUT_LEFT = -1;
    private const int INPUT_RIGHT = 1;

    // プレイヤーが落下してからの経過時間
    private float _fallTime = 0f;

    [SerializeField,Header("ミノが落ちる時間")]
    private float _fallCoolTime = 0.7f;

    // プレイヤーが着地してからの経過時間
    private float _groundTime = 0f;

    // プレイヤーが地面にいるかどうか
    private bool isGround = false;

    [SerializeField,Header("ミノが死ぬまでの時間")]
    private float _deathTime = 0.3f;

    [SerializeField, Header("ミノが死ぬまでの時間（最大）")]
    private float _maxDeathTime = 1.2f;

    [SerializeField,Header("ミノが死ぬまでの時間（最小）")]
    private float _minDeathTime = 0.3f;

    [SerializeField,Header("ミノの死ぬまでの追加時間")]
    private float _addDeathTime = 0.15f;

    // 効果音-------------------------------------

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

    // -------------------------------------------

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
    // 操作できるミノ
    public GameObject PlayerableMino { get => _playerableMino; set => _playerableMino = value; }

    // 着地判定
    public bool IsGround { get => isGround; set => isGround = value; }

    #endregion

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
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
    /// <para>PlayerController</para>
    /// <para>プレイヤーの挙動を制御する</para>
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
        // 上入力されたとき
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            // ミノを地面まで落とす
            HardDrop();
        }
        // 下入力されたとき
        if (_verticalInput < 0)
        {
            // 下に１マス移動する
            SoftDrop();
        }

        // プレイヤーの自動落下
        if ((Time.time - _fallTime) > _fallCoolTime)
        {
            // 下に１マス移動
            PlayerableMino.transform.Translate(0f, -1f, 0f,Space.World);

            _fallTime = Time.time;

            // プレイヤーが壁に重なったら
            if (BeforeMoving(PlayerableMino))
            {
                // １マス戻す
                PlayerableMino.transform.Translate(0f, 1f, 0f, Space.World);

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
                _groundPosition.y = _playerableMino.transform.position.y;
            }

            //タイマー加算
            _groundTime += Time.deltaTime;

            _deathTime = Mathf.Clamp(_deathTime,_minDeathTime,_maxDeathTime);

            // プレイヤーの現在の座標が着地した座標よりも１以上低かったら
            if (_groundPosition.y - _playerableMino.transform.position.y >= 1)
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
                UpdateMinoToField();

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
            _superRotationScript.SuperRotation(PlayerableMino, INPUT_LEFT);

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
            _superRotationScript.SuperRotation(PlayerableMino, INPUT_RIGHT);

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
            _minoControllerScript.HoldController(PlayerableMino);          
        }
    }
    /// <summary>
    /// <para>HorizontalMove</para>
    /// <para>左右に移動する</para>
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
            PlayerableMino.transform.Translate(_vectorRight * input, Space.World);

            // 現在の時間を設定
            _inputTime = Time.time;

            // 壁と重なったら
            if (BeforeMoving(PlayerableMino))
            {
                // １マス戻す
                PlayerableMino.transform.Translate(_vectorRight * -input, Space.World);
            }
        }     
    }
    /// <summary>
    /// <para>HardDrop</para>
    /// <para>ミノを地面まで落とす</para>
    /// </summary>
    private void HardDrop()
    {
        // 着地音
        _audioSource.PlayOneShot(_groundSound);

        // 着地するまで下に落とす
        GroundFall();

        // 置いたミノをフィールドに反映させる処理
        UpdateMinoToField();

        // 親オブジェ（回転軸)と子オブジェ（ミノ）の親子関係を解除する
        CutParentMino();

        // ゲームの状態を置いたミノを消す処理に切り替える
        _gameControllerScript.GameType = GameControllerScript.GameState.MINO_ERASE;
    }
    /// <summary>
    /// <para>SoftDrop</para>
    /// <para>下に１マス移動する</para>
    /// </summary>
    private void SoftDrop()
    {
        // 時間が経過したら
        if ((Time.time - _inputTime) > _inputCoolTime)
        {
            // 下に１マス移動
            PlayerableMino.transform.Translate(0f, -1f, 0f, Space.World);

            // 現在の時間を設定
            _inputTime = Time.time;

            // 壁に重なったら
            if (BeforeMoving(PlayerableMino))
            {
                // 上に１マス移動
                PlayerableMino.transform.Translate(0f, 1f, 0f, Space.World);

                // 着地判定を設定
                IsGround = true;
            }
        }
    }

    /// <summary>
    /// プレイヤーが壁に重なっているかを調べる処理
    /// </summary>
    /// <param name="mino">調べるミノ</param>
    /// <returns>プレイヤーが壁に重なっているか</returns>
    public bool BeforeMoving(GameObject mino)
    {
        // ミノのすべてのブロックを調べる
        foreach(Transform children in mino.GetComponentInChildren<Transform>())
        {
            // 整数化
            int posX = Mathf.RoundToInt(children.transform.position.x);
            int posY = Mathf.RoundToInt(children.transform.position.y);
         
            // ミノがステージの範囲外だったら
            if (posX <= -1 || 10 <= posX || posY <= -20)
            {
                return true;
            }
            // 置いてるミノに重なったら
            if (posY < 0 && _fieldDataScript.FieldData[posX, -posY] != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 置いたミノをフィールドに反映させる処理
    /// </summary>
    private void UpdateMinoToField()
    {
        // ミノのすべてのブロックを調べる
        foreach (Transform _children in PlayerableMino.GetComponentInChildren<Transform>())
        {
            // 整数化
            int _posX = Mathf.RoundToInt(_children.transform.position.x);
            int _posY = Mathf.RoundToInt(_children.transform.position.y);
            
            // フィールドの上にミノを置いたら 
            if(_posY >= 0)
            {
                // ゲームオーバー
                _gameControllerScript.GameOverScene();
                return;
            }
            // 置いたミノをフィールドデータに反映させる
            _fieldDataScript.FieldData[_posX, -_posY] = _children.gameObject;

            // フィールドデータに置いたミノの情報を送る
            _fieldDataScript.SetPlayerPosition(PlayerableMino);
        }
    }
    /// <summary>
    /// 親オブジェクト（回転軸)と子オブジェクト（ミノ×４）を切り離す処理
    /// </summary>
    private void CutParentMino()
    {
        // 親についてる子オブジェクトを切り離す
        PlayerableMino.transform.DetachChildren();
    }
    /// <summary>
    /// ミノを着地するまで落とす処理
    /// </summary>
    private void GroundFall()
    {
        // プレイヤーが着地するまで
        while (!BeforeMoving(PlayerableMino))
        {
            // 下に１マス移動
            PlayerableMino.transform.Translate(0f, -1f, 0f, Space.World);
        }
        // １マス戻す
        PlayerableMino.transform.Translate(0f, 1f, 0f, Space.World);
    }
}
