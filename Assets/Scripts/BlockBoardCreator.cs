using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBoardCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private int orderInLayer;

    private Vector2Int blockCount;
    private Vector2 blockHalf;

    private void Start()
    {
        Initialized(new Vector2Int(10, 10), new Vector2(0.5f, 0.5f));
    }

    public void Initialized(Vector2Int blockCount, Vector2 blockHalf)
    {
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;

        CreateBoard();
    }

    private void CreateBoard()
    {
        for ( int y = 0; y < blockCount.y; y++ )
        {
            for ( int x = 0; x < blockCount.x; x++ )
            {
                float positionX = -blockCount.x/2f + blockHalf.x + x;
                float positionY =  blockCount.y/2f - blockHalf.y - y;
                Vector3 position = new Vector3(positionX, positionY, 0);

                GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
            }
        }
    }

}
