using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMgr : MonoBehaviour
{
    public static ZombieMgr Instance;

	// ��ʬ���ɵ�
	public Transform[] zombieBornPoint;
	// ��ʬԤ����
	public GameObject zombiePrefeb;
	// ������˳�������ʬͼ��㼶
	private int layerIndex = 0;

	// �洢�����Ľ�ʬ�б�
	public List<Zombie> zombieList = new List<Zombie>();
	// ����ʱչʾ�Ľ�ʬ
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

	// ����һֻ��ʬ
	private void CreateZombie()
	{
		// ���ɽ�ʬ
		GameObject go = GameObject.Instantiate(zombiePrefeb);
		// �漴ѡ��һ�����ɵ�
		int index = Random.Range(0, 5); // 0 1 2 3 4 
		// ���ý�ʬ����λ�õĸ��ڵ�
		go.transform.parent = zombieBornPoint[index];
		// �����ɵ�ԭ�����ɽ�ʬ
		go.transform.localPosition = Vector3.zero;

		// ���������ʬ�ص�ʱ��ͼ�����⣬������˳������㼶
		layerIndex += 1;
		go.GetComponent<SpriteRenderer>().sortingOrder = layerIndex;

		// ��ӽ���ʬ�б���
		AddZombie(go.GetComponent<Zombie>());

	}

	// ����ʬ��ӽ��б���
	public void AddZombie(Zombie zom)
	{
		zombieList.Add(zom);
	}

	// ����ʬ�Ƴ��б�
	public void RemoveZombie(Zombie zom)
	{
		zombieList.Remove(zom);
	}

	// ���ɿ���ʱչʾ�Ľ�ʬ
	public void CreateStartZombie()
	{
		for (int i = 0; i < 8; i++) {
			// ���ɽ�ʬ
			GameObject go = GameObject.Instantiate(zombiePrefeb);
			// �漴ѡ��һ�����ɵ�
			int index = Random.Range(0, 5); // 0 1 2 3 4 
											// ���ý�ʬ����λ�õĸ��ڵ�
			go.transform.parent = zombieBornPoint[index];
			// �����ɵ�ԭ�����ɽ�ʬ
			go.transform.localPosition = Vector3.zero;

			// ��ӽ���ʬ�б�
			Zombie zombie = go.GetComponent<Zombie>();
			zombieStartShow.Add(zombie);

			// ֹͣ����չʾ��ʬ�����߶���
			go.GetComponent<Animator>().speed = 0;
			zombie.isWalk = false;
		}
	}

	// ���ٿ���ʱչʾ�Ľ�ʬ
	public void DestroyStartZombie() 
	{
		for (int i = 0; i < zombieStartShow.Count; i++) {
			GameObject.Destroy(zombieStartShow[i].gameObject);
		}
	}

	// ÿ��һ��ʱ�䴴����ʬ
	public void CreateZombieByTime()
	{
		// ��ʱ ����13ֻ�Ĺؿ�
		StartCoroutine(CreateSomeZombie(12));
	}

	// ʹ�ü����ݣ���ֻ��һ��
	private IEnumerator CreateSomeZombie(int total)
	{
		while (layerIndex < total) {
			// �Ƿ��ˢ�½�ʬ
			if (isRefresh) {
				// ���3-5sˢ��
				int delay = Random.Range(3, 5);
				yield return new WaitForSeconds(delay);

				// ����1-4ֻ��ʬ
				int randomNum = Random.Range(1, 4);
				for(int i = 0;i < randomNum;i++) {
					CreateZombie();
				}
				
			}
			// �ȴ���������
			yield return new WaitForSeconds(5);
		}

		// �ȴ���ʬ�����½�
		yield return new WaitForSeconds(5);
		// ��ʱ ����13ֻ
		StartCoroutine(CreateSomeZombie(12));
	}

	// ֹͣ������ʬ
	public void StopCreateSomeZombie()
	{
		StopAllCoroutines();
	}

	// ����ʬ
	public void ClearZombie()
	{
		isRefresh = false;
		for (int i = 0; i < zombieList.Count; i++) {
			GameObject.Destroy(zombieList[i].gameObject);
			zombieList.Clear();
		}
	}
}
