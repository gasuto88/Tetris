using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMinoScript : MonoBehaviour
{
    // ゴーストミノ
    private GameObject _ghostMino = default;

    //　ゴーストミノが下に移動した回数
    private int _heightCount = default;

    // プレイヤーを動かすスクリプト
    private PlayerControllerScript _playerControllerScript = default;

    // フィールドを管理するスクリプト
    private FieldDataScript _fieldDataScript = default;

    // ゲームの状態を管理するスクリプト
    private GameControllerScript _gameControllerScript = default;

    public GameObject GhostMino { get => _ghostMino; set => _ghostMino = value; }

   
    private void Start()
    {
        // PlayerControllerScriptを取得
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        // GameControllerScriptを取得
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // FieldDataScriptを取得
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldDataScript>();
    }
    /// <summary>
    /// ゴーストミノの挙動
    /// </summary>
    public void GhostMinoController()
    {
        // ゴーストミノの座標をプレイヤーミノと同じにする
        GhostMino.transform.position = _playerControllerScript.PlayerableMino.transform.position;

        // ゴーストミノが下に移動した回数を初期化
        _heightCount = default;

        // プレイヤーが壁に重なってない　かつ　ゴーストミノが下に移動した回数がフィールドの高さより少ないとき
        while (!_playerControllerScript.BeforeMoving(_ghostMino) &&
            _heightCount <= _fieldDataScript.Height)
        {
            // ゴーストミノを下に１マス移動
            _ghostMino.transform.Translate(0f, -1f, 0f, Space.World);

            // ゴーストミノが下に移動した回数を１増やす
            _heightCount++;
            
        }
        // ゴーストミノを上に１マス移動
        _ghostMino.transform.Translate(0f, 1f, 0f, Space.World);

        // ゴーストミノの回転をプレイヤーミノと同じにする
        GhostMino.transform.rotation = _playerControllerScript.PlayerableMino.transform.rotation;

    }
}
