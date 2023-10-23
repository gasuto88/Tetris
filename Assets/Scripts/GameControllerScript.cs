using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameControllerScript : MonoBehaviour,IGameController
{

    // START         ゲームが開始される前の処理
    // MINO_CREATE   ミノが生成された瞬間
    // MINO_MOVE     プレイヤーがミノを動かせる状態
    // STOP          ゲームが一時停止されている状態
    // END　　　　　 ゲームが終わったときの処理

    private enum GameState
    {
        START,     
        MINO_CREATE,
        MINO_MOVE,
        STOP,
        END
    }

    public delegate void GameTypeChangeMethod();

    public GameTypeChangeMethod gameTypeChangeMethod = default;

    private ICreateMino _iCreateMino = default;

    private IRandomSelectMino _iRandomSelectMino = default;

    private PlayerControllerScript _playerInputScript = default;

    private GameState _gameState = GameState.START;

    private GameObject _playerMino = default;

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

    private void Start()
    {
        GameObject g = GameObject.Find("MinoController");

        _iCreateMino = g.GetComponent<CreateMinoScript>();

        _iRandomSelectMino = g.GetComponent<RandomSelectMinoScript>();        

        _playerInputScript = GetComponent<PlayerControllerScript>();

        gameTypeChangeMethod = () => { _gameState = GameState.MINO_CREATE; };
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
        switch (_gameState)
        {
            case GameState.START:

                _iRandomSelectMino.RandomSelectMino();

                _gameState = GameState.MINO_CREATE;

                break;
            // ミノが生成されたとき
            case GameState.MINO_CREATE:

                _iCreateMino.NextMinoInstance();

                _gameState = GameState.MINO_MOVE;

                break;
            // ミノが操作できるとき
            case GameState.MINO_MOVE:

                _playerInputScript.PlayerController(PlayerMino , gameTypeChangeMethod);
                break;

            case GameState.STOP:
                break;

            case GameState.END:
                break;

        }
    }
}
