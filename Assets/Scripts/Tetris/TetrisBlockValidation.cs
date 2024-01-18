using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class TetrisBlockValidation : MonoBehaviour
    {
        private Block[] theBoard;
        public Vector2Int boardCount { get; private set; }

        public void Initialized(Block[] theBoard, Vector2Int boardCount)
        {
            this.theBoard = theBoard;
            this.boardCount = boardCount;
        }

        // To figure out whether the blocks on the board, under the TetrisBlock are empty or filled
        public bool IsEmptyUnder(TetrisBlock tetrisBlock)
        {
            // To figure out which Block object(s) at the very bottom of this TetrisBlock
            float lowestY = tetrisBlock.transform.GetChild(0).position.y;
            for (int i = 1; i < tetrisBlock.transform.childCount; i++)
            {
                float y = tetrisBlock.transform.GetChild(i).position.y;
                lowestY = lowestY > y ? y : lowestY;
            }

            for (int j = 0; j < tetrisBlock.transform.childCount; j++)
            {
                Vector3 position = tetrisBlock.transform.GetChild(j).position;
                if (position.y > lowestY) continue;

                // Row below the lowest block
                Vector3 below = position + Vector3.down;

                int index = (int)(below.x + (boardCount.x / 2f - 0.5f) + (below.y * boardCount.x));
                if (below.y < 0 || theBoard[index].IsFilled)
                {
                    return false;
                }
            }

            return true;
        }

        // Fill the board where the TetrisBlock object lands
        public void FillTheBoardWith(TetrisBlock tetrisBlock)
        {
            Block[] blocks = tetrisBlock.GetComponentsInChildren<Block>();

            foreach ( Block block in blocks )
            {
                Vector3 position = block.transform.position;
                int x = (int)(position.x + (boardCount.x / 2f - 0.5f));
                theBoard[(int)(position.y * boardCount.y + position.x)].FillIt(tetrisBlock.Color);
            }
        }
    }
}