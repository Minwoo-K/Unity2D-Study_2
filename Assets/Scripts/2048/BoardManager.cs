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

        SpawnBlockAtRandomSlot();
        SpawnBlockAtRandomSlot();
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
            SpawnBlockAtRandomSlot();
    }

    private void SpawnBlockAtRandomSlot()
    {
        // Fetch all Slots with no Block attached
        List<Slot> emptySlots = theBoard.FindAll( slot => slot.placedBlock == null );
        
        if (emptySlots.Count != 0)
        {
            // Randomize a number within the range of empty Slots
            int random = Random.Range(0, emptySlots.Count);
            // Spawn a Block on the Slot
            SpawnBlock(emptySlots[random].Coordinate);
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
