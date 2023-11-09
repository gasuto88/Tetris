/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;


public class RetryScript : MonoBehaviour
{
    /// <summary>
    /// <para>OnButtonRetry</para>
    /// <para>ゲームシーンに遷移する</para>
    /// </summary>
    public void OnButtonRetry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
