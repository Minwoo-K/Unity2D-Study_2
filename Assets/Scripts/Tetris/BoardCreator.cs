using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class BoardCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject blockPrefab;     // Block Prefab
        [SerializeField]
        private int orderInLayer;           // Order in Layer

        private Vector2Int boardCount;      // The (x, y) Board Count

        public void Initialized(Vector2Int boardCount)
        {
            this.boardCount = boardCount;
        }

        public Block[] CreateBoard()
        {
            Block[] theBoard = new Block[boardCount.x * boardCount.y];

            for ( int y = 0; y < boardCount.y; y++ )
            {
                for ( int x = 0; x < boardCount.x; x++ )
                {
                    // Set a position for a block to spawn at
                    float positionX = -boardCount.x / 2f + 0.5f + x;
                    Vector2 position = new Vector2(positionX, y);
                    // Create/Spawn the Block on the position
                    GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                    // Set the sortingOrder
                    clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
                    // Save the Block object into theBoard
                    theBoard[y * boardCount.x + x] = clone.GetComponent<Block>();
                }
            }

            return theBoard;
        }
    }
}