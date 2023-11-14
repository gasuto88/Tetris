/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// �Q�[���̏�Ԃ��Ǘ�����
/// </summary>
public class GameControllerScript : MonoBehaviour
{
    // MINO_CREATE   �~�m�𐶐����Ă�����
    // MINO_MOVE     �~�m�𓮂����Ă�����
    // MINO_DELETE   �~�m�������Ă�����

    /// <summary>
    /// �Q�[���̏��
    /// </summary>
    public enum GameState
    {      
        MINO_CREATE,
        MINO_MOVE,
        MINO_DELETE             
    }

    // �Q�[���̏�Ԃ��~�m�𐶐����Ă����Ԃɐݒ�
    private GameState _gameState = GameState.MINO_CREATE;

    // Next�̐擪�̃~�m�����o���X�N���v�g
    private CreateMinoScript _createMinoScript = default;

    // �����_�������ꂽ7��ނ̃~�m�e�[�u�����쐬����X�N���v�g
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // �v���C���[�𓮂����X�N���v�g
    private PlayerControllerScript _playerControllerScript = default;

    // �t�B�[���h���Ǘ�����X�N���v�g
    private FieldManagerScript _fieldManagerScript = default; 

    // �~�m���Ǘ�����X�N���v�g
    private MinoControllerScript _minoControllerScript = default;

    // �S�[�X�g�~�m�𓮂����X�N���v�g
    private GhostMinoScript _ghostMinoScript = default;

    // �Q�[���̏��
     public GameState GameType { get => _gameState; set => _gameState = value; }

    /// <summary>
    /// �X�V�O����
    /// </summary>
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
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }
    /// <summary>
    /// <para>�X�V����</para>
    /// </summary>
    private void Update()
    {
        GameController();       
    }

    /// <summary>
    /// GameController
    /// �Q�[���̏�Ԃ��Ǘ�
    /// </summary>
    public void GameController()
    {
        switch (GameType)
        {
            // �~�m�𐶐����Ă�����
            case GameState.MINO_CREATE:

                // �����_���̃~�m�e�[�u�����쐬����
                _randomSelectMinoScript.RandomSelectMino();
            �@�@
                // Next�̐擪�̃~�m�����o��
                _createMinoScript.FetchNextMino();

                // Next�̃~�m��\������
                _minoControllerScript.NextDisplay();

                // Hold�̃N�[���^�C�������炷
                _minoControllerScript.DownCoolTime();

                // ���n�����������
                _playerControllerScript.IsGround = false;

                // �Q�[���̏�Ԃ��~�m�𑀍�ł����ԂɕύX����
                GameType = GameState.MINO_MOVE;

                break;

            // �~�m�𓮂����Ă�����
            case GameState.MINO_MOVE:

                // �v���C���[�𓮂���
                _playerControllerScript.PlayerController();

                // �Q�[���̏�Ԃ��~�m�𓮂����Ă����Ԃ�������
                if (GameType == GameState.MINO_MOVE)
                {
                    // �S�[�X�g�~�m�𓮂���
                    _ghostMinoScript.GhostMinoMove();
                }

                break;

            // �~�m�������Ă�����
            case GameState.MINO_DELETE:

                // �t�B�[���h�̃~�m������
                _fieldManagerScript.DeleteMino();

                // �v���C���[�~�m������
                Destroy(_playerControllerScript.PlayerableMino);

                // �S�[�X�g�~�m������
                Destroy(_ghostMinoScript.GhostMino);

                // �Q�[����Ԃ��~�m�𐶐����Ă����ԂɕύX����
                GameType = GameState.MINO_CREATE;

                break;
        }
    }
    /// <summary>
    /// GameOverScene
    /// �Q�[���I�[�o�[�V�[���ɑJ��
    /// </summary>
    public void GameOverScene()
    {
        // �Q�[���I�[�o�[�V�[���ɑJ�ڂ���
        SceneManager.LoadScene("GameOverScene");
    }
}
