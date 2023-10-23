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
            // �~�m���������ꂽ�Ƃ�
            case GameState.MINO_CREATE:

                _iCreateMino.NextMinoInstance();

                _gameState = GameState.MINO_MOVE;

                break;
            // �~�m������ł���Ƃ�
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
