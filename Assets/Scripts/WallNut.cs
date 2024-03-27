using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
	public override void Start()
	{
		// ���ǽ �������ֵ120
		maxHealth = 120;
		InitPlant();
	}

	// ���˿�Ѫ
	public override int ChangeHealth(int damage)
	{
		currentHealth -= damage;

		if (currentHealth <= 0) {
			GameObject.Destroy(gameObject);
		}

		anim.SetFloat("pre", (float)currentHealth / maxHealth);

		return currentHealth;
	}
}
