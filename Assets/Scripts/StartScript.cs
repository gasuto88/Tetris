/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScript : MonoBehaviour
{
    /// <summary>
    /// OnButtonStart
    /// ゲームシーンに遷移する
    /// </summary>
    public void OnButtonStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
