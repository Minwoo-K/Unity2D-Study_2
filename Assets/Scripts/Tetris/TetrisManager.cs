using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("Core Objects")]
    [SerializeField]
    private Transform[] blockSpawningPoints;        // Random Spawning Points in the board
    [SerializeField]
    private TetrisBlockSpawner tetrisBlockSpawner;  // TetrisBlockSpawner Component

    private List<TetrisBlock> nextBlocks;               // Next Blocks on the right panel
    private readonly int tetrisBlockCount = 3;      // Maximum Count of Next Blocks standing by

    // Game Start
    private void Start()
    {
        nextBlocks = new List<TetrisBlock>();
        for ( int i = 0; i < tetrisBlockCount; i++ )
        {
            nextBlocks.Add(tetrisBlockSpawner.SpawnTetrisBlock(tetrisBlockSpawner.SpawningPoints[i]));
        }

        PlaceNextTetrisBlock();
    }

    private void PlaceNextTetrisBlock()
    {
        // Get a random number from the spawning points in the board
        int random = Random.Range(0, blockSpawningPoints.Length);
        // 
        nextBlocks[0].transform.SetParent(blockSpawningPoints[random], false);
        //
        nextBlocks.RemoveAt(0);
        //
        
    }
}
