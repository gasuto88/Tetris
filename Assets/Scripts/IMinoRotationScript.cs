using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IMinoRotationScript : MonoBehaviour
{
    private PlayerControllerScript _playerControllerScript = default;

    private void Start()
    {
        _playerControllerScript = GetComponent<PlayerControllerScript>();
    }

    /// <summary>
    /// Iミノのスーパーローテーション
    /// </summary>
    /// <param name="_playerMino"></param>
    /// <param name="_input"></param>
    public void IMinoRotation(GameObject _playerMino, int _input)
    {
        Vector3 _playerPositionTemp = _playerMino.transform.position;

        Quaternion _playerRotationTemp = _playerMino.transform.rotation;

        // ミノが上をむいているとき
        if (_playerMino.transform.rotation.eulerAngles.z == 0)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    case 2:
                        if(_input < 0)
                        {
                            _playerMino.transform.Translate(-2f, 0f, 0f, Space.World);
                        }
                        else if(_input > 0)
                        {
                            _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        }
                        
                        break;
                    case 3:
                        _playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        break;
                    case 4:
                        _playerMino.transform.Translate(-3f, -_input, 0f, Space.World);
                        break;
                    case 5:
                        _playerMino.transform.Translate(3f, _input * 3f, 0f, Space.World);
                        break;
                    case 6:
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;

                }
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    break;
                }
            }
        }
        // ミノが左をむいているとき
        else if (_playerMino.transform.rotation.eulerAngles.z == 90)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    case 2:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-2f, 0f, 0f, Space.World);
                        }
                        else if(_input > 0)
                        {
                            _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        }
                        

                        break;
                    case 3:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        }
                        break;
                    case 4:

                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(0f, -2f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        }

                        break;
                    case 5:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-3f, 3f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, 3f, 0f, Space.World);
                        }

                        break;
                    case 6:
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;

                }
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    break;
                }
            }
        }
        // ミノが下をむいているとき
        else if (_playerMino.transform.rotation.eulerAngles.z == 180)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    case 2:
                        if(_input < 0)
                        {
                            _playerMino.transform.Translate(2f, 0f, 0f, Space.World);
                        }
                        else if(_input > 0)
                        {
                            _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        }
                        
                        break;
                    case 3:
                        _playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        break;
                    case 4:
                        if(_input < 0)
                        {
                            _playerMino.transform.Translate(3f, 1f, 0f, Space.World);
                        }
                        else if(_input > 0)
                        {
                            _playerMino.transform.Translate(3f, -2f, 0f, Space.World);
                        }                       
                        break;
                    case 5:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-3f, 1f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, 3f, 0f, Space.World);
                        }
                        break;
                    case 6:
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;
                }
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    break;
                }
            }
        }
        // ミノが右をむいているとき
        else if (_playerMino.transform.rotation.eulerAngles.z == 270)
        {
            for (int i = 1; i <= 6; i++)
            {
                switch (i)
                {
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, -_input * 90f, Space.World);
                        break;
                    case 2:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(2f, 0f, 0f, Space.World);
                        }
                        break;
                    case 3:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, 0f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-3f, 0f, 0f, Space.World);
                        }
                        break;
                    case 4:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(-3f, 2f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(3f, 1f, 0f, Space.World);
                        }
                        
                        break;
                    case 5:
                        if (_input < 0)
                        {
                            _playerMino.transform.Translate(3f, -3f, 0f, Space.World);
                        }
                        else if (_input > 0)
                        {
                            _playerMino.transform.Translate(-3f, -3f, 0f, Space.World);
                        }
                        break;
                    case 6:
                        _playerMino.transform.position = _playerPositionTemp;
                        _playerMino.transform.rotation = _playerRotationTemp;
                        break;

                }
                if (!_playerControllerScript.CheckCollision(_playerMino))
                {
                    break;
                }
            }
        }
    }
}
