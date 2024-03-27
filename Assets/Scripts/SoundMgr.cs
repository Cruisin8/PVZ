using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMgr : MonoBehaviour
{
    public static SoundMgr Instance;

	private AudioSource audioSource;

	private Dictionary<string, AudioClip> cache = new Dictionary<string, AudioClip>();

	private void Awake()
	{
		Instance = this;

		audioSource = transform.GetComponent<AudioSource>();
	}

	public AudioClip LoadAudio(string path)
	{
		AudioClip clip = Resources.Load<AudioClip>(path);
		if(clip == null) {
			Debug.Log("音效加载失败 path:" + path);
		}

		return clip;
	}

	public AudioClip GetAudioClip(string path)
	{
		// 如果字典里没该音效的话，就添加该音效
		if (!cache.ContainsKey(path)) {
			AudioClip clip = LoadAudio(path);
			cache.Add(path, clip);
		}

		return cache[path];
	}

	// 播放背景音乐		volume:音量大小 0-1
	public void PlayBGM(string name, float volume = 1f)
	{
		// 停止播放
		audioSource.Stop();

		// 再播放
		audioSource.clip = GetAudioClip(name);

		audioSource.Play();
	}

	// 停止背景音乐
	public void StopBGM()
	{
		audioSource.Stop();
	}

	// 播放音效  		volume:音量大小 0-1
	public void PlayEffect(string path, float volume = 1f)
	{
		audioSource.PlayOneShot(LoadAudio(path), volume);
	}

}
