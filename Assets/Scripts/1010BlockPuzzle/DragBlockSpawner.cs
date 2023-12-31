using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class DragBlockSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject[] dragBlockPrefabs;
        [SerializeField]
        private Transform[] spawningPoints;
        [SerializeField]
        private BlockPlacementValidation blockPlacementValidation;

        private Vector3 spawningGap = new Vector3(10, 0, 0);

        public Transform[] SpawningPoints { get => spawningPoints; }

        public void SpawnDragBlocks()
        {
            for (int i = 0; i < spawningPoints.Length; i++)
            {
                int random = Random.Range(0, dragBlockPrefabs.Length - 1);

                Vector3 spawningPosition = spawningPoints[i].position + spawningGap;

                GameObject clone = Instantiate(dragBlockPrefabs[random], spawningPosition, Quaternion.identity, spawningPoints[i]);

                clone.GetComponent<DragBlock>().Initialized(blockPlacementValidation, clone.transform.parent.position);
            }
        }
    }
}
