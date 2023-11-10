/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;

public class NormalMinoRotationScript : MonoBehaviour
{
    // プレイヤーを動かすスクリプト
    private PlayerControllerScript _playerControllerScript = default;

    /// <summary>
    /// <para>更新前処理</para>
    /// </summary>
    private void Start()
    {
        // PlayerControllerScriptを取得
        _playerControllerScript = GetComponent<PlayerControllerScript>();
    }

    /// <summary>
    /// <para>NormalMinoRotation</para>
    /// <para>通常ミノのスーパーローテーションをする</para>
    /// </summary>
    /// <param name="playerMino">操作できるミノ</param>
    /// <param name="input">左右の入力</param>
    public void NormalMinoRotation(GameObject playerMino, int input)
    {
        // 操作できるミノの座標を保管
        Vector3 playerPositionTemp = playerMino.transform.position;

        // 操作できるミノの角度を保管
        Quaternion playerRotationTemp = playerMino.transform.rotation;

        // ミノが上をむいているとき
        if (playerMino.transform.rotation.eulerAngles.z == 0)
        {
            for (int i = 1 ; i <= 6 ; i++)
            {
                switch (i)
                {
                    // 第１条件---------------------------------------------------------
                    case 1:
                        playerMino.transform.Rotate(0f, 0f, -input * 90f,Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        playerMino.transform.Translate(-input,0f, 0f,Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        playerMino.transform.Translate(0f, 1f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        playerMino.transform.Translate(input, -3f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        playerMino.transform.Translate(-input, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 最終処理---------------------------------------------------------
                    case 6:
                        // ミノを初期の状態に戻す
                        playerMino.transform.position = playerPositionTemp;
                        playerMino.transform.rotation = playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------                  
                }
                // ミノがブロックに重なっていなければ
                if (!_playerControllerScript.CheckCollision(playerMino))
                {
                    // 処理を終了する
                    break;
                }
            }
        }

        // ミノが左をむいているとき
        else if (playerMino.transform.rotation.eulerAngles.z == 90)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // 第１条件---------------------------------------------------------
                    case 1:
                        playerMino.transform.Rotate(0f, 0f, -input * 90f,Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:                       
                        playerMino.transform.Translate(-1f, 0f, 0f, Space.World);                              
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:                      
                        playerMino.transform.Translate(1f, 3f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        playerMino.transform.Translate(-1f, 0f, 0f, Space.World);                       
                        break;
                    // -----------------------------------------------------------------

                    // 最終処理---------------------------------------------------------
                    case 6:
                        // ミノを初期の状態に戻す
                        playerMino.transform.position = playerPositionTemp;
                        playerMino.transform.rotation = playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------
                }
                // ミノがブロックに重なっていなければ
                if (!_playerControllerScript.CheckCollision(playerMino))
                {
                    // 処理を終了する
                    break;
                }
            }
        }
        // ミノが下をむいているとき
        else if (playerMino.transform.rotation.eulerAngles.z == 180)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // 第１条件---------------------------------------------------------
                    case 1:
                        playerMino.transform.Rotate(0f, 0f, -input * 90f,Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        playerMino.transform.Translate(input, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        playerMino.transform.Translate(0, 1f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        playerMino.transform.Translate(-input,-3f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        playerMino.transform.Translate(input, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 最終処理---------------------------------------------------------
                    case 6:
                        // ミノを初期の状態に戻す
                        playerMino.transform.position = playerPositionTemp;
                        playerMino.transform.rotation = playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------

                }
                // ミノがブロックに重なっていなければ
                if (!_playerControllerScript.CheckCollision(playerMino))
                {
                    // 処理を終了する
                    break;
                }             
            }
        }
        // ミノが右をむいているとき
        else if (playerMino.transform.rotation.eulerAngles.z == 270)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // 第１条件---------------------------------------------------------
                    case 1:
                        playerMino.transform.Rotate(0f, 0f, -input * 90f,Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        playerMino.transform.Translate(-1f, 3f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 最終処理---------------------------------------------------------
                    case 6:
                        // ミノを初期の状態に戻す
                        playerMino.transform.position = playerPositionTemp;
                        playerMino.transform.rotation = playerRotationTemp;
                        break;
                    // -----------------------------------------------------------------

                }
                // ミノがブロックに重なっていなければ
                if (!_playerControllerScript.CheckCollision(playerMino))
                {
                    // 処理を終了する
                    break;
                }              
            }
        }
    }
}
