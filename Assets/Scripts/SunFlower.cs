using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
	// 生产阳光计时器
	private float timer;

    // 冷却间隔5s
    private float internel = 5f;

	private bool isLight = false;

	public override void Start()
	{
		timer = 0;

		// 向日葵 最大生命值12
		maxHealth = 12;
		InitPlant();
	}

	private void Update()
	{
		if (isOnGround == false) {
			return;
		}

		timer += Time.deltaTime;

		if(timer >= internel) {
			// 播放发光动画
			anim.SetBool("isLight", true);
		}
	}

	// 发光动画播放完毕后，切换不发光动画
	public void FinishSunAnimOver()
	{
		// 播放变暗动画
		anim.SetBool("isLight", false);

		// 生产阳光
		CreateSun();

		// 重置计时器
		timer = 0;
	}

	private void CreateSun()
    {
		// 让阳光随机生成在向日葵的左侧或右侧
		bool isLeft = Random.Range(0, 2) < 1;
		GameObject go = Resources.Load<GameObject>("Prefabs/Sun");
		if(go == null) {
			Debug.Log("Prefabs/Sun 阳光预制体读取失败");
		}
		float X, Y = 0;
		if (isLeft) {
			X = Random.Range(transform.position.x - 70f, transform.position.x - 60f);
			Y = Random.Range(transform.position.y - 20f, transform.position.y + 20f);
		}
		else {
			X = Random.Range(transform.position.x + 70f, transform.position.x + 60f);
			Y = Random.Range(transform.position.y - 20f, transform.position.y + 20f);
		}
		GameObject sun = Instantiate(go);
		sun.transform.position = new Vector3(X, Y, 0);

		// 3s后销毁
		GameObject.Destroy(sun, 3f);
	}
}
