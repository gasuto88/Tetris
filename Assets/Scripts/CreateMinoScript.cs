using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour ,ICreateMino
{
    private IRandomSelectMino _iRandomSelectMino = default;

    private PlayerControllerScript _playerControllerScript = default;

    private Transform _minoSpawnTransform = default;

    private Transform _oIMinoSpawnTransform = default;

    private GhostMinoScript _ghostMinoScript = default;

    private SpriteRenderer _spriteRenderer = default;

    [SerializeField, Header("ゴーストミノの色"),Range(0,1)]
    private float _alpha = 0;

    private Color _ghostColor = default;

    private void Start()
    {
        _ghostColor = new Color(1.0f, 1.0f, 1.0f, _alpha);

        _iRandomSelectMino = GetComponent<RandomSelectMinoScript>();

        _playerControllerScript = GetComponent<PlayerControllerScript>();

        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;

        _oIMinoSpawnTransform = GameObject.Find("O_IMinoSpawnPosition").transform;

        _ghostMinoScript = GetComponent<GhostMinoScript>();    
    }
    
    /// <summary>
    /// Nextにあるミノを生成する
    /// </summary>
    public void NextMinoInstance()
    {   
        // OMinoとIMinoじゃなかったら
        if (_iRandomSelectMino.MinoList[0].tag != "OMino" && _iRandomSelectMino.MinoList[0].tag != "IMino")
        {
            _iRandomSelectMino.MinoList[0].transform.position = _minoSpawnTransform.position;
        }
        // OMinoとIMInoのとき
        else
        {
            _iRandomSelectMino.MinoList[0].transform.position = _oIMinoSpawnTransform.position;
        }

        _playerControllerScript.PlayerMino = _iRandomSelectMino.MinoList[0];

        _ghostMinoScript.GhostMino = _iRandomSelectMino.GhostList[0];

        _ghostMinoScript.GhostMino.transform.position =
            _playerControllerScript.PlayerMino.transform.position;

        // ゴーストミノの透明度を上げている
        foreach(Transform _children in _ghostMinoScript.GhostMino.GetComponentInChildren<Transform>())
        {
            _children.GetComponent<SpriteRenderer>().color = _ghostColor;
        }       

        // リストの中から生成されたミノを削除する
        _iRandomSelectMino.MinoList.RemoveAt(0);
        _iRandomSelectMino.GhostList.RemoveAt(0);
    }
}
