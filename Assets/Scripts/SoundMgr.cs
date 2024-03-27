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
			Debug.Log("��Ч����ʧ�� path:" + path);
		}

		return clip;
	}

	public AudioClip GetAudioClip(string path)
	{
		// ����ֵ���û����Ч�Ļ�������Ӹ���Ч
		if (!cache.ContainsKey(path)) {
			AudioClip clip = LoadAudio(path);
			cache.Add(path, clip);
		}

		return cache[path];
	}

	// ���ű�������		volume:������С 0-1
	public void PlayBGM(string name, float volume = 1f)
	{
		// ֹͣ����
		audioSource.Stop();

		// �ٲ���
		audioSource.clip = GetAudioClip(name);

		audioSource.Play();
	}

	// ֹͣ��������
	public void StopBGM()
	{
		audioSource.Stop();
	}

	// ������Ч  		volume:������С 0-1
	public void PlayEffect(string path, float volume = 1f)
	{
		audioSource.PlayOneShot(LoadAudio(path), volume);
	}

}
