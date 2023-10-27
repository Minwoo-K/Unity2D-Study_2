using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dragBlocksPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private void Awake()
    {
        StartCoroutine(SpawnBlocks());
    }

    private IEnumerator SpawnBlocks()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            yield return new WaitForSeconds(0.1f);

            int random = Random.Range(0, dragBlocksPrefabs.Length - 1);

            Instantiate(dragBlocksPrefabs[random], spawningPoints[i].position, Quaternion.identity, spawningPoints[i]);
        }
    }
}
