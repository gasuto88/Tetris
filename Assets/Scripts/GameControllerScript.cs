using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameControllerScript : MonoBehaviour
{
    // MINO_CREATE   �~�m���������ꂽ�u��
    // MINO_MOVE     �v���C���[���~�m�𓮂�������
    // MINO_ERASE    �~�m�������Ă�����
    // STOP          �Q�[�����ꎞ��~����Ă�����

    public enum GameState
    {      
        MINO_CREATE,
        MINO_MOVE,
        MINO_ERASE,
        STOP        
    }

    
    private CreateMinoScript _createMinoScript = default;

    // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����X�N���v�g
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
    /// �Q�[���̏�Ԃ��Ǘ�
    /// </summary>
    public void GameController()
    {
        switch (GameType)
        {
            // �~�m���������ꂽ�Ƃ�
            case GameState.MINO_CREATE:

                // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����
                _randomSelectMinoScript.RandomSelectMino();
            �@�@
                // Next�̐擪�̃~�m�����o��
                _createMinoScript.NextMinoTakeOut();

                // Next�̃~�m��\������
                _minoControllerScript.NextDisplay();

                _minoControllerScript.HoldCount();

                _playerControllerScript.IsGround = false;

                GameType = GameState.MINO_MOVE;

                break;
            // �~�m�𑀍�ł���Ƃ�
            case GameState.MINO_MOVE:

                _playerControllerScript.PlayerController();

                if (GameType == GameState.MINO_MOVE)
                {
                    _ghostMinoScript.GhostController();
                }

                break;
            // �~�m����������
            case GameState.MINO_ERASE:

                _fieldDataScript.FieldMinoErase();

                // �v���C���[�~�m������
                Destroy(_playerControllerScript.PlayerMino);
                // �S�[�X�g�~�m������
                Destroy(_ghostMinoScript.GhostMino);
               
                GameType = GameState.MINO_CREATE;

                break;
            case GameState.STOP:

                break;
        }
    }
    /// <summary>
    /// �Q�[���I�[�o�[�V�[���ɑJ��
    /// </summary>
    public void GameOver()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
