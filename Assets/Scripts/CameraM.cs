using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraM : MonoBehaviour
{
    public static CameraM Instance;

	public void Awake()
	{
		Instance = this;

		transform.position = new Vector3 (-75, 0, -31);
	}

	//private void Start()
	//{
	//	// 测试
	//	CameraMove();
	//}

	public void CameraMove()
	{
		StartCoroutine(MoveRight());
	}

	// 摄像机向右移动  -152 -> 152
	private IEnumerator MoveRight()
	{
		// 创建开场展示的僵尸
		ZombieMgr.Instance.CreateStartZombie();

		while (transform.position.x < 152) {
			yield return new WaitForSeconds(0.02f);
			transform.position += new Vector3(3f, 0, 0);
		}

		// 移动到最右后等待2s
		yield return new WaitForSeconds(2f);
		StartCoroutine(MoveLeft());
	}

	private IEnumerator MoveLeft()
	{
		while (transform.position.x >= -152) {
			yield return new WaitForSeconds(0.02f);
			transform.position -= new Vector3(3f, 0, 0);
		}

		// 销毁开场展示的僵尸
		ZombieMgr.Instance.DestroyStartZombie();

		// 显示UI
		UIMgr.Instance.ShowUI();
		// 显示开场动画
		UIMgr.Instance.ShowReady();

		// 阳光开始生成
		SkyMgr.Instance.StartCreateSun();
	}
}
