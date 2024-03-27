using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMgr : MonoBehaviour
{
	// ¿ªÊ¼ÓÎÏ·
	public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
