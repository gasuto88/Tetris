/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{
    // 通常のミノを回転するスクリプト
    private NormalMinoRotationScript _normalMinoRotationScript = default;

    // Iミノを回転するスクリプト
    private IMinoRotationScript _iMinoRotationScript = default;

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // NormalMinoRotationScriptを取得
        _normalMinoRotationScript = GetComponent<NormalMinoRotationScript>();

        // IMinoRotationScriptを取得
        _iMinoRotationScript = GetComponent<IMinoRotationScript>();
    }

    /// <summary>
    /// <para>SuperRotation</para>
    /// <para>スーパーローテーションをする</para>
    /// </summary>
    /// <param name="playerMino">操作できるミノ</param>
    /// <param name="input">左右の入力</param>
    public void SuperRotation(GameObject playerMino,int input)
    {
        // プレイヤーがIミノとOミノ以外だったら
        if (playerMino.tag != "IMino" && playerMino.tag != "OMino")
        {
            // 通常のミノを回転する
            _normalMinoRotationScript.NormalMinoRotation(playerMino, input);
        }
        // プレイヤーがIミノだったら
        else if(playerMino.tag == "IMino")
        {
            // Iミノを回転する
            _iMinoRotationScript.IMinoRotation(playerMino,input);
        }
    }
}
