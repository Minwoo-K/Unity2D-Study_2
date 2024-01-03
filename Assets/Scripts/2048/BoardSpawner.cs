using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private RectTransform slotRectParent;
    [SerializeField]
    private BoardManager boardManager;

    public List<Slot> SpawnBoard(Vector2Int boardCount)
    {
        //Vector2Int boardCount = new Vector2Int(4, 4);

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
                neighbourSlots[0] = coordinate + new Vector2Int(0, -1); // or Vector2Int.down
                neighbourSlots[1] = coordinate + Vector2Int.right;
                neighbourSlots[2] = coordinate + new Vector2Int(0, 1);  // or Vector2Int.up
                neighbourSlots[3] = coordinate + Vector2Int.left;

                slot.Initialized(boardManager, neighbourSlots, coordinate);

                board.Add(slot);
            }
        }

        return board;
    }
}
