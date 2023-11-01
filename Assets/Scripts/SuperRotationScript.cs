using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{

    private TMinoRotationScript _tMinoRotationScript = default;

    private OtherMinoRotationScript _otherMinoRotationScript = default;

    private IMinoRotationScript _iMinoRotationScript = default;

    private void Start()
    {
        _tMinoRotationScript = GetComponent<TMinoRotationScript>();

        _otherMinoRotationScript = GetComponent<OtherMinoRotationScript>();

        _iMinoRotationScript = GetComponent<IMinoRotationScript>();
    }

    public void SuperRotation(GameObject _playerMino,int _input)
    {
        // �v���C���[��T�~�m��I�~�m�ȊO��������
        if (_playerMino.tag != "TMino" && _playerMino.tag != "IMino")
        {
            _otherMinoRotationScript.OtherMinoRotation(_playerMino, _input);
        }
        // �v���C���[��T�~�m��������
        else if(_playerMino.tag == "TMino")
        {
            _tMinoRotationScript.TMinoRotation(_playerMino,_input);
        }
        // �v���C���[��I�~�m��������
        else if(_playerMino.tag == "IMino")
        {
            _iMinoRotationScript.IMinoRotation(_playerMino,_input);
        }      
    }
}
