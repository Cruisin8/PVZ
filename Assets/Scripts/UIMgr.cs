using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMgr : MonoBehaviour
{
    public static UIMgr Instance;

	// 阳光数量
	public Text sunNum;
	// 植物选择面板
	private GameObject chooseBg;

	// 开场准备动画
	private ReadyAnim readyAnim;
	// 游戏结束字幕
	private OverPannel overPannel;

	private void Awake()
	{
		Instance = this;

		sunNum = GameObject.Find("SunNum").GetComponent<Text>();
		chooseBg = GameObject.Find("ChooseBg").gameObject;

		readyAnim = GameObject.Find("ReadyAnim").gameObject.GetComponent<ReadyAnim>();

		overPannel = GameObject.Find("OverPannel").gameObject.GetComponent<OverPannel>();
	}

	public void ChangeUICount(int num)
	{
		sunNum.text = num.ToString();
	}

	public void HideUI()
	{
		chooseBg.SetActive(false);
	}

	public void ShowUI()
	{
		chooseBg.SetActive(true);
	}

	public void ShowReady()
	{
		readyAnim.ShowReady();
	}

	public void GameOver()
	{
		overPannel.Over();
	}
}
