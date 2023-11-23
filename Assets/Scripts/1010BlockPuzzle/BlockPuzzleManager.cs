using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private BlockBoardCreator backgroundBlockCreator;
    [SerializeField]
    private BlockBoardCreator theBlockBoardCreator;
    [SerializeField]
    private BlockPlacementValidation blockPlacementValidation;
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;

    private BlockSlot[] theBlockBoard;
    private int dragBlockCount;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlocks = 3;

    private void Awake()
    {
        backgroundBlockCreator.Initialized(blockCount, blockHalf);
        backgroundBlockCreator.CreateBlockBoard();

        theBlockBoardCreator.Initialized(blockCount, blockHalf);
        theBlockBoard = theBlockBoardCreator.CreateBlockBoard();

        blockPlacementValidation.Initialized(this, theBlockBoard, blockCount, blockHalf);

        theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];

        theBlockBoardCreator.Initialized(blockCount, blockHalf);

        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlocks;

        dragBlockSpawner.SpawnDragBlocks();
    }

    public void CommandAfterBlockPlacement(DragBlock dragBlock)
    {
        StartCoroutine(AfterBlockPlacement(dragBlock));
    }

    private IEnumerator AfterBlockPlacement(DragBlock dragBlock)
    {
        Destroy(dragBlock);

        dragBlockCount--;

        if ( dragBlockCount == 0 )
        {
            SpawnDragBlocks();
        }

        yield return null;
    }
}
