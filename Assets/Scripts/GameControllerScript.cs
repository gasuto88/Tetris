using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControllerScript : MonoBehaviour
{
    // MINO_CREATE   ミノが生成された瞬間
    // MINO_MOVE     プレイヤーがミノを動かせる状態
    // MINO_ERASE    ミノを消している状態
    // STOP          ゲームが一時停止されている状態

    /// <summary>
    /// ゲームの状態
    /// </summary>
    public enum GameState
    {      
        MINO_CREATE,
        MINO_MOVE,
        MINO_ERASE,
        STOP        
    }

    private GameState _gameState = GameState.MINO_CREATE;

    // Nextの先頭のミノを取り出すスクリプト
    private CreateMinoScript _createMinoScript = default;

    // ランダム化された7種類のミノテーブルを作成するスクリプト
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // プレイヤーを動かすスクリプト
    private PlayerControllerScript _playerControllerScript = default;

    // フィールドを管理するスクリプト
    private FieldDataScript _fieldDataScript = default; 

    // ミノを管理するスクリプト
    private MinoControllerScript _minoControllerScript = default;

    // ゴーストミノを動かすスクリプト
    private GhostMinoScript _ghostMinoScript = default;

     public GameState GameType { get => _gameState; set => _gameState = value; }


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
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
    }

    private void Update()
    {
        GameController();       
    }

    /// <summary>
    /// ゲームの状態を管理
    /// </summary>
    public void GameController()
    {
        switch (GameType)
        {
            // ミノが生成されたとき
            case GameState.MINO_CREATE:

                // ランダム化された7種類のミノテーブルを作成する
                _randomSelectMinoScript.RandomSelectMino();
            　　
                // Nextの先頭のミノを取り出す
                _createMinoScript.NextMinoTakeOut();

                // Nextのミノを表示する
                _minoControllerScript.NextDisplay();

                // Holdをした後のクールタイム 
                _minoControllerScript.HoldCount();

                
                _playerControllerScript.IsGround = false;

                // ゲームの状態をミノを操作できる状態に変更する
                GameType = GameState.MINO_MOVE;

                break;

            // ミノを操作できるとき
            case GameState.MINO_MOVE:

                _playerControllerScript.PlayerController();

                if (GameType == GameState.MINO_MOVE)
                {
                    _ghostMinoScript.GhostMinoController();
                }

                break;

            // ミノを消す処理
            case GameState.MINO_ERASE:

                _fieldDataScript.FieldMinoErase();

                // プレイヤーミノを消す
                Destroy(_playerControllerScript.PlayerableMino);
                // ゴーストミノを消す
                Destroy(_ghostMinoScript.GhostMino);
               
                GameType = GameState.MINO_CREATE;

                break;

            case GameState.STOP:

                break;
        }
    }
    /// <summary>
    /// ゲームオーバーシーンに遷移
    /// </summary>
    public void GameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
