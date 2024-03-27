using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallNut : Plant
{
	public override void Start()
	{
		// 坚果墙 最大生命值120
		maxHealth = 120;
		InitPlant();
	}

	// 受伤扣血
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
