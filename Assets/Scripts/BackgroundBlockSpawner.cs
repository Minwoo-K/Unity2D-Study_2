using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private int orderInLayer;

    public BlockSlot[] SpawnBlockBoard(Vector2Int blockCount, Vector2 blockHalf)
    {
        BlockSlot[] blockBoard = new BlockSlot[blockCount.x * blockCount.y];

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for (int x = 0; x < blockCount.x; x++)
            {
                float positionX = -blockCount.x * 0.5f + blockHalf.x + x;
                float positionY = blockCount.y * 0.5f - blockHalf.y - y;
                Vector3 position = new Vector3(positionX, positionY);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;

                blockBoard[y * blockCount.x + x] = clone.GetComponent<BlockSlot>();
            }
        }

        return blockBoard;
    }
}
