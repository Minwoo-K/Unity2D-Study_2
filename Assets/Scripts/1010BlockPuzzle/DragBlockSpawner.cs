using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dragBlockPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private void Awake()
    {
        SpawnDragBlocks();
    }

    public void SpawnDragBlocks()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            int random = Random.Range(0, dragBlockPrefabs.Length - 1);

            Instantiate(dragBlockPrefabs[random], spawningPoints[i].position, Quaternion.identity, spawningPoints[i]);
        }
    }
}
