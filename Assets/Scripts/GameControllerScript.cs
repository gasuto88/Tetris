/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲームの状態を管理する
/// </summary>
public class GameControllerScript : MonoBehaviour
{
    // MINO_CREATE   ミノを生成している状態
    // MINO_MOVE     ミノを動かしている状態
    // MINO_DELETE   ミノを消している状態

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {      
        MINO_CREATE,
        MINO_MOVE,
        MINO_DELETE             
    }

    // ゲームの状態をミノを生成している状態に設定
    private GameState _gameState = GameState.MINO_CREATE;

    // Nextの先頭のミノを取り出すスクリプト
    private CreateMinoScript _createMinoScript = default;

    // ランダム化された7種類のミノテーブルを作成するスクリプト
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // プレイヤーを動かすスクリプト
    private PlayerControllerScript _playerControllerScript = default;

    // フィールドを管理するスクリプト
    private FieldManagerScript _fieldManagerScript = default; 

    // ミノを管理するスクリプト
    private MinoControllerScript _minoControllerScript = default;

    // ゴーストミノを動かすスクリプト
    private GhostMinoScript _ghostMinoScript = default;

    // ゲームの状態
     public GameState GameType { get => _gameState; set => _gameState = value; }

    /// <summary>
    /// 更新前処理
    /// </summary>
    private void Start()
    {
        // MinoControllerを取得
        GameObject g = GameObject.Find("MinoController");

        // CreateMinoScriptを取得
        _createMinoScript = g.GetComponent<CreateMinoScript>();

        // RandomSelectMinoScriptを取得
        _randomSelectMinoScript = g.GetComponent<RandomSelectMinoScript>();

        // MinoControllerScriptを取得
        _minoControllerScript = g.GetComponent<MinoControllerScript>();

        // PlayerControllerScriptを取得
        _playerControllerScript = g.GetComponent<PlayerControllerScript>();

        // GhostMinoScriptを取得
        _ghostMinoScript = g.GetComponent<GhostMinoScript>();

        // FieldDataScriptを取得
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }
    /// <summary>
    /// <para>更新処理</para>
    /// </summary>
    private void Update()
    {
        GameController();       
    }

    /// <summary>
    /// GameController
    /// ゲームの状態を管理
    /// </summary>
    public void GameController()
    {
        switch (GameType)
        {
            // ミノを生成している状態
            case GameState.MINO_CREATE:

                // ランダムのミノテーブルを作成する
                _randomSelectMinoScript.RandomSelectMino();
            　　
                // Nextの先頭のミノを取り出す
                _createMinoScript.FetchNextMino();

                // Nextのミノを表示する
                _minoControllerScript.NextDisplay();

                // Holdのクールタイムを減らす
                _minoControllerScript.DownCoolTime();

                // 着地判定を初期化
                _playerControllerScript.IsGround = false;

                // ゲームの状態をミノを操作できる状態に変更する
                GameType = GameState.MINO_MOVE;

                break;

            // ミノを動かしている状態
            case GameState.MINO_MOVE:

                // プレイヤーを動かす
                _playerControllerScript.PlayerController();

                // ゲームの状態がミノを動かしている状態だったら
                if (GameType == GameState.MINO_MOVE)
                {
                    // ゴーストミノを動かす
                    _ghostMinoScript.GhostMinoMove();
                }

                break;

            // ミノを消している状態
            case GameState.MINO_DELETE:

                // フィールドのミノを消す
                _fieldManagerScript.DeleteMino();

                // プレイヤーミノを消す
                Destroy(_playerControllerScript.PlayerableMino);

                // ゴーストミノを消す
                Destroy(_ghostMinoScript.GhostMino);

                // ゲーム状態をミノを生成している状態に変更する
                GameType = GameState.MINO_CREATE;

                break;
        }
    }
    /// <summary>
    /// GameOverScene
    /// ゲームオーバーシーンに遷移
    /// </summary>
    public void GameOverScene()
    {
        // ゲームオーバーシーンに遷移する
        SceneManager.LoadScene("GameOverScene");
    }
}
