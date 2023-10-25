using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDataScript : MonoBehaviour
{
    // �t�B�[���h�̃I�u�W�F�N�g
    // 0 ��
    // 1 �����Ȃ��~�m
   
    private const int NO_BLOCK = 0;
    private const int STATIC_MINO = 1;

    // �t�B�[���h�f�[�^
    private int[,] _fieldData =
    {
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0},
        {0,0,0,0,0,0,0,0,0,0}
           
    };

    public int[,] FieldData { get => _fieldData; set => _fieldData = value; }

    /// <summary>
    /// �~�m������񖄂܂������������
    /// </summary>
    public void FieldMinoErase()
    {
        int blockCount = default;

        for (int i = 19; i > -1; i--)
        {
            for(int j = 0;j < 10; j++)
            {
                if (FieldData[i, j] == 1)
                {
                    blockCount++;
                }
            }

            if(blockCount >= 10)
            {

                for (int k = i + 1; k > 0; k--)
                {
                    for (int l = 0; l < 10; l++)
                    {
                        //�@�����i�Ɉ��̒i���㏑������
                        FieldData[k, l] = FieldData[k - 1, l];
                    }
                }

                blockCount = 0;
            }
        }
    }
}
