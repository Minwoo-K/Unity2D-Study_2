using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform nodeParent;

    public List<Node> SpawnNodes(Board board, Vector2Int blockCount)
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
                // Get the Coordinate value 
                Vector2Int coordinate = new Vector2Int(x, y);

                // Create a nullable array for NeighbourNodes of this Node
                Vector2Int?[] neighbourNodes = new Vector2Int?[4];
                // Get the 4 Nodes in 4 different directions
                Vector2Int up = coordinate + Vector2Int.down;
                Vector2Int right = coordinate + Vector2Int.right;
                Vector2Int down = coordinate + Vector2Int.up;
                Vector2Int left = coordinate + Vector2Int.left;
                // Check if the neighbour Nodes are within the board
                if (IsValidNode(up, blockCount)) neighbourNodes[0] = up;
                if (IsValidNode(right, blockCount)) neighbourNodes[1] = right;
                if (IsValidNode(down, blockCount)) neighbourNodes[2] = down;
                if (IsValidNode(left, blockCount)) neighbourNodes[3] = left;

                // Initialize the Node upon spawning
                node.Initialized(board, neighbourNodes, coordinate);

                // Add the Node to the List
                nodeList.Add(node);
            }
        }
        // return the List of Nodes
        return nodeList;
    }

    private bool IsValidNode(Vector2Int position, Vector2Int blockCount)
    {
        if (position.x == -1 || position.x == blockCount.x || position.y == -1 || position.y == blockCount.y)
        {
            return false;
        }

        return true;
    }
}
