using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldDataScript : MonoBehaviour
{
    // フィールドのオブジェクト
    // 0 空白
    // 1 動かないミノ
   
    private const int NO_BLOCK = 0;
    private const int STATIC_MINO = 1;

    // フィールドデータ
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
    /// ミノが横一列埋まったら消す処理
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
                        //　消す段に一個上の段を上書きする
                        FieldData[k, l] = FieldData[k - 1, l];
                    }
                }

                blockCount = 0;
            }
        }
    }
}
