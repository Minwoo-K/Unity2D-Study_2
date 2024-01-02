using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private RectTransform slotRectParent;

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

                slot.Initialized(new Vector2Int(x, y));

                board.Add(slot);
            }
        }

        return board;
    }
}
