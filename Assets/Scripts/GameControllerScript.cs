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

    public GameObject PlayerMino { get => _playerMino; set => _playerMino = value; }

    private void Start()
    {
        GameObject g = GameObject.Find("MinoController");

        _iCreateMino = g.GetComponent<CreateMinoScript>();

        _iRandomSelectMino = g.GetComponent<RandomSelectMinoScript>();

        _instanceMinoTransform = GameObject.Find("InstanceMinoPosition").transform;
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

                if (_horizontalInput > 0)
                {
                    PlayerMino.transform.Translate(1f, 0f, 0f);
                }
                else if (_horizontalInput < 0)
                {
                    PlayerMino.transform.Translate(-1f, 0f, 0f);
                }
                if(_verticalInput > 0)
                {
                    
                }
                else if(_verticalInput < 0)
                {
                    PlayerMino.transform.Translate(0f, -1f, 0f);
                }

                break;

            case GameState.STOP:
                break;

            case GameState.END:
                break;

        }
    }
}
