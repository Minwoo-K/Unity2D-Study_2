using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private DragBlock[] dragBlockPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private Vector3 spawningGap = new Vector3(10, 0, 0);

    public void SpawnDragBlocksCommand()
    {
        SpawnDragBlocks();
    }

    private void SpawnDragBlocks()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            int random = Random.Range(0, dragBlockPrefabs.Length - 1);
            Vector3 spawningPosition = spawningPoints[i].position + spawningGap;
            GameObject clone = Instantiate(dragBlockPrefabs[random].gameObject, spawningPosition, Quaternion.identity, spawningPoints[i]);
            DragBlock dragBlock = clone.GetComponent<DragBlock>();
            dragBlock.Initialized(spawningPoints[i].position);
        }
    }
}
