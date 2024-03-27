using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	// �ƶ��ٶ�
    public float speed = 8f;
	// �ƶ�����
	private Vector3 dir = new Vector3(-1, 0, 0);

	// ����״̬��
    private Animator anim;
	// �ƶ�״̬
	public bool isWalk = true;
	// ����״̬
	public bool isHurt = false;

	// ��ʬ������
	public int damage = 4;
	// ��ʬ�������
	public float attackInterval = 0.5f;
	// ��ʬ������ʱ��
	private float damageTime;
	// ��ʬ��ǰ����ֵ
	public int currentHealth;
	// ��ʬ�������ֵ
	public int maxHealth = 6;
	// ��ʬʧȥͷ��֮�������ֵ
	public int lostHeadHealth = 4;

	// ��ʬ�����ͷ
	public GameObject head;
	// ����״̬
	public bool isDie = false;
	// �ж�ͷ��û�е���
	public bool isLostHead = false;
	
	// ��Ϸ������ ��ʬ�ߵ����λ��ʱ��Ϸʧ��
	public Vector3 finalPoint;

	private void Start()
	{
		anim = GetComponent<Animator>();
		currentHealth = maxHealth;
		head = transform.Find("head").gameObject;

		head.SetActive(false);
		isDie = false;
		isLostHead = false;

		GameObject go = GameObject.Find("FinalPoint");
		if (go != null) { 
			finalPoint = go.transform.position;
		} else {
			Debug.Log("δ�ҵ�FinalPoint");
			finalPoint = new Vector3(-531, -71, 0);
		}
		
	}

	private void Update()
	{
		// ս���׶ν�ʬ��ʼ�ƶ�
		if(LvMgr.Instance.currentState == GameState.Fight) {
			if (isDie) {
				return;
			}

			if (isWalk) {
				anim.SetBool("isWalk", true);
				transform.position += dir * Time.deltaTime * speed;

				// ��ʬ�߹��˲�ƺ��-513�� ���Ӵ��ţ�-531���߹�ȥ
				if (transform.position.x < finalPoint.x + 30) {
					Vector3 dir = (finalPoint - transform.position).normalized;
					transform.Translate(dir * Time.deltaTime * speed * 2);

					if(Vector3.Distance(finalPoint,transform.position) < 0.05f) {
						// ��ʬ�ߵ����յ� ��Ϸ����
						LvMgr.Instance.currentState = GameState.End;
					}
				}
			}
		}
	}

	// ��ʬ������������
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ��������������ǩ��ֲ��
		if (collision.tag == "Plant") {
			Plant plant = collision.gameObject.GetComponent<Plant>();
			if (plant == null) {
				// ��ʬ��ײ�����ֲ���쳣
				Debug.Log("��ʬ��ײ�����ֲ���쳣");
				return;
			}

			if (plant.isOnGround == false) {
				// ֲ�ﱻ�������ϣ���δ��ֲ
				return;
			}

			// ����ֹͣ����s
			isWalk = false;
			anim.SetBool("isWalk", false);
		}
	}

	// ��ʬ������������ײ
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isDie) {
			// ��ʬ����
			return;
		}

		if(collision.tag == "Plant") {
			Plant plant = collision.gameObject.GetComponent<Plant>();
			if (plant == null) {
				// ��ʬ��ײ�����ֲ���쳣
				Debug.Log("��ʬ��ײ�����ֲ���쳣");
				return;
			}

			if (plant.isOnGround == false) {
				// ֲ�ﱻ�������ϣ���δ��ֲ
				return;
			}
			
			// ��ʼ��ʱ
			damageTime += Time.deltaTime;
			// ���Թ���
			if(damageTime >= attackInterval) {
				// ���ý�ʬ������ʱ��damageTime
				damageTime = 0;

				// ����ֲ�� 
				plant.ChangeHealth(damage);
				// ֲ������
				if (plant.currentHealth <= 0) {
					isWalk = true;
					anim.SetBool("isWalk", true);
				}

				// ������Ч
				SoundMgr.Instance.PlayEffect(Config.S_chomp, 0.5f);

			}
		}
	}

	// �˳���ײ��ʱ �Ե�ֲ��
	private void OnTriggerExit2D (Collider2D collision)
	{
		if (isDie) {
			return;
		}

		isWalk = true;
		anim.SetBool("isWalk", true);

	}

	// ���˿�Ѫ
	public int ChangeHealth(int damage)
	{
		isHurt = true;

		currentHealth -= damage;

		// ��ʬʧȥͷ��
		if(currentHealth < lostHeadHealth && !isLostHead) {
			isLostHead = true;
			anim.SetBool("isHurt", true);
			head.SetActive(true);
		}

		// ��ʬ����
		if (currentHealth <= 0) {
			// ������������
			anim.SetTrigger("Die");
			isDie = true;
			//GameObject.Destroy(gameObject);
		}

		return currentHealth;
	}

	public void DieAnim()
	{
		anim.enabled = false;
		GameObject.Destroy(gameObject);
	}
}
