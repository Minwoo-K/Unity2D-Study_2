using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Square
{
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

            for (int y = 0; y < blockCount.y; y++)
            {
                for (int x = 0; x < blockCount.x; x++)
                {
                    // Spawn a Node object of the Prefab
                    GameObject clone = Instantiate(nodePrefab, nodeParent);
                    // Rename the Node with a coordinate
                    clone.name = $"Node[ {x}, {y} ]";
                    // Fetch Node component
                    Node node = clone.GetComponent<Node>();
                    // Initialize the Node component
                    Vector2Int coor = new Vector2Int(x, y);
                    node.Initialized(coor);
                    // Add the Node component to the List
                    nodeList.Add(node);
                }
            }

            return nodeList;

        }
    }

}
