using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
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

        public bool TryPlaceBlock(DragBlock dragBlock)
        {
            // Check each child block with the boolean validations
            for (int i = 0; i < dragBlock.ChildBlockPositions.Length; i++)
            {
                Vector3 position = dragBlock.transform.position + dragBlock.ChildBlockPositions[i];
                if (IsBlockOutsideMap(position)) return false;

                if (IsBlockFilled(position)) return false;
            }

            // At this point, the validation has been passed
            // Fill the spots with the dragBlock
            for (int i = 0; i < dragBlock.ChildBlockPositions.Length; i++)
            {
                Vector3 position = dragBlock.transform.position + dragBlock.ChildBlockPositions[i];
                int index = GetIndexFromPosition(position);

                theBlockBoard[index].GetFilled(dragBlock.Color);
            }

            blockPuzzleManager.CommandAfterBlockPlacement(dragBlock);

            return true;
        }

        public bool IsBlockOutsideMap(Vector3 position)
        {
            float maxValue = blockCount.y / 2f - blockHalf.y;
            float minValue = -blockCount.x / 2f + blockHalf.x;

            if (position.x > maxValue || position.x < minValue || position.y > maxValue || position.y < minValue)
            {
                return true;
            }

            return false;
        }

        public bool IsBlockFilled(Vector3 position)
        {
            int index = GetIndexFromPosition(position);

            if (theBlockBoard[index].IsFilled) return true;

            return false;
        }

        private int GetIndexFromPosition(Vector3 position)
        {
            float x = blockCount.x / 2f - blockHalf.x + position.x;
            float y = blockCount.y / 2f - blockHalf.y - position.y;

            return (int)(y * blockCount.x + x);
        }
    }
}
