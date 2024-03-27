using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 阳光
public class Sun : MonoBehaviour
{
    // 阳光种类： true天上掉落 false向日葵生成
    private bool isSkySun = false;
	
    // 天空中的阳光下落的目标点
	private float TargetY;

	// 阳光下落速度
	private float sunSpeed = 100;
    // 阳光数量
    private GameObject sunNum;
    // 阳光是否被拾取
    private bool isClick = false;

	public void Start()
	{
        sunNum = GameObject.Find("SunNum");
	}

	private void Update()
	{
        // 判断是否为天上掉落的阳光
        if (isSkySun) {
            // 判定阳光是否被点击拾取，如果阳光被拾取了，就不再下落，用于防止阳光飞向面板时产生偏移
            if(isClick) {
                return;
            }

            // 判断是否掉落到了目标点
            if(transform.position.y > TargetY) {
				// new Vector3(0, -1, 0) : y向下的方向向量
				transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * sunSpeed);
            }
            else {
				// 掉落到目标点了，3s后销毁
				GameObject.Destroy(gameObject, 3f);
            }
        }
	}

	// 初始化天上的太阳
	public void InitSkySun(float x, float y, float targetY)
	{
		transform.position = new Vector3(x, y, 0);

		TargetY = targetY;

		isSkySun = true;

	}

	// 鼠标点击事件
	private void OnMouseDown()
	{
        // 阳光被拾取
        isClick = true;

		// 点击阳光后，阳光移动到阳光计数位置
		// 坐标转换，屏幕坐标转换成世界坐标
		Vector3 target = Camera.main.ScreenToWorldPoint(sunNum.transform.position);

		// 调用携程
		StartCoroutine(FlyTo(target));

        // 更新数据区 更新UI
        GameMgr.Instance.ChangeSunCount(50);
        UIMgr.Instance.ChangeUICount(GameMgr.Instance.GetSunCount());

		// 播放音效
		SoundMgr.Instance.PlayEffect(Config.S_points, 1);

	}

    // 收集阳光的协程
    private IEnumerator FlyTo(Vector3 target)
    {
		// 获取拾取阳光到阳光计数位置的方向向量，normalized表示长度为1的标准化向量
		Vector3 dir = (target - transform.position).normalized;

		// 判断两点距离，没到就一直移动
		// 飞行时间超过2s自动销毁（因为坐标转换问题，导致阳光无法飞到正确位置，所以加一个时间）
		float flyTime = 0.0f;
		while (Vector3.Distance(target,transform.position) > 0.1f && flyTime <= 2f) {
            // 等待0.01s时间后再执行
            yield return new WaitForSeconds(0.01f);
            transform.Translate(dir * 25);
			flyTime += 0.01f;
		}

        // 当距离小于0.1，或飞行时间超过2s时，销毁阳光
        GameObject.Destroy(gameObject);

	}
}
