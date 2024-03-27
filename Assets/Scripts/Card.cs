using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// ��Ƭ��ȴʱ��
    private float coolTime = 4;
	// ��ȴ��ʱ��
    private float timer;
	// ��ɫ��Ƭͼ��
	private Image image;
	// ��Ƭ���ڱ��������ڿ�Ƭ���ⲻ��������
	private GameObject bg;
	// ��Ƭ���ĵ�����
	public int costSun = 50;

	// Ҫ����������
	private GameObject plantPrefab;
	private GameObject curObj;

	private void Start()
	{
		timer = 0;
		image = transform.Find("progress").GetComponent<Image>();
		bg = transform.Find("bg").gameObject;
		LoadByName(gameObject.name);
	}

	private void Update()
	{
		timer += Time.deltaTime;
		UpdateProgress();
		UpdateBg();
	}

	private void UpdateProgress()
	{
		// ������ȴ�ٷֱ�
		float per = Mathf.Clamp(timer / coolTime, 0, 1);
		image.fillAmount = 1 - per;
	}

	private void UpdateBg()
	{
		// 1. �ж��Ƿ�����ȴ�� fillAmount = 0
		// 2. �ж������Ƿ��㹻
		if (image.fillAmount == 0 && GameMgr.Instance.GetSunCount() >= costSun) {
			// bg������
			bg.SetActive(false);

		}
		else {
			bg.SetActive(true);
		}
	}

	public Vector3 ScreenToWorld(Vector3 pos)
	{
		Vector3 po = Camera.main.ScreenToWorldPoint(pos);

		// ȷ��posΪ0
		Vector3 final = new Vector3(po.x, po.y, 0);

		return final;
	}

	public void LoadByName(string name)
	{
		string[] cardName = name.Split("_");
		string resPath = "Prefabs/" + cardName[1];
		plantPrefab = Resources.Load<GameObject>(resPath);
		if(plantPrefab == null) {
			Debug.Log("��Ƭ���ض�ӦԤ����ʧ�� ��Ƭ����:"+ name + "Ԥ����·��: " + resPath);
		}
	}

	// ����һ�����壬�������������λ������Ӧ���������괦
	public void OnBeginDrag(PointerEventData eventData)
	{
		// ������ڱ������ڼ���״̬��ֱ�ӷ��أ��ڰ������δ�жϣ�
		if (bg.activeSelf) {
			return;
		}

		// unity����ϵ�� 1.��������ϵ 2.��Ļ����ϵ 3.�ӿ�����
		// ��ӡ������Ļ���꣬�����������꣨������card�ϣ�
		//Debug.Log(eventData.position);
		curObj = Instantiate(plantPrefab);
		curObj.transform.position = ScreenToWorld(eventData.position);
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (curObj != null) {
			curObj.transform.position = ScreenToWorld(eventData.position);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		// �Ƿ��ڵ���
		if (curObj != null) {
			Collider2D[] col = Physics2D.OverlapPointAll(ScreenToWorld(eventData.position));
			// ����
			for (int i = 0; i < col.Length; i++) {
				// 1.��������  2.��û��ֲ��
				if (col[i].tag == "Land" && col[i].gameObject.transform.childCount == 0) {
					curObj.transform.position = col[i].transform.position;
					curObj.transform.SetParent(col[i].transform);

					// �ж��Ƿ��Ѿ���ֲ�ˣ�������϶�ʱֲ���ڷ����ӵ���bug
					Plant plant = curObj.GetComponent<Plant>();
					if(plant != null) {
						plant.isOnGround = true;
					}

					// ��ֲ������curObj
					curObj = null;
					// ��ʱ������
					timer = 0;
					// �������� ����UI
					GameMgr.Instance.ChangeSunCount(-costSun);
					UIMgr.Instance.ChangeUICount(GameMgr.Instance.GetSunCount());
					// ������Ч
					SoundMgr.Instance.PlayEffect(Config.S_plant, 1);
					return;

				}

			}
			// ���û�к��ʵ�����curObj�����ڣ�����Ҫ���� ������Ҫ�������⡢������ֲCD��
			if(curObj != null) {
				GameObject.Destroy(curObj); 
				curObj = null;
			}
		}
	}
}
