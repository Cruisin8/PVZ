using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMgr : MonoBehaviour
{
    public static ZombieMgr Instance;

	// 僵尸生成点
	public Transform[] zombieBornPoint;
	// 僵尸预制体
	public GameObject zombiePrefeb;
	// 按生成顺序给定僵尸图层层级
	private int layerIndex = 0;

	// 存储创建的僵尸列表
	public List<Zombie> zombieList = new List<Zombie>();
	// 开场时展示的僵尸
	public List<Zombie> zombieStartShow = new List<Zombie>();

	public bool isRefresh = false;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		CreateZombieByTime();
	}

	// 生成一只僵尸
	private void CreateZombie()
	{
		// 生成僵尸
		GameObject go = GameObject.Instantiate(zombiePrefeb);
		// 随即选择一个生成点
		int index = Random.Range(0, 5); // 0 1 2 3 4 
		// 设置僵尸生成位置的父节点
		go.transform.parent = zombieBornPoint[index];
		// 在生成点原点生成僵尸
		go.transform.localPosition = Vector3.zero;

		// 用来解决僵尸重叠时的图层问题，按生成顺序给定层级
		layerIndex += 1;
		go.GetComponent<SpriteRenderer>().sortingOrder = layerIndex;

		// 添加进僵尸列表中
		AddZombie(go.GetComponent<Zombie>());

	}

	// 将僵尸添加进列表中
	public void AddZombie(Zombie zom)
	{
		zombieList.Add(zom);
	}

	// 将僵尸移除列表
	public void RemoveZombie(Zombie zom)
	{
		zombieList.Remove(zom);
	}

	// 生成开场时展示的僵尸
	public void CreateStartZombie()
	{
		for (int i = 0; i < 8; i++) {
			// 生成僵尸
			GameObject go = GameObject.Instantiate(zombiePrefeb);
			// 随即选择一个生成点
			int index = Random.Range(0, 5); // 0 1 2 3 4 
											// 设置僵尸生成位置的父节点
			go.transform.parent = zombieBornPoint[index];
			// 在生成点原点生成僵尸
			go.transform.localPosition = Vector3.zero;

			// 添加进僵尸列表
			Zombie zombie = go.GetComponent<Zombie>();
			zombieStartShow.Add(zombie);

			// 停止开场展示僵尸的行走动画
			go.GetComponent<Animator>().speed = 0;
			zombie.isWalk = false;
		}
	}

	// 销毁开场时展示的僵尸
	public void DestroyStartZombie() 
	{
		for (int i = 0; i < zombieStartShow.Count; i++) {
			GameObject.Destroy(zombieStartShow[i].gameObject);
		}
	}

	// 每隔一段时间创建僵尸
	public void CreateZombieByTime()
	{
		// 暂时 生成13只的关卡
		StartCoroutine(CreateSomeZombie(12));
	}

	// 使用假数据，先只做一关
	private IEnumerator CreateSomeZombie(int total)
	{
		while (layerIndex < total) {
			// 是否该刷新僵尸
			if (isRefresh) {
				// 随机3-5s刷新
				int delay = Random.Range(3, 5);
				yield return new WaitForSeconds(delay);

				// 创建1-4只僵尸
				int randomNum = Random.Range(1, 4);
				for(int i = 0;i < randomNum;i++) {
					CreateZombie();
				}
				
			}
			// 等待开场动画
			yield return new WaitForSeconds(5);
		}

		// 等待僵尸总数下降
		yield return new WaitForSeconds(5);
		// 暂时 生成13只
		StartCoroutine(CreateSomeZombie(12));
	}

	// 停止创建僵尸
	public void StopCreateSomeZombie()
	{
		StopAllCoroutines();
	}

	// 清理僵尸
	public void ClearZombie()
	{
		isRefresh = false;
		for (int i = 0; i < zombieList.Count; i++) {
			GameObject.Destroy(zombieList[i].gameObject);
			zombieList.Clear();
		}
	}
}
