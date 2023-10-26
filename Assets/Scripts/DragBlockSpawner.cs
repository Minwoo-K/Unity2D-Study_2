using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] dragBlocksPrefabs;
    [SerializeField]
    private Transform[] spawningPoints;

    private Vector3 gapToParent = new Vector3(10, 0, 0);

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

            GameObject clone = Instantiate(dragBlocksPrefabs[random], spawningPoints[i].position + gapToParent, Quaternion.identity, spawningPoints[i]);

            clone.GetComponent<SpawningBlockAnimation>().PlayAnimation(spawningPoints[i].position);
        }
    }
}
