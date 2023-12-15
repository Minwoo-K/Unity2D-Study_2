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

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                GameObject clone = Instantiate(nodePrefab, nodeParent);
            }
        }
    }
}
