/*----------------------------------------------------
 * FieldManagerScript.cs
 * �t�B�[���h���Ǘ�����
 * 
 * �쐬���@11��10��
 * 
 * ����ҁ@�{�؁@��n
 ---------------------------------------------------*/
using UnityEngine;

public class FieldManagerScript : MonoBehaviour
{
    // �X�R�A��\������X�N���v�g
    private ScoreScript _scoreScript = default;

    // �t�B�[���h�f�[�^
    private GameObject[,] _fieldData = new GameObject[10,20];

    // �t�B�[���h�̍���
    private const int HEIGHT = 19;

    //private const int

    // ��������̖��O -------------------------------
    private const string TETRIS = "TETRIS";
    private const string TSINGLE = "TSPIN - SINGLE";
    private const string TDOUBLE = "TSPIN - DOUBLE";
    private const string TTRIPLE = "TSPIN - TRIPLE";
    // ----------------------------------------------

    // �������i�� -----------------------------------
    private const int DELETE_ONE = 1;
    private const int DELETE_TWO = 2;
    private const int DELETE_THREE = 3;
    // ----------------------------------------------

    // T�X�s�����ǂ���
    private bool isTspin = default;

    // T�X�s�����𒲂ׂ�X�N���v�g
    private TSpinCheckScript _tspinCheckScript = default;

    // ���삵�Ă���~�m
    private GameObject _playerMino = default;

    private AudioSource _audioSource = default;

    [SerializeField,Header("�������������Ƃ��̉�")]
    private AudioClip _eraseSound = default;

    [SerializeField, Header("��������������������Ƃ��̉�")]
    private AudioClip _manyEraseSound = default;

    //�t�B�[���h�̏��
    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    // �t�B�[���h�̍���
    public int Height { get => HEIGHT;}
    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
    private void Start()
    {
        // ScoreScript���擾
        _scoreScript = GameObject.Find("Canvas").GetComponent<ScoreScript>();

        // TSpinCheckScript���擾
        _tspinCheckScript = GameObject.Find("MinoController").GetComponent<TSpinCheckScript>();

        // AudioSource���擾
        _audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// <para>DeleteFieldMino</para>
    /// <para>�t�B�[���h�̃~�m������</para>
    /// </summary>
    public void DeleteFieldMino()
    {
        // �����̃u���b�N��
        int blockCount = 0;
        
        // �������i��
        int deleteCount = 0;

        // ���삵�Ă���~�m��T�~�m�@���@T�X�s����������
        if (_playerMino.tag == "TMino" && _tspinCheckScript.TSpinCheck(_playerMino)) 
        {
            // T�X�s�����Ă���
            isTspin = true;
        }
        else
        {
            // T�X�s�����Ă��Ȃ�
            isTspin = false;
        }

        // �t�B�[���h�̈�ԉ���������ɐi��
        for (int i = HEIGHT; i >= 0; i--)
        {
            // �t�B�[���h�̈�ԍ��������E�ɐi��
            for (int j = 0; j < 10; j++)
            {
                // �t�B�[���h�Ƀu���b�N���u���Ă�������
                if (FieldData[j, i] != null)
                {
                    // �����̃u���b�N��������₷
                    blockCount++;
                }
            }
            // �����Ƀu���b�N���S�����܂��Ă�����
            if (blockCount >= 10)
            {�@
                // �u���b�N�̖��܂���������������ɐi��
                for (int k = i; k > -1; k--)
                {
                    // �t�B�[���h�̈�ԍ��������E�ɐi��
                    for (int l = 0; l < 10; l++)
                    {
                        // ���݂̍������u���b�N�̖��܂��������Ɠ����@���@�t�B�[���h�Ƀu���b�N���u���Ă�������
                        if (k == i && FieldData[l, i] != null)
                        {
                            // �u���Ă���u���b�N���폜����
                            Destroy(FieldData[l, i].gameObject);

                            // ���̃u���b�N�̃t�B�[���h�f�[�^����ɂ���
                            FieldData[l, i] = null;

                        }
                        // ��ԏ�̒i��艺�@���@���̒i�Ƀu���b�N���u���Ă�������@
                        if (k > 0 && FieldData[l,k - 1] != null)
                        {
                            // �u���Ă���u���b�N������Ɉړ�
                            FieldData[l, k - 1].transform.Translate(0f, -1f, 0f, Space.World);

                            //�@���̒i�̃f�[�^�����݂̃f�[�^�ɏ㏑������
                            FieldData[l,k] = FieldData[l, k - 1];

                            // ���̒i�̃t�B�[���h�f�[�^����ɂ���
                            FieldData[l, k - 1] = null;
                        }
                    }
                }
                // ���ɒ��ׂ�i������ɉ�����
                i++;

                // �������i��
                deleteCount++;
            }
            // �����̃u���b�N����������
            blockCount = 0;
        }
        // 4�i��������
        if (deleteCount >= 4)
        {
            // TETRIS�i�Z�j����ʂɕ\������
            _scoreScript.ActionDisplay(TETRIS);

            // ��������������������Ƃ��̉�
            _audioSource.PlayOneShot(_manyEraseSound);
        }
        else if (deleteCount >= 1)
        {
            // �������������Ƃ��̉�
            _audioSource.PlayOneShot(_eraseSound);
        }

        // �s�X�s����������
        if (isTspin)
        {
            // �������i���ɂ���ĕ�����
            switch (deleteCount)
            {
                // 1�i
                case DELETE_ONE:

                    // TSINGLE����ʂɕ\������
                    _scoreScript.ActionDisplay(TSINGLE);

                    break;

                // 2�i
                case DELETE_TWO:

                    // TDOUBLE����ʂɕ\������
                    _scoreScript.ActionDisplay(TDOUBLE);

                    break;
                // 3�i
                case DELETE_THREE:

                    // TTRIPLE����ʂɕ\������
                    _scoreScript.ActionDisplay(TTRIPLE);

                    break;
            }
        }
        //�@�������i���𑗂�
        _scoreScript.ScoreDisplay(deleteCount);
    }

    /// <summary>
    /// <para>SetPlayerInfo</para>
    /// <para>���삵�Ă���~�m���󂯎��</para>
    /// </summary>
    /// <param name="player">���삵�Ă���~�m</param>
    public void SetPlayerInfo(GameObject player)
    {
        _playerMino = player;
    }
}
