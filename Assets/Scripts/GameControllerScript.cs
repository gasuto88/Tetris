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

    private ICreateMino _iCreateMino = default;

    private IRandomSelectMino _iRandomSelectMino = default;

    private GameState _gameState = GameState.START;

    private Transform _instanceMinoTransform = default;

    private GameObject _playerMino = default;

    private Vector2 _playerPosition = default;

    private bool isInput = default;

    [SerializeField,Header("入力クールタイム")]
    private float _inputCoolTime = 0.1f;

    private float _inputTime = default;

    private float _fallTime = default;

    private float _fallCoolTime = 1f;

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

    private void Start()
    {
        GameObject g = GameObject.Find("MinoController");

        _iCreateMino = g.GetComponent<CreateMinoScript>();

        _iRandomSelectMino = g.GetComponent<RandomSelectMinoScript>();

        _instanceMinoTransform = GameObject.Find("InstanceMinoPosition").transform;

        _playerPosition = _instanceMinoTransform.position;
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

            case GameState.MINO_CREATE:

                _iCreateMino.NextMinoInstance();

                _gameState = GameState.MINO_MOVE;

                break;

            case GameState.MINO_MOVE:

                float _horizontalInput = Input.GetAxisRaw("Horizontal");
                float _verticalInput = Input.GetAxisRaw("Vertical");

                if (_horizontalInput > 0 && (Time.time - _inputTime) > _inputCoolTime)
                {
                    _playerPosition += Vector2.right;

                    _inputTime = Time.time;
                }
                else if (_horizontalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
                {
                    _playerPosition += Vector2.left;

                    _inputTime = Time.time;
                }

                if(_verticalInput > 0)
                {
                    
                }
                else if(_verticalInput < 0 && (Time.time - _inputTime) > _inputCoolTime)
                {
                    _playerPosition += Vector2.down;

                    _inputTime = Time.time;
                }

                if ((Time.time - _fallTime) > _fallCoolTime) 
                {
                    _playerPosition += Vector2.down;

                    _fallTime = Time.time;
                }
                PlayerMino.transform.position = _playerPosition;
                break;

            case GameState.STOP:
                break;

            case GameState.END:
                break;

        }
    }
}
