using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private RectTransform slotRectParent;
    [SerializeField]
    private BoardManager boardManager;
    [SerializeField]
    private GridLayoutGroup gridLayoutGroup;

    public List<Slot> SpawnBoard(Vector2Int boardCount, float blockSize)
    {
        gridLayoutGroup.cellSize = new Vector2(blockSize, blockSize);

        List<Slot> board = new List<Slot>();

        for ( int y = 0; y < boardCount.y; y ++ )
        {
            for ( int x = 0; x < boardCount.x; x ++ )
            {
                GameObject clone = Instantiate(slotPrefab, slotRectParent);

                clone.name = $"[Slot {y}, {x}]";

                Slot slot = clone.GetComponent<Slot>();

                Vector2Int coordinate = new Vector2Int(x, y);

                Vector2Int?[] neighbourSlots = new Vector2Int?[4];
                Vector2Int up = coordinate + new Vector2Int(0, -1); // or Vector2Int.down
                Vector2Int right = coordinate + Vector2Int.right;
                Vector2Int down = coordinate + new Vector2Int(0, 1);  // or Vector2Int.up
                Vector2Int left = coordinate + Vector2Int.left;

                if (IsValid(boardCount, up)) neighbourSlots[0] = up;
                if (IsValid(boardCount, right)) neighbourSlots[1] = right;
                if (IsValid(boardCount, down)) neighbourSlots[2] = down;
                if (IsValid(boardCount, left)) neighbourSlots[3] = left;

                slot.Initialized(boardManager, neighbourSlots, coordinate);

                board.Add(slot);
            }
        }

        return board;
    }

    private bool IsValid(Vector2Int boardCount, Vector2Int slotCoordinate)
    {
        if ( slotCoordinate.x >= boardCount.x || slotCoordinate.y >= boardCount.y ||
             slotCoordinate.x < 0 || slotCoordinate.y < 0 )
        {
            return false;
        }

        return true;
    }
}
