using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TMinoRotationScript : MonoBehaviour
{
    private PlayerControllerScript _playerControllerScript = default;

    private void Start()
    {
        _playerControllerScript = GetComponent<PlayerControllerScript>();
    }
    /// <summary>
    /// Tミノのスーパーローテーション
    /// </summary>
    /// <param name="_playerMino"></param>
    /// <param name="_input"></param>
    public void TMinoRotation(GameObject _playerMino, int _input)
    {
        Transform _playerTransformTemp = _playerMino.transform;
        // ミノが上をむいているとき
        if (_playerMino.transform.rotation.eulerAngles.z == 0)
        {
            for (int i = 1 ; i <= 6 ; i++)
            {
                switch (i)
                {
                    case 1:
                        _playerMino.transform.Rotate(0f, 0f, _input * 90f,Space.World);
                        break;
                    case 2:
                        _playerMino.transform.Translate(_input,0f, 0f,Space.World);
                        break;
                    case 3:
                        _playerMino.transform.Translate(0f, 1f, 0f, Space.World);
                        break;
                    case 4:
                        _playerMino.transform.Translate(-_input, -3f, 0f, Space.World);
                        break;
                    case 5:
                        _playerMino.transform.Translate(_input, 0f, 0f, Space.World);
                        break;
                    case 6:
                        _playerMino.transform.position = _playerTransformTemp.position;
                        _playerMino.transform.rotation = _playerTransformTemp.rotation;
                        break;
                    
                }
                if (!_playerControllerScript.BeforeMoving(_playerMino))
                {
                    break;
                }
                Debug.LogWarning("ue" + i);
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
                        _playerMino.transform.Rotate(0f, 0f, _input * 90f,Space.World);
                        break;
                    case 2:
                        
                        _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);                 
                        
                        break;
                    case 3:
                        _playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        break;
                    case 4:
                        
                        _playerMino.transform.Translate(1f, 3f, 0f, Space.World);
                        
                        break;
                    case 5:
                        _playerMino.transform.Translate(-1f, 0f, 0f, Space.World);
                        
                        break;
                    case 6:
                        _playerMino.transform.position = _playerTransformTemp.position;
                        _playerMino.transform.rotation = _playerTransformTemp.rotation;
                        break;

                }
                if (!_playerControllerScript.BeforeMoving(_playerMino))
                {
                    break;
                }
                Debug.LogWarning("左" + i);
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
                        _playerMino.transform.Rotate(0f, 0f, _input * 90f,Space.World);
                        break;
                    case 2:
                        _playerMino.transform.Translate(-_input, 0f, 0f, Space.World);
                        break;
                    case 3:
                        _playerMino.transform.Translate(0, 1f, 0f, Space.World);
                        break;
                    case 4:
                        _playerMino.transform.Translate(_input,-3f, 0f, Space.World);
                        break;
                    case 5:
                        _playerMino.transform.Translate(-_input, 0f, 0f, Space.World);
                        break;
                    case 6:
                        _playerMino.transform.position = _playerTransformTemp.position;
                        _playerMino.transform.rotation = _playerTransformTemp.rotation;
                        break;

                }
                if (!_playerControllerScript.BeforeMoving(_playerMino))
                {
                    break;
                }
                Debug.LogWarning("sita" + i);
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
                        _playerMino.transform.Rotate(0f, 0f, _input * 90f,Space.World);
                        break;
                    case 2:
                        _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        break;
                    case 3:
                        _playerMino.transform.Translate(0f, -1f, 0f, Space.World);
                        break;
                    case 4:
                        _playerMino.transform.Translate(-1f, 3f, 0f, Space.World);
                        break;
                    case 5:
                        _playerMino.transform.Translate(1f, 0f, 0f, Space.World);
                        break;
                    case 6:
                        _playerMino.transform.position = _playerTransformTemp.position;
                        _playerMino.transform.rotation = _playerTransformTemp.rotation;
                        break;

                }
                if (!_playerControllerScript.BeforeMoving(_playerMino))
                {
                    break;
                }
                Debug.LogWarning("右" + i);
            }
        }
    }
}
