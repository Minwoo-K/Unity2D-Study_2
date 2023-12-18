using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private NodeSpawner nodeSpawner;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Transform blockParent;

    public List<Node> NodeList { get; private set; }
    public Vector2Int BlockCount { get; private set; }

    private void Awake()
    {
        BlockCount = new Vector2Int(4, 4);

        NodeList = new List<Node>();
        NodeList = nodeSpawner.SpawnNodes(BlockCount);
    }
    private void Start()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());
    }

    private void SpawnBlockAtRandomNode()
    {
        List<Node> emptyNodes = NodeList.FindAll(node => node.blockInfo == null);

        if (emptyNodes.Count != 0)
        {
            int index = Random.Range(0, emptyNodes.Count);
            // To-Do: Figure out a coordinate of the Node to spawn a block
            Node node = emptyNodes[index];

            node.blockInfo = SpawnBlock(node.Coordinate);
        }
        else
        {
            // Game Over check
        }
    }

    // To-Do: Write a function that spawns a block based on a coordinate value
    private Block SpawnBlock(Vector2Int coordinate)
    {
        GameObject clone = Instantiate(blockPrefab, blockParent);

        Block block = clone.GetComponent<Block>();

        block.Initialized();

        return block;
    }
}
