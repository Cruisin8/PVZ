using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OverPannel : MonoBehaviour
{
    private Image overImage;

	private void Start()
	{
		overImage = GetComponent<Image>();

		gameObject.SetActive(false);
	}

	public void Over()
	{
		gameObject.SetActive(true);

		StartCoroutine(ReturnStart());
	}

	// 加载开始场景
	private IEnumerator ReturnStart()
	{
		yield return new WaitForSeconds(0.5f);

		SceneManager.LoadScene(0);
	}
}
