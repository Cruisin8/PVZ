using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMgr : MonoBehaviour
{
	// 单例
    public static SkyMgr Instance;

	// 太阳掉落位置范围
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

	// 开始创建阳光
	public void StartCreateSun(float delay = 5)
	{
		// 2s后第一次调用，之后每delay秒调用一次CreateSun方法，delay默认为5s
		InvokeRepeating("CreateSun", 2, delay);
	}

	// 停止创建阳光
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
		// 在屏幕外生成阳光
		sun.InitSkySun(X, Screen.height + 10F, Y);
		
	}
}
