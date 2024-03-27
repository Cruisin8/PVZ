using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public static GameMgr Instance;

	// 阳光数量
	private int sunCount;

	private void Start()
	{
		Instance = this;
		
		// 初始阳光 0
		sunCount = 0;
		UIMgr.Instance.ChangeUICount(sunCount);
		
	}

	// 获取阳光数
	public int GetSunCount()
	{
		return sunCount; 
	}

	// 修改阳光数
	public void ChangeSunCount(int num)
	{
		if(sunCount + num < 0) {
			return;
		}

		// 数据处理
		sunCount += num;

		// UI
		UIMgr.Instance.ChangeUICount(sunCount);
	}

}
