using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform nodeParent;

    private void Awake()
    {
        Vector2Int blockCount = new Vector2Int(4, 4);

        for ( int i = 0; i < blockCount.x * blockCount.y; i++ )
        {
            GameObject clone = Instantiate(nodePrefab, nodeParent);
        }

    }
}
