using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject slotPrefab;
    [SerializeField]
    private RectTransform slotRectParent;

    private void Awake()
    {
        Vector2Int boardCount = new Vector2Int(4, 4);

        for ( int y = 0; y < boardCount.y; y ++ )
        {
            for ( int x = 0; x < boardCount.x; x ++ )
            {
                GameObject clone = Instantiate(slotPrefab, slotRectParent);
            }
        }
    }
}
