using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    // 子弹预制体
    public GameObject bulletPrefab;
    // 子弹对象池列表
    private List<GameObject> pool = new List<GameObject>();
    // 子弹池内子弹总数
	public int amount = 10;
    // 当前子弹号
	private int currentIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

	private void Start()
	{
		for (int i = 0; i < amount; i++) { 
            GameObject go = Instantiate(bulletPrefab);
            go.SetActive(false);
            pool.Add(go);
        }
	}

    public GameObject GetPoolObject()
    {
        // 尝试从对象池中找一个未激活的返回出去
        for (int i = 0; i < pool.Count; i++) {
            if (!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        // 如果对象池中的都激活了，就生成一个新的
        GameObject go = GameObject.Instantiate(bulletPrefab);
        pool.Add(go);
        go.SetActive(true);

        return go;
    }
}
