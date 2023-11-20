using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private int orderInLayer;

    private readonly Vector2Int blockCount = new Vector2Int(10, 10);
    private readonly Vector2    blockHalf = new Vector2(0.5f, 0.5f);

    private void Awake()
    {
        CreateBlockBoard();
    }

    public void CreateBlockBoard()
    {
        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                float positionX = -blockCount.x / 2f + blockHalf.x + x;
                float positionY =  blockCount.y / 2f - blockHalf.y - y;
                Vector3 position = new Vector3(positionX, positionY);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }
    }
}
