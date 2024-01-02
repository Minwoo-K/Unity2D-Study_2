using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private BoardSpawner boardSpawner;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private RectTransform blockRectParent;

    public List<Slot> theBoard { get; private set; }
    public Vector2Int BoardCount { get; private set; }

    private void Awake()
    {
        BoardCount = new Vector2Int(4, 4);

        theBoard = boardSpawner.SpawnBoard(BoardCount);
    }

    private void Start()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(boardSpawner.GetComponent<RectTransform>());

        foreach ( Slot slot in theBoard )
        {
            slot.localPosition = slot.GetComponent<RectTransform>().localPosition;
        }
    }

    private void SpawnBlock(Vector2Int coordinate)
    {
        // Figure out the index from the coordinate value
        int index = coordinate.y * BoardCount.x + coordinate.x;
        // If the Slot is holding a block, do nothing.
        if (theBoard[index].placedBlock != null) return;
        // Spawn a Block
        GameObject  clone = Instantiate(blockPrefab, blockRectParent);
        Block       block = clone.GetComponent<Block>();
        Slot        slot  = theBoard[index];
        // Update the position
        block.GetComponent<RectTransform>().localPosition = slot.localPosition;
        // Attach the Block component to the corresponding Slot object
        slot.placedBlock = block;
    }
}
