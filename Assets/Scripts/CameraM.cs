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
	//	// ����
	//	CameraMove();
	//}

	public void CameraMove()
	{
		StartCoroutine(MoveRight());
	}

	// ����������ƶ�  -152 -> 152
	private IEnumerator MoveRight()
	{
		// ��������չʾ�Ľ�ʬ
		ZombieMgr.Instance.CreateStartZombie();

		while (transform.position.x < 152) {
			yield return new WaitForSeconds(0.02f);
			transform.position += new Vector3(3f, 0, 0);
		}

		// �ƶ������Һ�ȴ�2s
		yield return new WaitForSeconds(2f);
		StartCoroutine(MoveLeft());
	}

	private IEnumerator MoveLeft()
	{
		while (transform.position.x >= -152) {
			yield return new WaitForSeconds(0.02f);
			transform.position -= new Vector3(3f, 0, 0);
		}

		// ���ٿ���չʾ�Ľ�ʬ
		ZombieMgr.Instance.DestroyStartZombie();

		// ��ʾUI
		UIMgr.Instance.ShowUI();
		// ��ʾ��������
		UIMgr.Instance.ShowReady();

		// ���⿪ʼ����
		SkyMgr.Instance.StartCreateSun();
	}
}
