/*----------------------------------------------------------
MinoControllerScript.cs

 更新日　11月9日

 制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;
/// <summary>
/// // ミノを管理する
/// </summary>
public class MinoControllerScript : MonoBehaviour
{
    #region フィールド変数

    // Nextの一番目の座標
    private Transform _nextPositionOne = default;
    // Nextの二番目の座標
    private Transform _nextPositionTwo = default;
    // Nextの三番目の座標
    private Transform _nextPositionThree = default;

    // Nextの座標を入れる配列
    private Transform[] _nextPositions = new Transform[4];

    // Holdのゲームオブジェクト
    private GameObject _holdObject = default;

    // Holdの座標
    private Transform _holdTransform = default;

    // Holdのゲームオブジェクト（ゴーストミノ用）
    private GameObject _holdGhostObject = default;

    // Holdをした後のクールタイム
    private int _holdCoolTime = default;

    // ランダム化された7種類のミノテーブルを作成するスクリプト
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // ゲームの状態を管理するスクリプト
    private GameControllerScript _gameControllerScript = default;

    // ゴーストミノを動かすスクリプト
    private GhostMinoScript _ghostMinoScript = default;

    // ミノを保管する座標
    private Transform _minoStorageTransform = default;

    private AudioSource _audioSource = default;

    #endregion

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // GameControllerScriptを取得
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // RandomSelectMinoScriptを取得
        _randomSelectMinoScript = GetComponent<RandomSelectMinoScript>();

        // Nextの座標を取得
        _nextPositionOne = GameObject.Find("NextPosition (1)").transform;
        _nextPositionTwo = GameObject.Find("NextPosition (2)").transform;
        _nextPositionThree = GameObject.Find("NextPosition (3)").transform;

        // Nextの座標を配列に入れる
        _nextPositions = new Transform[] { _nextPositionOne, _nextPositionTwo, _nextPositionThree};

        // GhostMinoScriptを取得
        _ghostMinoScript = GetComponent<GhostMinoScript>();

        // Holdの座標を取得
        _holdTransform = GameObject.Find("HoldPosition").transform;

        // ミノを保管する座標を取得
        _minoStorageTransform = GameObject.Find("MinoStoragePosition").transform;

        // AudioSourceを取得
        _audioSource = GetComponent<AudioSource>();

    }
    /// <summary>
    /// 更新処理
    /// </summary>
    private void Update()
    {
        AddMino();       
    }
    /// <summary>
    /// AddMino
    /// リストのミノの個数を増やす
    /// </summary>
    private void AddMino()
    {    
        // リストのミノが7個以下になったら
        if(_randomSelectMinoScript.MinoList.Count <= 7)
        {
            // ランダムのミノテーブルを作成する
            _randomSelectMinoScript.RandomSelectMino();
        }
    }
    /// <summary>
    /// NextDisplay
    /// NEXTミノの表示
    /// </summary>
    public void NextDisplay()
    {
        // 3つのNextにミノを移動させる
        for (int i = 0; i < 3; i++)
        {
            _randomSelectMinoScript.MinoList[i].transform.position = _nextPositions[i].transform.position;
        }
    }
    /// <summary>
    /// HoldController
    /// Holdの中身を出し入れする処理
    /// </summary>
    /// <param name="playerableMino">操作できるミノ</param>
    public void HoldController(GameObject playerableMino,AudioClip holdSound)
    {
        // クールタイムが0だったら
        if (_holdCoolTime <= 0)
        {
            // Hold音を再生
            _audioSource.PlayOneShot(holdSound);

            // クールタイムを設定 
            _holdCoolTime = 2;

            // Holdにミノが入っていたら
            if (_holdObject != null)
            {
                // Holdに入ってるミノをリストの先頭に追加
                _randomSelectMinoScript.MinoList.Insert(0, _holdObject);
                _randomSelectMinoScript.GhostList.Insert(0, _holdGhostObject);
            }

            // 操作できるミノを設定
            _holdObject = playerableMino;

            // ゴーストミノを設定
            _holdGhostObject = _ghostMinoScript.GhostMino;

            // Holdに入ってるミノをHoldの座標に移動する
            _holdObject.transform.position = _holdTransform.position;         

            // ゴーストミノを保管する座標に設定する
            _holdGhostObject.transform.position = _minoStorageTransform.position;

            // Holdのミノの角度を初期化
            _holdObject.transform.rotation = default;
            _holdGhostObject.transform.rotation = default;

            // ゲーム状態をミノを生成している状態に変更する
            _gameControllerScript.GameType = GameControllerScript.GameState.MINO_CREATE;
        }

        

    }
    /// <summary>
    /// DownCoolTime
    /// Holdのクールタイムを減らす
    /// </summary>
    public void DownCoolTime()
    {
        _holdCoolTime--;
    }
}
