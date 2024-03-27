using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 子弹负责攻击僵尸的功能
public class PeaBullet : MonoBehaviour
{
	private float speed = 150f;

	private Vector3 dir = new Vector3(1, 0, 0);

	public int damage = 1;

	private void Start()
	{
		StartCoroutine(DeleteBullet());
	}

	private IEnumerator DeleteBullet()
	{
		// 等待5f 子弹销毁
		yield return new WaitForSeconds(8);
		// GameObject.Destroy(gameObject);
		// 取消激活状态，回收进对象池中
		gameObject.SetActive(false);
	}

	private void Update()
	{
		transform.Translate(dir * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果是僵尸
		if (collision.tag == "Zombie") {
			Zombie zom = collision.GetComponent<Zombie>();

			if(zom != null ) {
				zom.ChangeHealth(damage);
			}
		}
	}
}
