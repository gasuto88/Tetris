/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;

public class TSpinCheckScript : MonoBehaviour
{
    // フィールドを管理するスクリプト
    private FieldManagerScript _fieldManagerScript = default;

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // FieldManagerScriptを取得
        _fieldManagerScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }

    /// <summary>
    /// <para>TSpinCheck</para>
    /// <para>Tスピン判定を調べる</para>
    /// </summary>
    /// <param name="playerMino">操作できるミノ</param>
    /// <returns>Tスピンしているか</returns>
    public bool TSpinCheck(GameObject playerMino)
    {
        // 整数化
        int playerPosX = Mathf.RoundToInt(playerMino.transform.position.x);
        int playerPosY = Mathf.RoundToInt(playerMino.transform.position.y);

        // ブロックの数
        int blockCount = 0;
        
        // 回転軸から見て左上
        if (playerPosX <= 0 || _fieldManagerScript.FieldData[playerPosX - 1, -playerPosY - 1])
        {
            blockCount++;
        }
        // 回転軸から見て右上
        if (playerPosX >= 9 || _fieldManagerScript.FieldData[playerPosX + 1, - playerPosY - 1])
        {
            blockCount++;
        }
        // 回転軸から見て左下
        if (playerPosX <= 0 || playerPosY <= -19 || _fieldManagerScript.FieldData[playerPosX - 1, -playerPosY + 1])
        {
            blockCount++;
        }
        // 回転軸から見て右下
        if (playerPosX >= 9 || playerPosY <= -19 || _fieldManagerScript.FieldData[playerPosX + 1, -playerPosY + 1])
        {           
            blockCount++;
        }

        // ブロックが3個以上だったら
        if(blockCount >= 3)
        {
            // Tスピンしている
            return true;
        }
        // Tスピンしていない
        return false;
    }
}
