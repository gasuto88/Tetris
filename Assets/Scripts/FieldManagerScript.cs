/*----------------------------------------------------
 * 
 * �X�V���@11��9��
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
    private int _height = 19;

    // �Z�̖��O--------------------------------------
    private const string TETRIS = "TETRIS";
    private const string TSINGLE = "TSPIN - SINGLE";
    private const string TDOUBLE = "TSPIN - DOUBLE";
    private const string TTRIPLE = "TSPIN - TRIPLE";
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
    public int Height { get => _height;}
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
        int eraseCount = 0;
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
        for (int i = 19; i > -1; i--)
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
                eraseCount++;
            }
            // �����̃u���b�N����������
            blockCount = 0;
        }
        // 4�i��������
        if (eraseCount >= 4)
        {
            // TETRIS�i�Z�j����ʂɕ\������
            _scoreScript.ActionDisplay(TETRIS);

            // ��������������������Ƃ��̉�
            _audioSource.PlayOneShot(_manyEraseSound);
        }
        else if (eraseCount >= 1)
        {
            // �������������Ƃ��̉�
            _audioSource.PlayOneShot(_eraseSound);
        }

        // �s�X�s����������
        if (isTspin)
        {  
            // �������i������i��������
            if (eraseCount == 1)
            {
                // TSINGLE�i�Z�j����ʂɕ\������
                _scoreScript.ActionDisplay(TSINGLE);
            }
            // �������i������i��������
            else if (eraseCount == 2)
            {
                // TDOUBLE�i�Z�j����ʂɕ\������
                _scoreScript.ActionDisplay(TDOUBLE);
            }
            // �������i�����O�i��������
            else if (eraseCount == 3)
            {
                // TTRIPLE�i�Z�j����ʂɕ\������
                _scoreScript.ActionDisplay(TTRIPLE);
            }
        }
        //�@�������i���𑗂�
        _scoreScript.ScoreDisplay(eraseCount);
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
