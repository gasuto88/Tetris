/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;


public class RetryScript : MonoBehaviour
{
    /// <summary>
    /// <para>OnButtonRetry</para>
    /// <para>�Q�[���V�[���ɑJ�ڂ���</para>
    /// </summary>
    public void OnButtonRetry()
    {
        SceneManager.LoadScene("GameScene");
    }
}
