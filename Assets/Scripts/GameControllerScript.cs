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

    public enum GameState
    {      
        MINO_CREATE,
        MINO_MOVE,
        MINO_ERASE,
        STOP        
    }

    
    private CreateMinoScript _createMinoScript = default;

    // ランダム化された7種類のミノテーブルを作成するスクリプト
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    private PlayerControllerScript _playerControllerScript = default;

    private FieldDataScript _fieldDataScript = default;

    private GameState _gameState = GameState.MINO_CREATE;

    private MinoControllerScript _minoControllerScript = default;

    private GhostMinoScript _ghostMinoScript = default;

     public GameState GameType { get => _gameState; set => _gameState = value; }


    private void Start()
    {
        GameObject g = GameObject.Find("MinoController");

        _createMinoScript = g.GetComponent<CreateMinoScript>();

        _randomSelectMinoScript = g.GetComponent<RandomSelectMinoScript>();

        _minoControllerScript = g.GetComponent<MinoControllerScript>();

        _playerControllerScript = g.GetComponent<PlayerControllerScript>();

        _ghostMinoScript = g.GetComponent<GhostMinoScript>();

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

                _minoControllerScript.HoldCount();

                _playerControllerScript.IsGround = false;

                GameType = GameState.MINO_MOVE;

                break;
            // ミノを操作できるとき
            case GameState.MINO_MOVE:

                _playerControllerScript.PlayerController();

                if (GameType == GameState.MINO_MOVE)
                {
                    _ghostMinoScript.GhostController();
                }

                break;
            // ミノを消す処理
            case GameState.MINO_ERASE:

                _fieldDataScript.FieldMinoErase();

                // プレイヤーミノを消す
                Destroy(_playerControllerScript.PlayerMino);
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
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
