using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    // �ӵ�Ԥ����
    public GameObject bulletPrefab;
    // �ӵ�������б�
    private List<GameObject> pool = new List<GameObject>();
    // �ӵ������ӵ�����
	public int amount = 10;
    // ��ǰ�ӵ���
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
        // ���ԴӶ��������һ��δ����ķ��س�ȥ
        for (int i = 0; i < pool.Count; i++) {
            if (!pool[i].activeInHierarchy) {
                pool[i].SetActive(true);
                return pool[i];
            }
        }

        // ���������еĶ������ˣ�������һ���µ�
        GameObject go = GameObject.Instantiate(bulletPrefab);
        pool.Add(go);
        go.SetActive(true);

        return go;
    }
}
