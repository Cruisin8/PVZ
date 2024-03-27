using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
	// 是否已经种植成功
	public bool isOnGround = false;

	// 当前生命值
	public int currentHealth;
	// 最大生命值
	public int maxHealth;

	// 植物动画机
	protected Animator anim;

	public virtual void Start()
	{
		InitPlant();
	}

	// 植物初始化
	public void InitPlant()
	{
		isOnGround = false;

		anim = GetComponent<Animator>();

		currentHealth = maxHealth;
	}

	// 受伤扣血
	public virtual int ChangeHealth(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0) {
			GameObject.Destroy(gameObject);
		}

		return currentHealth;
	}
}
