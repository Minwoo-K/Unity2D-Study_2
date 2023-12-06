using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class BoardCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject blockPrefab;
        [SerializeField]
        private int orderInLayer;

        private Vector2Int boardCount;

        private void Awake()
        {
            boardCount = new Vector2Int(10, 20);

            CreateBoard();
        }

        public void CreateBoard()
        {
            for ( int y = 0; y < boardCount.y; y++ )
            {
                for ( int x = 0; x < boardCount.x; x++ )
                {
                    float positionX = -boardCount.x / 2f + 0.5f + x;
                    Vector2 position = new Vector2(positionX, y);
                    GameObject clone = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                    clone.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
                }
            }
        }
    }
}