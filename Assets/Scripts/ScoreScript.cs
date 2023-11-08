using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
�@�@// �X�R�A��\������e�L�X�g
    private TextMeshProUGUI _scoreText = default;

    // �Z�iTETRIS�ATSPIN�Ȃǁj��\������e�L�X�g
    private TextMeshProUGUI _actionText = default;

    // ���݂̃X�R�A
    private int _scoreCount = default;

    private void Start()
    {
        // TextMeshProUGUI���擾
        _scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        // TextMeshProUGUI���擾
        _actionText = GameObject.Find("ActionText").GetComponent<TextMeshProUGUI>();
    }
    /// <summary>
    /// �X�R�A����ʂɕ\�����鏈��
    /// </summary>
    /// <param name="_scoreCount">�������i��</param>
    public void ScoreDisplay(int _eraseCount)
    {
        _scoreCount += _eraseCount * 100;

        // �X�R�A����ʂɕ\��
        _scoreText.text = "SCORE : " + _scoreCount.ToString();        
    }
    /// <summary>
    /// �Z�iTETRIS�ATSPIN�Ȃǁj����ʂɕ\�����鏈��
    /// </summary>
    /// <param name="_action">�Z�̖��O</param>
    public void ActionDisplay(string _action)
    {
        // �Z����ʂɕ\��
        _actionText.text = _action;

        // ���ԍ��Ńe�L�X�g�̕\��������
        StartCoroutine(ActionCoroutine());
    }
    /// <summary>
    /// �e�L�X�g�̕\������������
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
