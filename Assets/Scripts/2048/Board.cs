using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { Standby, Processing, End }

public class Board : MonoBehaviour
{
    [SerializeField]
    private TouchController touchController;
    [SerializeField]
    private NodeSpawner nodeSpawner;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private Transform blockParent;

    public List<Node> NodeList { get; private set; }
    public Vector2Int BlockCount { get; private set; }

    private List<Block> blockList;

    private State state = State.Standby;

    private void Awake()
    {
        BlockCount = new Vector2Int(4, 4);

        NodeList = nodeSpawner.SpawnNodes(this, BlockCount);

        blockList = new List<Block>();
    }

    private void Start()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());

        foreach (Node node in NodeList)
        {
            node.localPosition = node.GetComponent<RectTransform>().localPosition;
        }

        SpawnBlockAtRandomNode();
        SpawnBlockAtRandomNode();
    }

    private void Update()
    {
        // Only during Standby state, accept the input(touch)
        if ( state == State.Standby )
        {
            Direction direction = touchController.UpdateTouch();

            if ( direction != Direction.None )
            {
                ProcessAllBlocks(direction);
            }
        }
        else
        {
            UpdateState();
        }
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

        blockList.Add(block);
    }

    private void Move(Node from, Node to)
    {
        from.blockInfo.MoveToNode(to);

        if (from.blockInfo != null)
        {
            to.blockInfo = from.blockInfo;

            from.blockInfo = null;
        }
    }

    private void BlockProcess(Node node, Direction direction)
    {
        if (node.blockInfo == null) return;

        Node neighbourNode = node.FindTargetInDirection(node, direction);
        if (neighbourNode != null)
        {
            if (neighbourNode != null && neighbourNode.blockInfo == null)
            {
                Move(node, neighbourNode);
            }
        }
    }

    private void ProcessAllBlocks(Direction direction)
    {
        switch(direction)
        {
            case Direction.Up:
                for ( int y = 1; y < BlockCount.y; y++ )
                {
                    for ( int x = 0; x < BlockCount.x; x++ )
                    {
                        BlockProcess(NodeList[y * BlockCount.x + x], direction);
                    }
                }
                break;
            case Direction.Right:
                for (int y = 0; y < BlockCount.y; y++)
                {
                    for (int x = BlockCount.x-2; x >= 0 ; x--)
                    {
                        BlockProcess(NodeList[y * BlockCount.x + x], direction);
                    }
                }
                break;
            case Direction.Down:
                for (int y = BlockCount.y - 2; y >= 0; y--)
                {
                    for (int x = 0; x < BlockCount.x; x++)
                    {
                        BlockProcess(NodeList[y * BlockCount.x + x], direction);
                    }
                }
                break;
            case Direction.Left:
                for (int y = 0; y < BlockCount.y; y++)
                {
                    for (int x = 1; x < BlockCount.x; x++)
                    {
                        BlockProcess(NodeList[y * BlockCount.x + x], direction);
                    }
                }
                break;
        }

        foreach ( Block block in blockList )
        {
            if ( block.Target != null )
            {
                state = State.Processing;
                block.StartMoving();
            }
        }
    }

    /// <summary>
    /// Called after Blocks' movement and integration
    /// </summary>
    private void UpdateState()
    {
        // If all targets are null, that means all of them have been processed
        // which is true. If any of them is in process, false
        bool allTargetNull = true;

        foreach ( Block block in blockList )
        {
            if ( block.Target != null )
            {
                allTargetNull = false;
                break;
            }
        }

        if ( allTargetNull == true && state == State.Processing )
        {
            state = State.End;
        }

        if ( state == State.End )
        {
            state = State.Standby;

            SpawnBlockAtRandomNode();
        }
    }
}
