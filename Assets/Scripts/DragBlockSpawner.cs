using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private DragBlock[] dragBlockPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private Vector3 spawningPosition = new Vector3(10, 0, 0);

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
            GameObject clone = Instantiate(dragBlockPrefabs[random].gameObject, spawningPosition, Quaternion.identity, spawningPoints[i]);
            DragBlock dragBlock = clone.GetComponent<DragBlock>();
            dragBlock.Initialized();
        }
    }
}
