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
				// ����UI��� ���ſ�ʼ����
				UIMgr.Instance.HideUI();

				// ������ƶ����Ҳ� ���ƻ�
				CameraM.Instance.CameraMove();

				// ������Ϸ״̬
				currentState = GameState.Fight;

				break;
			case GameState.Fight:
				ZombieMgr.Instance.isRefresh = true;

				// ����BGM
				SoundMgr.Instance.PlayBGM(Config.Bgm1, 1);

				break;
			case GameState.End:
				GameOver();
				SceneManager.LoadScene(0);
				break;
		}
	}

	// ��Ϸ����
	public void GameOver()
	{
		// UI��ʾ
		UIMgr.Instance.GameOver();

		// �������
		SkyMgr.Instance.StopCreateSun();

		// ֹͣ������ʬ
		ZombieMgr.Instance.StopCreateSomeZombie();
		// ����ʬ
		ZombieMgr.Instance.ClearZombie();

		StopAllCoroutines();
	}
}
