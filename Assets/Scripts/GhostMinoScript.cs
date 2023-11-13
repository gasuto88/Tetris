/*----------------------------------------------------------

更新日　11月9日

制作者　本木　大地
----------------------------------------------------------*/
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
    private FieldManagerScript _fieldDataScript = default;

    // ゲームの状態を管理するスクリプト
    private GameControllerScript _gameControllerScript = default;

    // プレイヤーの移動方向
    private readonly Vector3 _vectorRight = Vector3.right;
    private readonly Vector3 _vectorUp = Vector3.up;
    private readonly Vector3 _vectorForward = Vector3.forward;

    // ゴーストミノ
    public GameObject GhostMino { get => _ghostMino; set => _ghostMino = value; }

   /// <summary>
   /// 更新前処理
   /// </summary>
    private void Start()
    {
        // PlayerControllerScriptを取得
        _playerControllerScript = GetComponent<PlayerControllerScript>();

        // GameControllerScriptを取得
        _gameControllerScript = GameObject.Find("GameController").GetComponent<GameControllerScript>();

        // FieldDataScriptを取得
        _fieldDataScript = GameObject.Find("Stage").GetComponent<FieldManagerScript>();
    }
    /// <summary>
    /// GhostMinoMove
    /// ゴーストミノを動かす
    /// </summary>
    public void GhostMinoMove()
    {
        // プレイヤーの座標を設定
        GhostMino.transform.position = _playerControllerScript.PlayerableMino.transform.position;

        // 下に移動した回数を初期化
        _heightCount = default;

        // プレイヤーが壁に重なってない　かつ　下に移動した回数がフィールドの高さより少ないとき
        while (!_playerControllerScript.CheckCollision(_ghostMino) &&
            _heightCount <= _fieldDataScript.Height)
        {
            // ゴーストミノを下に１マス移動
            _ghostMino.transform.Translate(-_vectorUp, Space.World);

            //下に移動した回数を加算
            _heightCount++;
            
        }
        // ゴーストミノを上に１マス移動
        _ghostMino.transform.Translate(_vectorUp, Space.World);

        // プレイヤーの角度を設定
        GhostMino.transform.rotation = _playerControllerScript.PlayerableMino.transform.rotation;

    }
}
