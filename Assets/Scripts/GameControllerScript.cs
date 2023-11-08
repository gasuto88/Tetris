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

    /// <summary>
    /// �Q�[���̏��
    /// </summary>
    public enum GameState
    {      
        MINO_CREATE,
        MINO_MOVE,
        MINO_ERASE,
        STOP        
    }

    private GameState _gameState = GameState.MINO_CREATE;

    // Next�̐擪�̃~�m�����o���X�N���v�g
    private CreateMinoScript _createMinoScript = default;

    // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����X�N���v�g
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // �v���C���[�𓮂����X�N���v�g
    private PlayerControllerScript _playerControllerScript = default;

    // �t�B�[���h���Ǘ�����X�N���v�g
    private FieldDataScript _fieldDataScript = default; 

    // �~�m���Ǘ�����X�N���v�g
    private MinoControllerScript _minoControllerScript = default;

    // �S�[�X�g�~�m�𓮂����X�N���v�g
    private GhostMinoScript _ghostMinoScript = default;

     public GameState GameType { get => _gameState; set => _gameState = value; }


    private void Start()
    {
        // MinoController���擾
        GameObject g = GameObject.Find("MinoController");

        // CreateMinoScript���擾
        _createMinoScript = g.GetComponent<CreateMinoScript>();

        // RandomSelectMinoScript���擾
        _randomSelectMinoScript = g.GetComponent<RandomSelectMinoScript>();

        // MinoControllerScript���擾
        _minoControllerScript = g.GetComponent<MinoControllerScript>();

        // PlayerControllerScript���擾
        _playerControllerScript = g.GetComponent<PlayerControllerScript>();

        // GhostMinoScript���擾
        _ghostMinoScript = g.GetComponent<GhostMinoScript>();

        // FieldDataScript���擾
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

                // Hold��������̃N�[���^�C�� 
                _minoControllerScript.HoldCount();

                
                _playerControllerScript.IsGround = false;

                // �Q�[���̏�Ԃ��~�m�𑀍�ł����ԂɕύX����
                GameType = GameState.MINO_MOVE;

                break;

            // �~�m�𑀍�ł���Ƃ�
            case GameState.MINO_MOVE:

                _playerControllerScript.PlayerController();

                if (GameType == GameState.MINO_MOVE)
                {
                    _ghostMinoScript.GhostMinoController();
                }

                break;

            // �~�m����������
            case GameState.MINO_ERASE:

                _fieldDataScript.FieldMinoErase();

                // �v���C���[�~�m������
                Destroy(_playerControllerScript.PlayerableMino);
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
    public void GameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }
}
