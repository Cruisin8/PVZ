using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyAnim : MonoBehaviour
{
    private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		gameObject.SetActive(false);
	}

	private void Update()
	{
		// ��ȡ��������״̬�� >= 1 ˵�����������
		if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1) {
			gameObject.SetActive(false);
		}
	}

	public void ShowReady()
	{
		gameObject.SetActive(true);
		anim.Play("ReadyAnim", 0, 0);
	}
}
