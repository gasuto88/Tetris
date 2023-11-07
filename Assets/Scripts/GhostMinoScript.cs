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
        
    }

    private void FloorMino()
    {
        GhostMino.transform.position = _playerControllerScript.PlayerMino.transform.position;

        _heightCount = default;

        while (!_playerControllerScript.BeforeMoving(_ghostMino) &&
            _heightCount <= _fieldDataScript.Height)
        {
            _ghostMino.transform.Translate(0f, -1f, 0f, Space.World);
            _heightCount++;
            
        }
        
        _ghostMino.transform.Translate(0f, 1f, 0f, Space.World);

        

        GhostMino.transform.rotation = _playerControllerScript.PlayerMino.transform.rotation;

    }
}
