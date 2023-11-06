using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPuzzleManager : MonoBehaviour
{
    [SerializeField]
    private BackgroundBlockSpawner backgroundBlockSpawner;
    [SerializeField]
    private BackgroundBlockSpawner foregroundBlockSpawner;
    [SerializeField]
    private DragBlockSpawner dragBlockSpawner;

    private BlockDeploymentSystem blockDeploymentSystem;
    private BlockSlot[] theBlockBoard;
    private int dragBlockCount;
    private List<BlockSlot> filledBlocks;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);
    private readonly int maxDragBlockCount = 3;

    private void Awake()
    {
        filledBlocks = new List<BlockSlot>();

        backgroundBlockSpawner.SpawnBlockBoard(blockCount, blockHalf);

        theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];
        theBlockBoard = foregroundBlockSpawner.SpawnBlockBoard(blockCount, blockHalf);

        blockDeploymentSystem = GetComponent<BlockDeploymentSystem>();
        blockDeploymentSystem.Initialized(this, theBlockBoard, blockCount, blockHalf);

        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        dragBlockCount = maxDragBlockCount;

        dragBlockSpawner.CreateDragBlocks();
    }

    private int BlockLineValidation()
    {
        int lineToRemove = 0;

        // Block Line Validation Check
        filledBlocks.Clear();

        // Horizontal (X axis)
        for (int y = 0; y < blockCount.y; y++)
        {
            int lineFilledCounts = 0;

            for (int x = 0; x < blockCount.x; x++)
            {
                if (theBlockBoard[y * blockCount.x + x].IsFilled) lineFilledCounts++;
            }

            if (lineFilledCounts == blockCount.x)
            {
                lineToRemove++;
                for (int x = 0; x < blockCount.x; x++)
                {
                    filledBlocks.Add(theBlockBoard[y * blockCount.x + x]);
                }
            }
        }

        // Vertical (Y axis)
        for (int y = 0; y < blockCount.y; y++)
        {
            int lineFilledCounts = 0;

            for (int x = 0; x < blockCount.x; x++)
            {
                if (theBlockBoard[blockCount.x * x + y].IsFilled) lineFilledCounts++;
            }

            if (lineFilledCounts == blockCount.y)
            {
                lineToRemove++;
                for (int x = 0; x < blockCount.x; x++)
                {
                    filledBlocks.Add(theBlockBoard[blockCount.x * x + y]);
                }
            }
        }

        return lineToRemove;
    }

    public void AfterBlockDeployment(DragBlock dragBlock)
    {
        StartCoroutine(OnAfterBlockDeployment(dragBlock));
    }

    private IEnumerator OnAfterBlockDeployment(DragBlock dragBlock)
    {
        int lineToRemove = BlockLineValidation();

        Destroy(dragBlock.gameObject);

        dragBlockCount--;

        if ( lineToRemove != 0 )
        {
            yield return StartCoroutine(RemoveFilledLine());
        }

        if ( dragBlockCount == 0 )
        {
            SpawnDragBlocks();
        }
    }

    private IEnumerator RemoveFilledLine()
    {
        foreach (BlockSlot block in filledBlocks)
        {
            block.GetEmpty();

            yield return new WaitForSeconds(0.01f);
        }

        filledBlocks.Clear();
    }
}
