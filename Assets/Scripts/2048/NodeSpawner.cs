using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform nodeParent;

    public List<Node> SpawnNodes(Vector2Int blockCount)
    {
        List<Node> nodeList = new List<Node>();

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                // Spawn a Node of the prefab
                GameObject clone = Instantiate(nodePrefab, nodeParent);
                // Name the Node object as its coordinate
                clone.name = $"Node[ {x}, {y} ]";
                // Fetch Node component and initialize it
                Node node = clone.GetComponent<Node>();
                node.Initialized(new Vector2Int(x, y));
                // Add the Node to the List
                nodeList.Add(node);
            }
        }
        // return the List of Nodes
        return nodeList;
    }
}
