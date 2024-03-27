using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Zombie : MonoBehaviour
{
	// 移动速度
    public float speed = 8f;
	// 移动方向
	private Vector3 dir = new Vector3(-1, 0, 0);

	// 动画状态机
    private Animator anim;
	// 移动状态
	public bool isWalk = true;
	// 受伤状态
	public bool isHurt = false;

	// 僵尸攻击力
	public int damage = 4;
	// 僵尸攻击间隔
	public float attackInterval = 0.5f;
	// 僵尸攻击计时器
	private float damageTime;
	// 僵尸当前生命值
	public int currentHealth;
	// 僵尸最大生命值
	public int maxHealth = 6;
	// 僵尸失去头部之后的生命值
	public int lostHeadHealth = 4;

	// 僵尸掉落的头
	public GameObject head;
	// 死亡状态
	public bool isDie = false;
	// 判断头有没有掉落
	public bool isLostHead = false;
	
	// 游戏结束点 僵尸走到这个位置时游戏失败
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
			Debug.Log("未找到FinalPoint");
			finalPoint = new Vector3(-531, -71, 0);
		}
		
	}

	private void Update()
	{
		// 战斗阶段僵尸开始移动
		if(LvMgr.Instance.currentState == GameState.Fight) {
			if (isDie) {
				return;
			}

			if (isWalk) {
				anim.SetBool("isWalk", true);
				transform.position += dir * Time.deltaTime * speed;

				// 僵尸走过了草坪（-513） 向房子大门（-531）走过去
				if (transform.position.x < finalPoint.x + 30) {
					Vector3 dir = (finalPoint - transform.position).normalized;
					transform.Translate(dir * Time.deltaTime * speed * 2);

					if(Vector3.Distance(finalPoint,transform.position) < 0.05f) {
						// 僵尸走到了终点 游戏结束
						LvMgr.Instance.currentState = GameState.End;
					}
				}
			}
		}
	}

	// 僵尸触发器刚碰到
	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 如果碰到的物体标签是植物
		if (collision.tag == "Plant") {
			Plant plant = collision.gameObject.GetComponent<Plant>();
			if (plant == null) {
				// 僵尸碰撞器检测植物异常
				Debug.Log("僵尸碰撞器检测植物异常");
				return;
			}

			if (plant.isOnGround == false) {
				// 植物被拿在手上，还未种植
				return;
			}

			// 动画停止行走s
			isWalk = false;
			anim.SetBool("isWalk", false);
		}
	}

	// 僵尸触发器持续碰撞
	private void OnTriggerStay2D(Collider2D collision)
	{
		if (isDie) {
			// 僵尸死亡
			return;
		}

		if(collision.tag == "Plant") {
			Plant plant = collision.gameObject.GetComponent<Plant>();
			if (plant == null) {
				// 僵尸碰撞器检测植物异常
				Debug.Log("僵尸碰撞器检测植物异常");
				return;
			}

			if (plant.isOnGround == false) {
				// 植物被拿在手上，还未种植
				return;
			}
			
			// 开始计时
			damageTime += Time.deltaTime;
			// 可以攻击
			if(damageTime >= attackInterval) {
				// 重置僵尸攻击计时器damageTime
				damageTime = 0;

				// 攻击植物 
				plant.ChangeHealth(damage);
				// 植物死亡
				if (plant.currentHealth <= 0) {
					isWalk = true;
					anim.SetBool("isWalk", true);
				}

				// 播放音效
				SoundMgr.Instance.PlayEffect(Config.S_chomp, 0.5f);

			}
		}
	}

	// 退出碰撞器时 吃掉植物
	private void OnTriggerExit2D (Collider2D collision)
	{
		if (isDie) {
			return;
		}

		isWalk = true;
		anim.SetBool("isWalk", true);

	}

	// 受伤扣血
	public int ChangeHealth(int damage)
	{
		isHurt = true;

		currentHealth -= damage;

		// 僵尸失去头部
		if(currentHealth < lostHeadHealth && !isLostHead) {
			isLostHead = true;
			anim.SetBool("isHurt", true);
			head.SetActive(true);
		}

		// 僵尸死亡
		if (currentHealth <= 0) {
			// 播放死亡动画
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
