/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;

public class IMinoRotationScript : MonoBehaviour
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
    /// <para>IMinoRotation</para>
    /// <para>Iミノのスーパーローテーションをする</para>
    /// </summary>
    /// <param name="playerMino">操作できるミノ</param>
    /// <param name="input">左右の入力</param>
    public void IMinoRotation(GameObject playerMino, int input)
    {
        // 操作できるミノの座標を保管
        Vector3 playerPositionTemp = playerMino.transform.position;

        // 操作できるミノの角度を保管
        Quaternion playerRotationTemp = playerMino.transform.rotation;

        // ミノが上をむいているとき
        if (playerMino.transform.rotation.eulerAngles.z == 0)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    // 第１条件---------------------------------------------------------
                    case 1:
                        playerMino.transform.Rotate(0f, 0f, -input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        // 左入力をしていたら
                        if(input < 0)
                        {
                            playerMino.transform.Translate(-2f, 0f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        }                        
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        playerMino.transform.Translate(-3f, -input, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        playerMino.transform.Translate(3f, input * 3f, 0f, Space.World);
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
                        playerMino.transform.Rotate(0f, 0f, -input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(-2f, 0f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(0f, -2f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(-3f, 3f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(3f, 3f, 0f, Space.World);
                        }

                        break;
                    // -----------------------------------------------------------------

                    //最終処理---------------------------------------------------------
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
                        playerMino.transform.Rotate(0f, 0f, -input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(2f, 0f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        }                    
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(3f, 1f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(3f, -2f, 0f, Space.World);
                        }                       
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(-3f, 1f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(3f, 3f, 0f, Space.World);
                        }
                        break;
                    // ----------------------------------------------------------------

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
                        playerMino.transform.Rotate(0f, 0f, -input * 90f, Space.World);
                        break;
                    // -----------------------------------------------------------------

                    // 第２条件---------------------------------------------------------
                    case 2:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(2f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // 第３条件---------------------------------------------------------
                    case 3:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        }
                        break;
                    // -----------------------------------------------------------------

                    // 第４条件---------------------------------------------------------
                    case 4:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(-3f, 2f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(3f, 1f, 0f, Space.World);
                        }                       
                        break;
                    // -----------------------------------------------------------------

                    // 第５条件---------------------------------------------------------
                    case 5:
                        // 左入力をしていたら
                        if (input < 0)
                        {
                            playerMino.transform.Translate(3f, -3f, 0f, Space.World);
                        }
                        // 右入力をしていたら
                        else if (input > 0)
                        {
                            playerMino.transform.Translate(-3f, -3f, 0f, Space.World);
                        }
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
