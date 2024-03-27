using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunFlower : Plant
{
	// ���������ʱ��
	private float timer;

    // ��ȴ���5s
    private float internel = 5f;

	private bool isLight = false;

	public override void Start()
	{
		timer = 0;

		// ���տ� �������ֵ12
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
			// ���ŷ��⶯��
			anim.SetBool("isLight", true);
		}
	}

	// ���⶯��������Ϻ��л������⶯��
	public void FinishSunAnimOver()
	{
		// ���ű䰵����
		anim.SetBool("isLight", false);

		// ��������
		CreateSun();

		// ���ü�ʱ��
		timer = 0;
	}

	private void CreateSun()
    {
		// ������������������տ��������Ҳ�
		bool isLeft = Random.Range(0, 2) < 1;
		GameObject go = Resources.Load<GameObject>("Prefabs/Sun");
		if(go == null) {
			Debug.Log("Prefabs/Sun ����Ԥ�����ȡʧ��");
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

		// 3s������
		GameObject.Destroy(sun, 3f);
	}
}
