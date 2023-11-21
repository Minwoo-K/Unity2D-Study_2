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

    private BlockSlot[] theBlockBoard;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2 blockHalf = new Vector2(0.5f, 0.5f);

    private void Awake()
    {
        backgroundBlockCreator.CreateBlockBoard();

        blockPlacementValidation.Initialized(blockCount, blockHalf);

        theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];

        theBlockBoard = theBlockBoardCreator.CreateBlockBoard();
    }
}
