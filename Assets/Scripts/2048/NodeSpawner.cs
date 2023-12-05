using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform nodeParent;

    private void Start()
    {

    }

    public List<Node> SpawnNodes(Vector2Int blockCount)
    {
        List<Node> nodeList = new List<Node>();

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++)
            {
                GameObject clone = Instantiate(nodePrefab, nodeParent);

                clone.name = $"Node[ {x}, {y} ]";

                Node node = clone.GetComponent<Node>();

                node.Initialized(new Vector2Int(x, y));

                nodeList.Add(node);
            }
        }

        return nodeList;

    }
}
