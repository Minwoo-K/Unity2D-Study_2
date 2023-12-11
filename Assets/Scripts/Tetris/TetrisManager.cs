using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisManager : MonoBehaviour
{
    [Header("Core Objects")]
    [SerializeField]
    private Transform[] blockSpawningPoints;
    [SerializeField]
    private TetrisBlockSpawner tetrisBlockSpawner;

    private TetrisBlock[] nextBlocks;
    private readonly int tetrisBlockCount = 3;

    private void Start()
    {
        nextBlocks = new TetrisBlock[tetrisBlockCount];
        for ( int i = 0; i < tetrisBlockCount; i++ )
        {
            nextBlocks[i] = tetrisBlockSpawner.SpawnTetrisBlock(tetrisBlockSpawner.SpawningPoints[i]);
        }
    }
}
