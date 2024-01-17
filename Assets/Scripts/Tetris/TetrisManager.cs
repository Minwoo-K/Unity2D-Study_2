using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class TetrisManager : MonoBehaviour
    {
        [Header("Core Objects")]
        [SerializeField]
        private Transform[] inBoardSpawningPoints; // Random Spawning Points on THE BOARD
        [SerializeField]
        private Transform[] standBySpawningPoints; // Random Spawning Points on the RIGHT PANEL
        [SerializeField]
        private TetrisBlockSpawner tetrisBlockSpawner;  // TetrisBlockSpawner Component

        private List<TetrisBlock> nextBlocks;           // Next Blocks on the right panel
        private readonly int tetrisBlockCount = 3;      // Maximum Count of Next Blocks standing by

        // Game Start
        private void Start()
        {
            nextBlocks = new List<TetrisBlock>();
            for (int i = 0; i < tetrisBlockCount; i++)
            {
                nextBlocks.Add(tetrisBlockSpawner.SpawnTetrisBlock(standBySpawningPoints[i]));
            }
        }

        private void Update()
        {
            // Debugging purpose
            if (Input.GetKeyDown(KeyCode.S))
                PlaceNextTetrisBlock();
        }

        // Place the Next Tetris in the queue onto the board
        private void PlaceNextTetrisBlock()
        {
            // Get a random number from the spawning points in the board
            int random = Random.Range(0, inBoardSpawningPoints.Length);
            // The first next block placed onto the random inBoardSpawningPoints in the board
            nextBlocks[0].transform.SetParent(inBoardSpawningPoints[random], false);
            // Call the function to see if the block has landed in the field
            BlockLanded(nextBlocks[0]);
            // Remove the TetrisBlock from the list
            nextBlocks.RemoveAt(0);
            // Rearrange the blocks with their Parent Transforms
            nextBlocks[0].transform.SetParent(standBySpawningPoints[0], false);
            nextBlocks[1].transform.SetParent(standBySpawningPoints[1], false);
            // Spawn the next block as one was used
            nextBlocks.Add(tetrisBlockSpawner.SpawnTetrisBlock(standBySpawningPoints[2]));
        }

        private bool BlockLanded(TetrisBlock block)
        {
            // Activate the moving funtion within the board
            block.OnBoard();

            return true;
        }
    }
}
