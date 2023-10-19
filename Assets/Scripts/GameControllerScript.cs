using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameControllerScript : MonoBehaviour,IGameController
{

    // START         �Q�[�����J�n�����O�̏���
    // MINO_CREATE   �~�m���������ꂽ�u��
    // MINO_MOVE     �v���C���[���~�m�𓮂�������
    // STOP          �Q�[�����ꎞ��~����Ă�����
    // END�@�@�@�@�@ �Q�[�����I������Ƃ��̏���

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

    [SerializeField,Header("���̓N�[���^�C��")]
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
    /// �Q�[���̏�Ԃ��Ǘ�
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
