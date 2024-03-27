using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 豌豆射手 负责制造子弹的功能，不负责攻击僵尸
public class Peashooter : Plant
{
	// 子弹生成点
	private Transform firePoint;

	// 子弹发射计时器
	private float timer;
	private float internel = 3f;

	public override void Start()
	{
		// 子弹生成点
		firePoint = transform.Find("FirePoint");

		// 每隔几秒生成子弹
		// 1. Invoke方法
		// InvokeRepeating("Fire", 2, 5);
		// 2. 协程方法
		// 3. 计时器方法
		timer = 0;

		// 豌豆射手 最大生命值20
		maxHealth = 20;
		InitPlant();

	}

	private void Update()
	{
		if(isOnGround == false) {
			return;
		}

		timer += Time.deltaTime;

		if (timer >= internel) {
			Fire();
			timer = 0;
		}
	}

	// 豌豆射手 射击
	private void Fire()
	{
		// 直接加载子弹
		// GameObject tmp = Resources.Load<GameObject>("Prefabs/PeaBullet");
		// GameObject go = GameObject.Instantiate(tmp, firePoint);

		// 从对象池加载子弹
		GameObject go = BulletPool.Instance.GetPoolObject();
		go.transform.position = firePoint.position;

		// 播放音效
		SoundMgr.Instance.PlayEffect(Config.S_shoot, 1);
	}
}
