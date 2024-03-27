using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����
public class Sun : MonoBehaviour
{
    // �������ࣺ true���ϵ��� false���տ�����
    private bool isSkySun = false;
	
    // ����е����������Ŀ���
	private float TargetY;

	// ���������ٶ�
	private float sunSpeed = 100;
    // ��������
    private GameObject sunNum;
    // �����Ƿ�ʰȡ
    private bool isClick = false;

	public void Start()
	{
        sunNum = GameObject.Find("SunNum");
	}

	private void Update()
	{
        // �ж��Ƿ�Ϊ���ϵ��������
        if (isSkySun) {
            // �ж������Ƿ񱻵��ʰȡ��������ⱻʰȡ�ˣ��Ͳ������䣬���ڷ�ֹ����������ʱ����ƫ��
            if(isClick) {
                return;
            }

            // �ж��Ƿ���䵽��Ŀ���
            if(transform.position.y > TargetY) {
				// new Vector3(0, -1, 0) : y���µķ�������
				transform.Translate(new Vector3(0, -1, 0) * Time.deltaTime * sunSpeed);
            }
            else {
				// ���䵽Ŀ����ˣ�3s������
				GameObject.Destroy(gameObject, 3f);
            }
        }
	}

	// ��ʼ�����ϵ�̫��
	public void InitSkySun(float x, float y, float targetY)
	{
		transform.position = new Vector3(x, y, 0);

		TargetY = targetY;

		isSkySun = true;

	}

	// ������¼�
	private void OnMouseDown()
	{
        // ���ⱻʰȡ
        isClick = true;

		// �������������ƶ����������λ��
		// ����ת������Ļ����ת������������
		Vector3 target = Camera.main.ScreenToWorldPoint(sunNum.transform.position);

		// ����Я��
		StartCoroutine(FlyTo(target));

        // ���������� ����UI
        GameMgr.Instance.ChangeSunCount(50);
        UIMgr.Instance.ChangeUICount(GameMgr.Instance.GetSunCount());

		// ������Ч
		SoundMgr.Instance.PlayEffect(Config.S_points, 1);

	}

    // �ռ������Э��
    private IEnumerator FlyTo(Vector3 target)
    {
		// ��ȡʰȡ���⵽�������λ�õķ���������normalized��ʾ����Ϊ1�ı�׼������
		Vector3 dir = (target - transform.position).normalized;

		// �ж�������룬û����һֱ�ƶ�
		// ����ʱ�䳬��2s�Զ����٣���Ϊ����ת�����⣬���������޷��ɵ���ȷλ�ã����Լ�һ��ʱ�䣩
		float flyTime = 0.0f;
		while (Vector3.Distance(target,transform.position) > 0.1f && flyTime <= 2f) {
            // �ȴ�0.01sʱ�����ִ��
            yield return new WaitForSeconds(0.01f);
            transform.Translate(dir * 25);
			flyTime += 0.01f;
		}

        // ������С��0.1�������ʱ�䳬��2sʱ����������
        GameObject.Destroy(gameObject);

	}
}
