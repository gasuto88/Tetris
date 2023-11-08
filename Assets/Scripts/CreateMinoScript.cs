using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateMinoScript : MonoBehaviour
{
    // ランダム化された7種類のミノテーブルを作成するスクリプト
    private RandomSelectMinoScript _randomSelectMinoScript = default;

    // プレイヤーを動かすスクリプト
    private PlayerControllerScript _playerControllerScript = default;

    // ゴーストミノを動かすスクリプト
    private GhostMinoScript _ghostMinoScript = default;

    // ミノを生成する座標
    private Transform _minoSpawnTransform = default;

    // OとIミノを生成する座標
    private Transform _oIMinoSpawnTransform = default;

    [SerializeField, Header("ゴーストミノの色"),Range(0,1)]
    private float _alpha = 0;

    // ゴーストミノの色
    private Color _ghostColor = default;

    private void Start()
    {
        // ゴーストの色を設定
        _ghostColor = new Color(1.0f, 1.0f, 1.0f, _alpha);

        // RandomSelectMinoScriptを取得
        _randomSelectMinoScript = GetComponent<RandomSelectMinoScript>();

        // PlayerControllerScriptを取得
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        // ミノを生成する座標を取得
        _minoSpawnTransform = GameObject.Find("MinoSpawnPosition").transform;

        // OとIミノを生成する座標を取得
        _oIMinoSpawnTransform = GameObject.Find("O_IMinoSpawnPosition").transform;

        // GhostMinoScriptを取得
        _ghostMinoScript = GetComponent<GhostMinoScript>();    
    }
    
    /// <summary>
    /// Nextの先頭のミノを取り出す
    /// </summary>
    public void NextMinoTakeOut()
    {
        // 先頭のミノがOとIミノじゃなかったら
        if (_randomSelectMinoScript.MinoList[0].tag != "OMino" && _randomSelectMinoScript.MinoList[0].tag != "IMino")
        {
            // 先頭のミノを生成座標に移動する
            _randomSelectMinoScript.MinoList[0].transform.position = _minoSpawnTransform.position;
        }
        // 先頭のミノがOとIミノのとき
        else
        {
            // 先頭のミノを生成座標に移動する
            _randomSelectMinoScript.MinoList[0].transform.position = _oIMinoSpawnTransform.position;
        }

        // 先頭のミノをプレイヤーミノに設定する 
        _playerControllerScript.PlayerableMino = _randomSelectMinoScript.MinoList[0];

        // 先頭のミノをゴーストミノに設定する
        _ghostMinoScript.GhostMino = _randomSelectMinoScript.GhostList[0];

        // ゴーストミノと操作ミノ
        _ghostMinoScript.GhostMino.transform.position =
            _playerControllerScript.PlayerableMino.transform.position;

        // ゴーストミノの透明度を下げて薄くしている
        foreach(Transform _children in _ghostMinoScript.GhostMino.GetComponentInChildren<Transform>())
        {
            _children.GetComponent<SpriteRenderer>().color = _ghostColor;
        }

        // リストの先頭のミノを削除する
        _randomSelectMinoScript.MinoList.RemoveAt(0);
        _randomSelectMinoScript.GhostList.RemoveAt(0);
    }
}
