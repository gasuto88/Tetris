/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

public class RandomSelectMinoScript : MonoBehaviour
{
    // �e�g���X�~�m�̃v���n�u--------------
    [SerializeField]
    private GameObject _iMino = default;

    [SerializeField]
    private GameObject _oMino = default;

    [SerializeField]
    private GameObject _sMino = default;

    [SerializeField]
    private GameObject _zMino = default;

    [SerializeField]
    private GameObject _jMino = default;

    [SerializeField]
    private GameObject _lMino = default;

    [SerializeField]
    private GameObject _tMino = default;

    // -------------------------------------
    private int _randomNumber = default;

    private int _selectNumber = default;

    // �~�m��ۊǂ�����W
    private Transform _minoStorageTransform = default;

    /// <summary>
    /// <para>�e�g���X�~�m�̃e�[�u��</para>
    /// </summary>
    private enum MinoTable
    {
        IMINO,
        OMINO,
        SMINO,
        ZMINO,
        JMINO,
        LMINO,
        TMINO
    }

    // 7��ނ̃~�m���i�[
    private MinoTable[] _minoTable = {
        MinoTable.IMINO, MinoTable.OMINO, MinoTable.SMINO,
        MinoTable.ZMINO, MinoTable.JMINO, MinoTable.LMINO, MinoTable.TMINO };

    // �~�m���o�Ă��鏇�Ԃ��i�[����
    List<GameObject> _minoList = new List<GameObject>();

    List<int> _numberList = new List<int>();

    // �S�[�X�g�~�m���o�Ă��鏇�Ԃ��i�[����
    List<GameObject> _ghostList = new List<GameObject>();

    // �~�m���o�Ă��鏇�Ԃ��i�[����
    public List<GameObject> MinoList { get => _minoList; set => _minoList = value; }
    // �S�[�X�g�~�m���o�Ă��鏇�Ԃ��i�[����
    public List<GameObject> GhostList { get => _ghostList; set => _ghostList = value; }

    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
    private void Start()
    {
        // �~�m��ۊǂ�����W���擾
        _minoStorageTransform = GameObject.Find("MinoStoragePosition").transform;
    }

    /// <summary>
    /// <para>RandomSelectMino</para>
    /// <para>�V��ނ̃~�m���d���Ȃ����X�g�ɓ����</para>
    /// </summary>
    public void RandomSelectMino()
    {
        // ���X�g�̒���0�`7�̐�����ǉ�����
        for (int i = 0; i < 7; i++)
        {
            _numberList.Add(i);
        }

        // 0�`7�̐�����S�����
        for (int j = 0; j < 7; j++)
        {
            // ���X�g�ɒ��g�������Ă�����
            if (_numberList.Count > 0)
            {
                // 0�`7�̒����烉���_���ɐ�����I��
                _randomNumber = Random.Range(0, _numberList.Count);

                // �I�񂾐�����ݒ�
                _selectNumber = _numberList[_randomNumber];
                                
                // �I�΂ꂽ�������폜����
                _numberList.RemoveAt(_randomNumber);
            }

            // �I�΂ꂽ�����̃~�m�����X�g�ɒǉ�����
            switch (_minoTable[_selectNumber])
            {
                // I�~�m
                case MinoTable.IMINO:
                    MinoList.Add(Instantiate(_iMino,_minoStorageTransform.position,_minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_iMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;
                
                // O�~�m
                case MinoTable.OMINO:
                    MinoList.Add(Instantiate(_oMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_oMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // S�~�m
                case MinoTable.SMINO:
                    MinoList.Add(Instantiate(_sMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_sMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // Z�~�m
                case MinoTable.ZMINO:
                    MinoList.Add(Instantiate(_zMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_zMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // J�~�m
                case MinoTable.JMINO:
                    MinoList.Add(Instantiate(_jMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_jMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // L�~�m
                case MinoTable.LMINO:
                    MinoList.Add(Instantiate(_lMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_lMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;

                // T�~�m
                case MinoTable.TMINO:
                    MinoList.Add(Instantiate(_tMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    GhostList.Add(Instantiate(_tMino, _minoStorageTransform.position, _minoStorageTransform.rotation));
                    break;
            }
        }
    }
}
