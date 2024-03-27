using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMgr : MonoBehaviour
{
	// ����
    public static SkyMgr Instance;

	// ̫������λ�÷�Χ
    private float MinX = -413F;
	private float MaxX = 234F;
	
	private float MinY = -227F;
	private float MaxY = 253F;

	private GameObject sunPre;

	private void Awake()
	{
		Instance = this;

		sunPre = Resources.Load<GameObject>("Prefabs/Sun");

	}

	// ��ʼ��������
	public void StartCreateSun(float delay = 5)
	{
		// 2s���һ�ε��ã�֮��ÿdelay�����һ��CreateSun������delayĬ��Ϊ5s
		InvokeRepeating("CreateSun", 2, delay);
	}

	// ֹͣ��������
	public void StopCreateSun()
	{
		CancelInvoke();
	}

	public void CreateSun()
	{
		GameObject go = GameObject.Instantiate(sunPre);
		Sun sun = go.GetComponent<Sun>();

		float X = Random.Range(MinX, MaxX);
		float Y = Random.Range(MinY, MaxY);
		// ����Ļ����������
		sun.InitSkySun(X, Screen.height + 10F, Y);
		
	}
}
