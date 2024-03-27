using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// 卡片冷却时间
    private float coolTime = 4;
	// 冷却计时器
    private float timer;
	// 彩色卡片图案
	private Image image;
	// 卡片纯黑背景（用于卡片阳光不足的情况）
	private GameObject bg;
	// 卡片消耗的阳光
	public int costSun = 50;

	// 要创建的物体
	private GameObject plantPrefab;
	private GameObject curObj;

	private void Start()
	{
		timer = 0;
		image = transform.Find("progress").GetComponent<Image>();
		bg = transform.Find("bg").gameObject;
		LoadByName(gameObject.name);
	}

	private void Update()
	{
		timer += Time.deltaTime;
		UpdateProgress();
		UpdateBg();
	}

	private void UpdateProgress()
	{
		// 计算冷却百分比
		float per = Mathf.Clamp(timer / coolTime, 0, 1);
		image.fillAmount = 1 - per;
	}

	private void UpdateBg()
	{
		// 1. 判断是否在冷却中 fillAmount = 0
		// 2. 判断阳光是否足够
		if (image.fillAmount == 0 && GameMgr.Instance.GetSunCount() >= costSun) {
			// bg不激活
			bg.SetActive(false);

		}
		else {
			bg.SetActive(true);
		}
	}

	public Vector3 ScreenToWorld(Vector3 pos)
	{
		Vector3 po = Camera.main.ScreenToWorldPoint(pos);

		// 确保pos为0
		Vector3 final = new Vector3(po.x, po.y, 0);

		return final;
	}

	public void LoadByName(string name)
	{
		string[] cardName = name.Split("_");
		string resPath = "Prefabs/" + cardName[1];
		plantPrefab = Resources.Load<GameObject>(resPath);
		if(plantPrefab == null) {
			Debug.Log("卡片加载对应预制体失败 卡片名称:"+ name + "预制体路径: " + resPath);
		}
	}

	// 生成一个物体，让它出现在鼠标位置所对应的世界坐标处
	public void OnBeginDrag(PointerEventData eventData)
	{
		// 如果纯黑背景处于激活状态则直接返回（黑白情况还未判断）
		if (bg.activeSelf) {
			return;
		}

		// unity坐标系： 1.世界坐标系 2.屏幕坐标系 3.视口坐标
		// 打印的是屏幕坐标，不是世界坐标（坐标在card上）
		//Debug.Log(eventData.position);
		curObj = Instantiate(plantPrefab);
		curObj.transform.position = ScreenToWorld(eventData.position);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (curObj != null) {
			curObj.transform.position = ScreenToWorld(eventData.position);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// 是否在地上
		if (curObj != null) {
			Collider2D[] col = Physics2D.OverlapPointAll(ScreenToWorld(eventData.position));
			// 遍历
			for (int i = 0; i < col.Length; i++) {
				// 1.在网格上  2.有没有植物
				if (col[i].tag == "Land" && col[i].gameObject.transform.childCount == 0) {
					curObj.transform.position = col[i].transform.position;
					curObj.transform.SetParent(col[i].transform);

					// 判断是否已经种植了，解决在拖动时植物在发射子弹的bug
					Plant plant = curObj.GetComponent<Plant>();
					if(plant != null) {
						plant.isOnGround = true;
					}

					// 种植后重置curObj
					curObj = null;
					// 计时器重置
					timer = 0;
					// 减少阳光 更新UI
					GameMgr.Instance.ChangeSunCount(-costSun);
					UIMgr.Instance.ChangeUICount(GameMgr.Instance.GetSunCount());
					// 播放音效
					SoundMgr.Instance.PlayEffect(Config.S_plant, 1);
					return;

				}

			}
			// 如果没有合适的网格，curObj还存在，则需要销毁 （还需要返还阳光、返还种植CD）
			if(curObj != null) {
				GameObject.Destroy(curObj); 
				curObj = null;
			}
		}
	}
}
