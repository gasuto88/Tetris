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

    private int _scoreCount = default;

    private ScoreScript _scoreScript = default;

    // �t�B�[���h�f�[�^
    private GameObject[,] _fieldData = new GameObject[20,10];

    private int _height = 19;

    private int _width = 9;
    //{
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0},
    //    {0,0,0,0,0,0,0,0,0,0}
           
    //};

    public GameObject[,] FieldData { get => _fieldData; set => _fieldData = value; }
    public int Height { get => _height; set => _height = value; }
    public int Width { get => _width; set => _width = value; }

    private void Start()
    {
        _scoreScript = GameObject.Find("Canvas").GetComponent<ScoreScript>();
    }

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
                if (FieldData[i, j] != null)
                {
                    blockCount++;
                }
            }
            // ����񑵂��Ă�����
            if(blockCount >= 10)
            {
                for (int k = i; k > -1; k--)
                {
                    for (int l = 0; l < 10; l++)
                    {             
                        if(k == i &&FieldData[i, l] != null)
                        {
                            Destroy(FieldData[i, l].gameObject);

                            FieldData[i, l] = null;
                            
                        }
                        if (k > 0 && FieldData[k-1, l] != null)
                        {                                                
                            FieldData[k-1, l].transform.Translate(0f, -1f, 0f,Space.World);

                            //�@�����i�Ɉ��̒i���㏑������
                            FieldData[k, l] = FieldData[k - 1, l];

                            FieldData[k - 1, l] = null;
                        }                                        
                    }               
                }
                i++;

                _scoreCount += 100;
                
            }
            blockCount = 0;

            _scoreScript.ScoreDisplay(_scoreCount);
        }
        ////�f�o�b�N�p
        //for (int j = 0; j < 20; j++)
        //{
        //    string unti = "";
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (FieldData[j, i] != null)
        //        {
        //            unti += 1;
        //        }
        //        else
        //        {
        //            unti += 0;
        //        }
        //    }
        //    Debug.LogWarning(unti);
        //}
    }
}
