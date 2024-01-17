/*----------------------------------------------------
  FieldManagerScript.cs
  
  �쐬���@11��10��
  
  �쐬�ҁ@�{�؁@��n
 ---------------------------------------------------*/
using UnityEngine;

/// <summary>
/// �t�B�[���h���Ǘ�����
/// </summary>
public class FieldManagerScript : MonoBehaviour
{
    #region �萔

    // �t�B�[���h�̏c���Ɖ���
    private const int HEIGHT = 19;
    private const int WIDTH = 10;

    // ��������̖��O ----------------------------------------
    private const string TETRIS = "TETRIS";
    private const string TSINGLE = "TSPIN SINGLE";
    private const string TDOUBLE = "TSPIN DOUBLE";
    private const string TTRIPLE = "TSPIN TRIPLE";
    // -------------------------------------------------------

    // �I�u�W�F�N�g�̃^�O�̖��O ------------------------------
    private const string SCORE_CANVAS = "ScoreCanvas";
    private const string MINO_CONTROLLER = "MinoController";
    private const string GAME_CONTROLLER = "GameController";
    // -------------------------------------------------------
    #endregion

    #region �t�B�[���h�ϐ�

    // �X�R�A��\������X�N���v�g
    private ScoreManagerScript _scoreScript = default;

    // T�X�s�����𒲂ׂ�X�N���v�g
    private TSpinCheckScript _tspinCheckScript = default;

    // �����Đ�����X�N���v�g
    private AudioManagerScript _audioManagerScript = default;

    // �t�B�[���h�f�[�^
    private GameObject[,] _fieldData = new GameObject[10,20];

    #endregion

    //�t�B�[���h�̏��
    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    // �t�B�[���h�̍���
    public int Height { get => HEIGHT;}

    /// <summary>
    /// �X�V�O����
    /// </summary>
    private void Start()
    {
        // ScoreScript���擾
        _scoreScript = GameObject.FindGameObjectWithTag(SCORE_CANVAS).GetComponent<ScoreManagerScript>();

        // TSpinCheckScript���擾
        _tspinCheckScript = GameObject.FindGameObjectWithTag(MINO_CONTROLLER).GetComponent<TSpinCheckScript>();

        // AudioManagerScript���擾
        _audioManagerScript = GameObject.FindGameObjectWithTag(GAME_CONTROLLER).GetComponent<AudioManagerScript>();
    }

    /// <summary>
    /// DeleteMino
    /// �t�B�[���h�̃~�m������
    /// </summary>
    public void DeleteMino()
    {       
        // �������i��
        int deleteCount = 0;

        // �t�B�[���h��1�ԉ�����1�ԏ�܂Ői��
        for (int i = HEIGHT; i >= 0; i--)
        {           
            // ��1��Ƀu���b�N���S�����܂��Ă�����
            if (CheckDeleteMino(i))
            {
                // �u���b�N�̖��܂����i����1�ԏ�܂Ői��
                for (int k = i; k >= 0; k--)
                {
                    // �t�B�[���h��1�ԍ�����1�ԉE�܂Ői��
                    for (int l = 0; l < WIDTH; l++)
                    {
                        // ���݂̍������u���b�N�̖��܂��������Ɠ����@���@�t�B�[���h�Ƀu���b�N���u���Ă�������
                        if (k == i && FieldData[l, i] != null)
                        {
                            // �u���b�N���폜����
                            Destroy(FieldData[l, i].gameObject);

                            // ���̃u���b�N�̃t�B�[���h�f�[�^��������
                            FieldData[l, i] = null;

                        }
                        // 1�ԏ�̒i��艺�@���@1��̒i�Ƀu���b�N���u���Ă�������@
                        if (k > 0 && FieldData[l, k - 1] != null)
                        {
                            // �u���b�N��1���Ɉړ�
                            FieldData[l, k - 1].transform.Translate(-Vector3.up, Space.World);

                            //�@1��̒i�̃f�[�^�����݂̃f�[�^�ɏ㏑������
                            FieldData[l, k] = FieldData[l, k - 1];

                            // 1��̒i�̃t�B�[���h�f�[�^��������
                            FieldData[l, k - 1] = null;
                        }
                    }
                }

                // ���ɒ��ׂ�i��1������
                i++;

                // �������i��
                deleteCount++;
            }
        }

        // �������i����1�ȏゾ������
        if (deleteCount >= 1)
        {
            // �\�����镶����I������
            SelectDisplayText(deleteCount);

            //�@�������i���𑗂�
            _scoreScript.ScoreDisplay(deleteCount);
        }
    }

    /// <summary>
    /// ��1��Ƀu���b�N���S�����܂��Ă��邩
    /// </summary>
    /// <param name="row">�i</param>
    /// <returns>�u���b�N���S�����܂��Ă��邩</returns>
    private bool CheckDeleteMino(int row)
    {
        // �t�B�[���h��1�ԍ�����1�ԉE�܂Ői��
        for (int j = 0; j < WIDTH; j++)
        {
            // �t�B�[���h�Ƀu���b�N���u���Ă��Ȃ�������
            if (FieldData[j, row] == null)
            {
                // �u���b�N���S�����܂��Ă��Ȃ�
                return false;
            }
        }

        // �u���b�N���S�����܂��Ă���
        return true;
    } 
    /// <summary>
    /// �\�����镶����I������
    /// </summary>
    /// <param name="deleteCount">�������i��</param>
    private void SelectDisplayText(int deleteCount)
    {
        // 4�i��������
        if (deleteCount >= 4)
        {
            // TETRIS����ʂɕ\������
            _scoreScript.ActionDisplay(TETRIS);

            // �~�m���S�i���������̉����Đ�
            _audioManagerScript.ManyDeleteSound();
        }
        // 1�i�ȏ��������
        else if (deleteCount >= 1)
        {
            // �~�m�����������̉����Đ�
            _audioManagerScript.DeleteSound();
        }

        // �s�X�s����������
        if (_tspinCheckScript.IsTSpin)
        {
            // �������i���ɂ���ĕ�����
            switch (deleteCount)
            {
                // 1�i
                case 1:
                    // TSINGLE����ʂɕ\������
                    _scoreScript.ActionDisplay(TSINGLE);
                    break;

                // 2�i
                case 2:
                    // TDOUBLE����ʂɕ\������
                    _scoreScript.ActionDisplay(TDOUBLE);
                    break;

                // 3�i
                case 3:
                    // TTRIPLE����ʂɕ\������
                    _scoreScript.ActionDisplay(TTRIPLE);
                    break;
            }
        }
    }
}
