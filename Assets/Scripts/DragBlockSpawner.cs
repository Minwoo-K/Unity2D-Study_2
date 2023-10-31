using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dragBlockPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private Vector3 gapFromSpawningPoint = new Vector3(10, 0, 0);

    private void Awake()
    {
        StartCoroutine(SpawnDragBlocks());
    }

    private IEnumerator SpawnDragBlocks()
    {
        for ( int i = 0; i < spawningPoints.Length; i++ )
        {
            yield return new WaitForSeconds(0.1f);

            int random = Random.Range(0, dragBlockPrefabs.Length - 1);
            
            GameObject clone = Instantiate(dragBlockPrefabs[random], spawningPoints[i].position + gapFromSpawningPoint, Quaternion.identity, spawningPoints[i]);
            
            clone.GetComponent<DragBlock>().PlayAnimation(spawningPoints[i].position);
        }
    }
}
