using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMgr : MonoBehaviour
{
	// ��ʼ��Ϸ
	public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
