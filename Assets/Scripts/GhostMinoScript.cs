using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMinoScript : MonoBehaviour
{
    private PlayerControllerScript _playerControllerScript = default;

    private GameObject _ghostMino = default;

    private int _heightCount = default;

    private enum GhostState
    {
        START,
        MOVE
    }

    //private GhostState _ghostState = GhostState.START;

    public GameObject GhostMino { get => _ghostMino; set => _ghostMino = value; }
    //public GhostState GhostState { get => _ghostState; set => _ghostState = value; }

    private FieldDataScript _fieldDataScript = default;

    private Vector2 _ghostPosition = default;

    private GameControllerScript _gameControllerScript = default;

    private void Start()
    {
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
    }

    public void GhostController()
    {
        FloorMino();
        //    switch (_ghostState)
        //    {
        //        case GhostState.START:

        //            _ghostPosition = _playerControllerScript.PlayerMino.transform.position;

        //            FloorMino();

        //            _ghostState = GhostState.MOVE;
        //            break;

        //        case GhostState.MOVE:

        //            if (Mathf.RoundToInt(GhostMino.transform.position.x) 
        //                != Mathf.RoundToInt(_playerControllerScript.PlayerMino.transform.position.x))
        //            {

        //                FloorMino();
        //            }
        //            break;
        //    }
        }

    private void FloorMino()
    {
        GhostMino.transform.position = _playerControllerScript.PlayerMino.transform.position;

        _heightCount = default;

        while (!_playerControllerScript.BeforeMoving(_ghostMino) &&
            _heightCount <= _fieldDataScript.Height)
        {
            //_ghostPosition += Vector2.down;
            _ghostMino.transform.Translate(0f, -1f, 0f, Space.World);
            _heightCount++;
            
        }
        
        _ghostMino.transform.Translate(0f, 1f, 0f, Space.World);

        //GhostMino.transform.position = _ghostPosition;

        GhostMino.transform.rotation = _playerControllerScript.PlayerMino.transform.rotation;

    }
}
