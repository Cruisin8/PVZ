using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �㶹���� ���������ӵ��Ĺ��ܣ������𹥻���ʬ
public class Peashooter : Plant
{
	// �ӵ����ɵ�
	private Transform firePoint;

	// �ӵ������ʱ��
	private float timer;
	private float internel = 3f;

	public override void Start()
	{
		// �ӵ����ɵ�
		firePoint = transform.Find("FirePoint");

		// ÿ�����������ӵ�
		// 1. Invoke����
		// InvokeRepeating("Fire", 2, 5);
		// 2. Э�̷���
		// 3. ��ʱ������
		timer = 0;

		// �㶹���� �������ֵ20
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

	// �㶹���� ���
	private void Fire()
	{
		// ֱ�Ӽ����ӵ�
		// GameObject tmp = Resources.Load<GameObject>("Prefabs/PeaBullet");
		// GameObject go = GameObject.Instantiate(tmp, firePoint);

		// �Ӷ���ؼ����ӵ�
		GameObject go = BulletPool.Instance.GetPoolObject();
		go.transform.position = firePoint.position;

		// ������Ч
		SoundMgr.Instance.PlayEffect(Config.S_shoot, 1);
	}
}
