/*----------------------------------------------------------

�X�V���@11��9��

����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartScript : MonoBehaviour
{
    /// <summary>
    /// <para>OnButtonStart</para>
    /// <para>�Q�[���V�[���ɑJ�ڂ���</para>
    /// </summary>
    public void OnButtonStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
