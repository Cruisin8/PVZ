using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// �ӵ����𹥻���ʬ�Ĺ���
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
		// �ȴ�5f �ӵ�����
		yield return new WaitForSeconds(8);
		// GameObject.Destroy(gameObject);
		// ȡ������״̬�����ս��������
		gameObject.SetActive(false);
	}

	private void Update()
	{
		transform.Translate(dir * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ����ǽ�ʬ
		if (collision.tag == "Zombie") {
			Zombie zom = collision.GetComponent<Zombie>();

			if(zom != null ) {
				zom.ChangeHealth(damage);
			}
		}
	}
}
