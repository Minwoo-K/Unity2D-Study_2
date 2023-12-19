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
        NodeList = nodeSpawner.SpawnNodes(this, BlockCount);
    }
    private void Start()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());

        foreach (Node node in NodeList)
        {
            node.localPosition = node.GetComponent<RectTransform>().localPosition;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("1")) SpawnBlockAtRandomNode();
    }

    private void SpawnBlockAtRandomNode()
    {
        List<Node> emptyNodes = NodeList.FindAll(node => node.blockInfo == null);

        if (emptyNodes.Count != 0)
        {
            int index = Random.Range(0, emptyNodes.Count);
            Node node = emptyNodes[index];

            SpawnBlock(node.Coordinate);
        }
        else
        {
            // Game Over check
        }
    }

    private void SpawnBlock(Vector2Int coordinate)
    {
        if ( NodeList[coordinate.y * BlockCount.x + coordinate.x].blockInfo != null ) return;

        Node node = NodeList[coordinate.y * BlockCount.x + coordinate.x];
        GameObject clone = Instantiate(blockPrefab, blockParent);
        Block block = clone.GetComponent<Block>();

        clone.GetComponent<RectTransform>().localPosition = node.localPosition;

        block.Initialized();
        node.blockInfo = block;
    }
}
