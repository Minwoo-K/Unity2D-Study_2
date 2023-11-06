using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDeploymentSystem : MonoBehaviour
{
    private BlockPuzzleManager blockPuzzleManager;
    private BlockSlot[] theBlockBoard;
    private Vector2Int  blockCounts;
    private Vector2     blockHalf;

    public void Initialized(BlockPuzzleManager blockPuzzleManager, BlockSlot[] theBlockBoard, Vector2Int blockCounts, Vector2 blockHalf)
    {
        this.blockPuzzleManager = blockPuzzleManager;
        this.theBlockBoard = theBlockBoard;
        this.blockCounts = blockCounts;
        this.blockHalf = blockHalf;
    }

    public bool TryDeployBlock(DragBlock dragBlock)
    {
        for ( int i = 0; i < dragBlock.ChildBlockPositions.Length; i++ )
        {
            Vector3 position = dragBlock.transform.position + dragBlock.ChildBlockPositions[i];

            if ( IsBlockInTheBoard(position) == false ) return false;       // Deployment Failed

            if ( IsEmptyOnTheSpot(position) == false ) return false;    // Deployment Failed
        }

        for ( int i = 0; i < dragBlock.ChildBlockPositions.Length; i++ )
        {
            Vector3 position = dragBlock.transform.position + dragBlock.ChildBlockPositions[i];

            int index = PositionToIndex(position);

            theBlockBoard[index].GetFilled(dragBlock.Color);
        }

        blockPuzzleManager.AfterBlockDeployment(dragBlock);

        return true;
    }

    private int PositionToIndex(Vector3 position)
    {
        // The below is the formulas when getting position X & Y for a BlockSlot
        //      float positionX = -blockCount.x * 0.5f + blockHalf.x + x;
        //      float positionY = blockCount.y * 0.5f - blockHalf.y - y;
        // Based on the formulas, we can get equations for x & y on the BlockBoard

        float x = blockCounts.x * 0.5f - blockHalf.x + position.x;
        float y = blockCounts.y * 0.5f - blockHalf.y - position.y;

        // index formula is [y * blockCount.x + x]
        return (int)(y * blockCounts.x + x);
    }

    private bool IsBlockInTheBoard(Vector3 position)
    {
        if ( position.x < -blockCounts.x * 0.5f + blockHalf.x || position.x > blockCounts.x * 0.5f - blockHalf.x ||
             position.y > blockCounts.y * 0.5f - blockHalf.y || position.y < -blockCounts.y * 0.5f + blockHalf.y )
        {
            return false; // Validation Failed
        }

        return true;    // Validation Success
    }

    private bool IsEmptyOnTheSpot(Vector3 position)
    {
        int index = PositionToIndex(position);

        if ( theBlockBoard[index].IsFilled == true )
        {
            Debug.Log("The Spot not empty");
            return false; // Validation Failed
        }

        return true;   // Validation Success
    }
}
