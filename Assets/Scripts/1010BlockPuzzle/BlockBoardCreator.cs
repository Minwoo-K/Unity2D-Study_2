using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private int orderInLayer;


    private Vector2Int  blockCount;
    private Vector2     blockHalf;

    public void Initialized(Vector2Int blockCount, Vector2 blockHalf)
    {
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;

        CreateBlockBoard();
    }

    public BlockSlot[] CreateBlockBoard()
    {
        BlockSlot[] theBlockBoard = new BlockSlot[blockCount.x * blockCount.y];

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                float positionX = -blockCount.x / 2f + blockHalf.x + x;
                float positionY =  blockCount.y / 2f - blockHalf.y - y;
                Vector3 position = new Vector3(positionX, positionY);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                theBlockBoard[y * blockCount.x + x] = clone.GetComponent<BlockSlot>();

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }

        return theBlockBoard;
    }
}
