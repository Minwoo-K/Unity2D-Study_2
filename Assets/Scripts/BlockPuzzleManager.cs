using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundBlockSpawner  backgroundBlockSpawner;
    [SerializeField]
    private BackgroundBlockSpawner  foregroundBlockSpawner;
    [SerializeField]
    private DragBlockSpawner        dragBlockSpawner;
    [SerializeField]
    private BlockDeploymentSystem   blockDeploymentSystem;

    private BlockSlot[] theBlockBoard;
    private int dragBlockCount;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlockCount = 3;

    private void Awake()
    {
        backgroundBlockSpawner.SpawnBlockBoard(blockCount, blockHalf);

        theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];
        theBlockBoard = foregroundBlockSpawner.SpawnBlockBoard(blockCount, blockHalf);

        SpawnDragBlocks();

        blockDeploymentSystem = GetComponent<BlockDeploymentSystem>();
        blockDeploymentSystem.Initialized();
    }

    private void SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlockCount;

        dragBlockSpawner.CreateDragBlocks();
    }
}
