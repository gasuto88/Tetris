/*--------------------------------------------
 *
 * �X�V�� 11��9��
 * 
 * ����ҁ@�{�؁@��n
 -------------------------------------------*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    #region �萔

    // �|�C���g�̑�����{��
    private const int UP_POINT = 100;

    // Tag�̖��O -----------------------------------
    private const string SCORE_TEXT = "ScoreText";
    private const string ACTION_TEXT = "ActionText";
    // ---------------------------------------------

    #endregion


    // �X�R�A��\������e�L�X�g
    private Text _scoreText = default;

    // ��������iTETRIS�ATSPIN�Ȃǁj��\������e�L�X�g
    private Text _actionText = default;

    // ���݂̃X�R�A
    private int _scoreCount = default;

    /// <summary>
    /// �X�V�O����
    /// </summary>
    private void Start()
    {
        // TextMeshProUGUI���擾
        _scoreText = GameObject.FindGameObjectWithTag(SCORE_TEXT).GetComponent<Text>();

        // TextMeshProUGUI���擾
        _actionText = GameObject.FindGameObjectWithTag(ACTION_TEXT).GetComponent<Text>();
    }
    /// <summary>
    /// ScoreDisplay
    /// �X�R�A����ʂɕ\������
    /// </summary>
    /// <param name="_scoreCount">�������i��</param>
    public void ScoreDisplay(int eraseCount)
    {
        // �������i�����̃|�C���g�����Z
        _scoreCount += eraseCount * UP_POINT;

        // �X�R�A����ʂɕ\��
        _scoreText.text = "SCORE " + _scoreCount.ToString();        
    }

    /// <summary>
    /// ActionDisplay
    /// ��������iTETRIS�ATSPIN�Ȃǁj����ʂɕ\������
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
    /// ActionCoroutine
    /// �e�L�X�g�̕\��������
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
