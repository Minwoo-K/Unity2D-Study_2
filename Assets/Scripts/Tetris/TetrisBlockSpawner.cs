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
        
    }

    public void SpawnTetrisBlock()
    {
        int random = Random.Range(0, tetrisBlockPrefabs.Length);
    }
}
