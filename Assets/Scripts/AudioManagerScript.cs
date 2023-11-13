/*-------------------------------------------------
* AudioManagerScript.cs
* 
* # 作成日　2023/11/13
*
* # 作成者　本木大地
-------------------------------------------------*/
using UnityEngine;

/// <summary>
/// 音を再生する
/// </summary>
public class AudioManagerScript : MonoBehaviour {

	#region フィールド変数

	[SerializeField, Header("ミノを消したときの音")]
	private AudioClip _deleteSound = default;

	[SerializeField, Header("ミノを4段消したときの音")]
	private AudioClip _manyDeleteSound = default;

	// 効果音のAudioSource
	private AudioSource _seAudio = default;

	#endregion

	/// <summary>
	/// 更新前処理
	/// </summary>
	private void Start () 
	{
		// AudioSourceを取得
		_seAudio = GetComponent<AudioSource>();
	}
	/// <summary>
	/// ManyDeleteSound
	/// ミノを４段消した時の音を再生する
	/// </summary>
	public void ManyDeleteSound()
    {
		// ミノを４段消した時の音を再生
		_seAudio.PlayOneShot(_manyDeleteSound);
	}
	/// <summary>
	/// DeleteSound
	/// ミノを1段消した時の音を再生する
	/// </summary>
	public void DeleteSound()
    {
		// ミノを1段消した時の音を再生
		_seAudio.PlayOneShot(_deleteSound);
	}
}