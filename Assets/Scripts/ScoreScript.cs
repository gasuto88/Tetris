/*--------------------------------------------
 *
 * �X�V�� 11��9��
 * 
 * ����ҁ@�{�؁@��n
 -------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
�@�@// �X�R�A��\������e�L�X�g
    private TextMeshProUGUI _scoreText = default;

    // ��������iTETRIS�ATSPIN�Ȃǁj��\������e�L�X�g
    private TextMeshProUGUI _actionText = default;

    // ���݂̃X�R�A
    private int _scoreCount = default;

    // �|�C���g�̑�����{��
    private const int UP_POINT = 100;

    /// <summary>
    /// <para>�X�V�O����</para>
    /// </summary>
    private void Start()
    {
        // TextMeshProUGUI���擾
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        // TextMeshProUGUI���擾
        _actionText = GameObject.Find("ActionText").GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// <para>ScoreDisplay</para>
    /// <para>�X�R�A����ʂɕ\������</para>
    /// </summary>
    /// <param name="_scoreCount">�������i��</param>
    public void ScoreDisplay(int eraseCount)
    {
        // �������i�����̃|�C���g�����Z
        _scoreCount += eraseCount * UP_POINT;

        // �X�R�A����ʂɕ\��
        _scoreText.text = "SCORE : " + _scoreCount.ToString();        
    }
    /// <summary>
    /// <para>ActionDisplay</para>
    /// <para>��������iTETRIS�ATSPIN�Ȃǁj����ʂɕ\������</para>
    /// </summary>
    /// <param name="action">�Z�̖��O</param>
    public void ActionDisplay(string action)
    {
        // �����������ʂɕ\��
        _actionText.text = action;

        // ���ԍ��Ńe�L�X�g�̕\��������
        StartCoroutine(ActionCoroutine());
    }
    /// <summary>
    /// <para>ActionCoroutine</para>
    /// <para>�e�L�X�g�̕\��������</para>
    /// </summary>
    /// <returns></returns>
    private IEnumerator ActionCoroutine()
    {
        // 2�b�܂�
        yield return new WaitForSeconds(2);

        // �e�L�X�g�̕\��������
        _actionText.text = "";
    }
}
