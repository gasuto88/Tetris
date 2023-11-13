/*----------------------------------------------------------
 SuperRotationScript.cs

 �X�V���@11��9��

 ����ҁ@�{�؁@��n
----------------------------------------------------------*/
using UnityEngine;
/// <summary>
/// �X�[�p�[���[�e�[�V����������
/// </summary>
public class SuperRotationScript : MonoBehaviour
{
    // �ʏ�̃~�m����]����X�N���v�g
    private NormalMinoRotationScript _normalMinoRotationScript = default;

    // I�~�m����]����X�N���v�g
    private IMinoRotationScript _iMinoRotationScript = default;

    /// <summary>
    /// �X�V�O����
    /// </summary>
    private void Start()
    {
        // NormalMinoRotationScript���擾
        _normalMinoRotationScript = GetComponent<NormalMinoRotationScript>();

        // IMinoRotationScript���擾
        _iMinoRotationScript = GetComponent<IMinoRotationScript>();
    }

    /// <summary>
    /// SuperRotation
    /// �X�[�p�[���[�e�[�V����������
    /// </summary>
    /// <param name="playerMino">����ł���~�m</param>
    /// <param name="input">���E�̓���</param>
    public void SuperRotation(GameObject playerMino,int input)
    {
        // �v���C���[��I�~�m��O�~�m�ȊO��������
        if (playerMino.tag != "IMino" && playerMino.tag != "OMino")
        {
            // �ʏ�̃~�m����]����
            _normalMinoRotationScript.NormalMinoRotation(playerMino, input);
        }
        // �v���C���[��I�~�m��������
        else if(playerMino.tag == "IMino")
        {
            // I�~�m����]����
            _iMinoRotationScript.IMinoRotation(playerMino,input);
        }
    }
}
