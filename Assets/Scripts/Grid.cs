using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
	private void Start()
	{
        CreateGrid();
	}

    // ´´½¨Íø¸ñ
	public void CreateGrid()
    {
        GameObject go = new GameObject();

        go.AddComponent<BoxCollider2D>();

        go.GetComponent<BoxCollider2D>().size = new Vector2(82, 96);

        go.transform.position = transform.position;

        go.name = "GridMetaPoint";

        for(int i = 0; i < 9; i++) {
            for(int j = 0; j < 5; j++) {
                GameObject tmp = Instantiate(go);

                tmp.tag = "Land";

				tmp.transform.position = transform.position + new Vector3(82 * i, 96 * j, 0);

                tmp.name = i + "_" + j;

            }
        }
    }
}
