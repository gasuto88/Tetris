using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperRotationScript : MonoBehaviour
{

    private TMinoRotationScript _tMinoRotationScript = default;

    private void Start()
    {
        _tMinoRotationScript = GetComponent<TMinoRotationScript>();
    }

    public void SuperRotation(GameObject _playerMino,int _input)
    {
        // �v���C���[��T�~�m��I�~�m�ȊO��������
        if (_playerMino.tag != "TMino" && _playerMino.tag != "IMino")
        {

        }
        // �v���C���[��T�~�m��������
        else if(_playerMino.tag == "TMino")
        {
            _tMinoRotationScript.TMinoRotation(_playerMino,_input);
        }
        // �v���C���[��I�~�m��������
        else if(_playerMino.tag == "IMino")
        {

        }      
    }
}
