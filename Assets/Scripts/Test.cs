using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	private void Start()
	{
		var c = BulletPool.Instance.GetPoolObject();
		c.transform.position = new Vector3 (0, 0, 0);
	}
}
