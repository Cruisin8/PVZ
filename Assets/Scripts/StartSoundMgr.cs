using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSoundMgr : MonoBehaviour
{
	public static StartSoundMgr Instance;

	private AudioSource audioSource;

	private void Awake()
	{
		Instance = this;

		audioSource = transform.GetComponent<AudioSource>();

		audioSource.Play();
	}
}
