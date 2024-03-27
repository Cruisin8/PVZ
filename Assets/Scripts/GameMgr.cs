using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;

	// ��������
	private int sunCount;

	private void Start()
	{
		Instance = this;
		
		// ��ʼ���� 0
		sunCount = 0;
		UIMgr.Instance.ChangeUICount(sunCount);
		
	}

	// ��ȡ������
	public int GetSunCount()
	{
		return sunCount; 
	}

	// �޸�������
	public void ChangeSunCount(int num)
	{
		if(sunCount + num < 0) {
			return;
		}

		// ���ݴ���
		sunCount += num;

		// UI
		UIMgr.Instance.ChangeUICount(sunCount);
	}

}
