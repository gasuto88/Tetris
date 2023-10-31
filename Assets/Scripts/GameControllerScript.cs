using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControllerScript : MonoBehaviour
{

    // START         �Q�[�����J�n�����O�̏���
    // MINO_CREATE   �~�m���������ꂽ�u��
    // MINO_MOVE     �v���C���[���~�m�𓮂�������
    // STOP          �Q�[�����ꎞ��~����Ă�����
    // END�@�@�@�@�@ �Q�[�����I������Ƃ��̏���

    public enum GameState
    {
        START,     
        MINO_CREATE,
        MINO_MOVE,
        MINO_ERASE,
        STOP,
        END
    }
    public delegate void MinoEraseChangeMethod();

    public MinoEraseChangeMethod _minoEraseMethod = default;

    public delegate void MinoCreateMethod();

    public MinoCreateMethod _minoCreateMethod = default;

    private ICreateMino _iCreateMino = default;

    private IRandomSelectMino _iRandomSelectMino = default;

    private PlayerControllerScript _playerInputScript = default;

    private FieldDataScript _fieldDataScript = default;

    private GameState _gameState = GameState.START;

    private MinoControllerScript _minoControllerScript = default;

    private GhostMinoScript _ghostMinoScript = default;

     public GameState GameType { get => _gameState; set => _gameState = value; }


    private void Start()
    {
        GameObject g = GameObject.Find("MinoController");

        _iCreateMino = g.GetComponent<CreateMinoScript>();

        _iRandomSelectMino = g.GetComponent<RandomSelectMinoScript>();

        _minoControllerScript = g.GetComponent<MinoControllerScript>();

        _playerInputScript = g.GetComponent<PlayerControllerScript>();

        _ghostMinoScript = g.GetComponent<GhostMinoScript>();

        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();

        _minoEraseMethod = () => { GameType = GameState.MINO_ERASE; };

        _minoCreateMethod = () => {GameType = GameState.MINO_CREATE; };
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
        switch (GameType)
        {
            case GameState.START:

                _iRandomSelectMino.RandomSelectMino();

                GameType = GameState.MINO_CREATE;

                break;
            // �~�m���������ꂽ�Ƃ�
            case GameState.MINO_CREATE:

                _iCreateMino.NextMinoInstance();

                _minoControllerScript.NextDisplay();

                _minoControllerScript.HoldCount();              

                GameType = GameState.MINO_MOVE;

                break;
            // �~�m������ł���Ƃ�
            case GameState.MINO_MOVE:

                _playerInputScript.PlayerController(_minoEraseMethod,_minoCreateMethod);

                if (GameType == GameState.MINO_MOVE)
                {
                    _ghostMinoScript.GhostController();
                }

                break;
            
            case GameState.MINO_ERASE:

                _fieldDataScript.FieldMinoErase();

                Destroy(_ghostMinoScript.GhostMino);

                GameType = GameState.MINO_CREATE;

                break;
            case GameState.STOP:

                break;

            case GameState.END:

                //Debug.LogError("�V�[���@��");
                //SceneManager.LoadScene("GameOverScene");

                break;

        }
    }
}
