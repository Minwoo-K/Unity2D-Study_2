using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private int orderInLayer;

    public BlockSlot[] CreateBoard(Vector2Int blockCount, Vector2 blockHalf)
    {
        BlockSlot[] theBoard = new BlockSlot[blockCount.x * blockCount.y];

        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                float positionX = -blockCount.x/2f + blockHalf.x + x;
                float positionY =  blockCount.y/2f - blockHalf.y - y;
                Vector3 position = new Vector3(positionX, positionY, 0);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);
                
                theBoard[y * blockCount.x + x] = clone.GetComponent<BlockSlot>();
                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }

        return theBoard;
    }

}
