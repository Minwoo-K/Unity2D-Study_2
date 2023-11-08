using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private DragBlock[] dragBlockPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private void Start()
    {
        SpawnDragBlocksCommand();
    }

    public void SpawnDragBlocksCommand()
    {
        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            int random = Random.Range(0, dragBlockPrefabs.Length - 1);
            Instantiate(dragBlockPrefabs[random].gameObject, spawningPoints[i].position, Quaternion.identity, spawningPoints[i]);
        }
    }
}
