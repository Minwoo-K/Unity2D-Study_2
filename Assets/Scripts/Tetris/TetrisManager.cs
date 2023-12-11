using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("Core Objects")]
    [SerializeField]
    private Transform[] inBoardSpawningPoints; // Random Spawning Points in the board
    [SerializeField]
    private Transform[] standBySpawningPoints; // Random Spawning Points on the right panel
    [SerializeField]
    private TetrisBlockSpawner tetrisBlockSpawner;  // TetrisBlockSpawner Component

    private List<TetrisBlock> nextBlocks;           // Next Blocks on the right panel
    private readonly int tetrisBlockCount = 3;      // Maximum Count of Next Blocks standing by

    // Game Start
    private void Start()
    {
        nextBlocks = new List<TetrisBlock>();
        for ( int i = 0; i < tetrisBlockCount; i++ )
        {
            nextBlocks.Add(tetrisBlockSpawner.SpawnTetrisBlock(standBySpawningPoints[i]));
        }

    }

    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.S))
            PlaceNextTetrisBlock();
    }

    private void PlaceNextTetrisBlock()
    {
        // Get a random number from the spawning points in the board
        int random = Random.Range(0, inBoardSpawningPoints.Length);
        // 
        nextBlocks[0].transform.SetParent(inBoardSpawningPoints[random], false);
        //
        nextBlocks.RemoveAt(0);
        //
        nextBlocks[0].transform.SetParent(standBySpawningPoints[0], false);
        nextBlocks[1].transform.SetParent(standBySpawningPoints[1], false);
        nextBlocks.Add(tetrisBlockSpawner.SpawnTetrisBlock(standBySpawningPoints[2]));
    }
}
