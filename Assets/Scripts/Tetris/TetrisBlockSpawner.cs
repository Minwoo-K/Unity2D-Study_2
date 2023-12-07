using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetrisBlockPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    public Transform[] SpawningPoints { get => spawningPoints; }

    private void Awake()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            SpawnTetrisBlock(spawningPoints[i]);
        }
    }

    public void SpawnTetrisBlock(Transform spawningPoint)
    {
        // Generate a random number to randomly spawn a TetrisBlock
        int random = Random.Range(0, tetrisBlockPrefabs.Length);
        // Spawn a TetrisBlock with the random number
        GameObject clone = Instantiate(tetrisBlockPrefabs[random], spawningPoint);
    }
}
