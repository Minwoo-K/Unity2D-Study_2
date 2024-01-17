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

        private void Awake()
        {
            // Set Numbers for the Board
            boardCount = new Vector2Int(10, 20);
            // Create the Board based on the counts
            CreateBoard();
        }

        public void CreateBoard()
        {
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
                }
            }
        }
    }
}