using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementValidation : MonoBehaviour
{
    private BlockPuzzleManager blockPuzzleManager;
    private BlockSlot[] theBlockBoard;
    private Vector2Int blockCount;
    private Vector2 blockHalf;

    public void Initialized(BlockPuzzleManager blockPuzzleManager, BlockSlot[] theBlockBoard, Vector2Int blockCount, Vector2 blockHalf)
    {
        this.blockPuzzleManager = blockPuzzleManager;
        this.theBlockBoard = theBlockBoard;
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
    }

    public bool TryPlaceDragBlock(DragBlock dragBlock)
    {
        for ( int i = 0; i < dragBlock.ChildBlockPositions.Length; i++ )
        {
            Vector3 position = dragBlock.transform.position + dragBlock.ChildBlockPositions[i];

            // Validate whether there's another block on the spot
            if ( IsBlockOutsideTheMap(position) ) return false;
            // Validate whether the block was placed within the map
            if ( IsOtherBlockOnSpot(position) ) return false;
        }

        // At this point, placement was a success
        for ( int i = 0; i < dragBlock.ChildBlockPositions.Length; i++ )
        {
            Vector3 position = dragBlock.transform.position + dragBlock.ChildBlockPositions[i];

            int index = GetIndexFromPosition(position);
            theBlockBoard[index].GetFilled(dragBlock.Color);
        }

        blockPuzzleManager.AfterBlockPlacement(dragBlock);

        return true;
    }

    public bool IsBlockOutsideTheMap(Vector3 position)
    {
        if ( position.x < -blockCount.x / 2f + blockHalf.x || blockCount.x / 2f - blockHalf.x < position.x ||
             position.y < -blockCount.y / 2f + blockHalf.y || blockCount.y / 2f - blockHalf.y < position.y)
        {
            return true;
        }

        return false;
    }

    public bool IsOtherBlockOnSpot(Vector3 position)
    {
        int index = GetIndexFromPosition(position);

        if ( theBlockBoard[index].IsFilled ) return true;

        return false;
    }

    public bool IsPossibleToPlaceBlock(DragBlock dragBlock)
    {

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                // To count how many blocks can be put at a time within the DragBlock
                int count = 0;
                // Figure out the position to check
                Vector3 position = new Vector3(-blockCount.x / 2f + blockHalf.x + x, blockCount.y / 2f - blockHalf.y - y, 0);
                // if even count blocks(x==2 or y==2), neat number without decimal point([-4,4], [-3,3] ...)
                // if odd count blocks (x==1 or y==3), a number with decimal point([-4.5, 4.5], [-3.5, 4.5] ...) to check available spots
                position.x = dragBlock.blockCount.x % 2 == 0 ? dragBlock.blockCount.x + 0.5f : dragBlock.blockCount.x;
                position.y = dragBlock.blockCount.y % 2 == 0 ? dragBlock.blockCount.y - 0.5f : dragBlock.blockCount.y;

                // Check each block can be placed. If any one of them isn't, break the loop to go to the next spot
                for ( int i = 0; i < dragBlock.ChildBlockPositions.Length; i++ )
                {
                    Vector3 oneBlockPosition = position + dragBlock.ChildBlockPositions[i];

                    if (IsBlockOutsideTheMap(oneBlockPosition)) break;

                    if (IsOtherBlockOnSpot(oneBlockPosition)) break;

                    count++;
                }

                // If count is equal to the number of blocks the DragBlock has, that means it can be placed within the board
                if ( count == dragBlock.ChildBlockPositions.Length ) return true;
            }
        }

        return false;
    }

    private int GetIndexFromPosition(Vector3 position)
    {
        float x = blockCount.x / 2f - blockHalf.x + position.x;
        float y = blockCount.y / 2f - blockHalf.y - position.y;

        return (int)(y * blockCount.x + x);
    }
}
