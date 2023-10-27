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
        StartCoroutine(SpawnDragBlocks());
    }

    private IEnumerator SpawnDragBlocks()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            yield return new WaitForSeconds(0.2f);

            int random = Random.Range(0, dragBlockPrefabs.Length - 1);
            GameObject clone = Instantiate(dragBlockPrefabs[random], spawningPoints[i].position, Quaternion.identity, spawningPoints[i]);
        }
    }
}
