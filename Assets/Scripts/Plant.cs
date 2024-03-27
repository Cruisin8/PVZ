using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	// �Ƿ��Ѿ���ֲ�ɹ�
	public bool isOnGround = false;

	// ��ǰ����ֵ
	public int currentHealth;
	// �������ֵ
	public int maxHealth;

	// ֲ�ﶯ����
	protected Animator anim;

	public virtual void Start()
	{
		InitPlant();
	}

	// ֲ���ʼ��
	public void InitPlant()
	{
		isOnGround = false;

		anim = GetComponent<Animator>();

		currentHealth = maxHealth;
	}

	// ���˿�Ѫ
	public virtual int ChangeHealth(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0) {
			GameObject.Destroy(gameObject);
		}

		return currentHealth;
	}
}
