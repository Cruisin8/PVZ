using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Start,
    Fight,
    End
}

public class LvMgr : MonoBehaviour
{
    public static LvMgr Instance;

	public GameState currentState;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		currentState = GameState.Start;
	}

	private void Update()
	{
		StateRefresh();
	}

	public void StateRefresh()
	{
		switch (currentState) {
			case GameState.Start:
				// 隐藏UI面板 播放开始动画
				UIMgr.Instance.HideUI();

				// 摄像机移动到右侧 再移回
				CameraM.Instance.CameraMove();

				// 更新游戏状态
				currentState = GameState.Fight;

				break;
			case GameState.Fight:
				ZombieMgr.Instance.isRefresh = true;

				// 播放BGM
				SoundMgr.Instance.PlayBGM(Config.Bgm1, 1);

				break;
			case GameState.End:
				GameOver();
				SceneManager.LoadScene(0);
				break;
		}
	}

	// 游戏结束
	public void GameOver()
	{
		// UI显示
		UIMgr.Instance.GameOver();

		// 清理天空
		SkyMgr.Instance.StopCreateSun();

		// 停止创建僵尸
		ZombieMgr.Instance.StopCreateSomeZombie();
		// 清理僵尸
		ZombieMgr.Instance.ClearZombie();

		StopAllCoroutines();
	}
}
