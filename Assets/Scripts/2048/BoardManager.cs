using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { StandBy = 0, Processing, Complete }

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

    private TouchController touchController;
    private List<Block> existingBlocks;
    private State state = State.StandBy;

    private void Awake()
    {
        BoardCount = new Vector2Int(4, 4);

        theBoard = boardSpawner.SpawnBoard(BoardCount);

        touchController = GetComponent<TouchController>();

        existingBlocks = new List<Block>();
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
        // if (Input.GetKeyDown("1")) SpawnBlockAtRandomSlot();

        if ( state == State.StandBy )
        {
            Direction direction = touchController.UpdateTouch();

            if ( direction != Direction.None )
            {
                AllBlocksProcess(direction);
            }
        }
        else
        {
            WatchState();
        }
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
        // Initialize the Block Component
        block.Initialized();
        // Update the position
        block.GetComponent<RectTransform>().localPosition = slot.localPosition;
        // Attach the Block component to the corresponding Slot object
        slot.placedBlock = block;
        // Add the spawned block to the List
        existingBlocks.Add(block);
    }

    private void AllBlocksProcess(Direction direction)
    {
        if ( state == State.StandBy )
        {
            if ( direction != Direction.None )
            {
                state = State.Processing;
                
                if ( direction == Direction.Up )
                {
                    for (int y = 1; y < BoardCount.y; y++)
                    {
                        for (int x = 0; x < BoardCount.x; x++)
                        {
                            BlockProcess(theBoard[y * BoardCount.x + x], direction);
                        }
                    }
                }
                else if ( direction == Direction.Right )
                {
                    for ( int y = 0; y < BoardCount.y; y++ )
                    {
                        for ( int x = (BoardCount.x-2); x >= 0; x-- )
                        {
                            BlockProcess(theBoard[y * BoardCount.x + x], direction);
                        }
                    }
                }
                else if ( direction == Direction.Down )
                {
                    for ( int y = (BoardCount.y-2); y >= 0; y-- )
                    {
                        for ( int x = 0; x < BoardCount.x; x++ )
                        {
                            BlockProcess(theBoard[y * BoardCount.x + x], direction);
                        }
                    }
                }
                else // direction == Direction.Left
                {
                    for ( int y = 0; y < BoardCount.y; y++ )
                    {
                        for ( int x = 1; x < BoardCount.x; x++ )
                        {
                            BlockProcess(theBoard[y * BoardCount.x + x], direction);
                        }
                    }
                }

                foreach ( Block block in existingBlocks )
                {
                    if ( block.Target != null )
                    {
                        block.StartMovingToTarget();
                    }
                }
            }
        }
    }

    // To figure out whether the Slot can integrate with another Slot afar in the direction
    //                    OR the Slot just moves to the end of the direction
    private void BlockProcess(Slot slot, Direction direction)
    {
        // If the Slot carries no block, do nothing
        if (slot.placedBlock == null) return;

        Slot neighbourSlot = slot.FindSlotToLand(slot, direction);
        if (neighbourSlot != null)
        {
            if (neighbourSlot != null && neighbourSlot.placedBlock == null)
            {
                Move(slot, neighbourSlot);
            }
        }
        else // Intergration of 2 Slots with blocks
        {

        }
    }

    // To move a non-empty Slot to another an empty slot 
    private void Move(Slot from, Slot to)
    {
        // Move it to the given Slot
        from.placedBlock.MoveTo(to);

        // If it carries a block,
        if ( from.placedBlock != null )
        {
            // Move the block to the given Slot as well
            to.placedBlock = from.placedBlock;
            // Empty the from Slot
            from.placedBlock = null;
        }
    }

    // To track the State to execute different situation
    private void WatchState()
    {
        bool allTargetsNull = false;

        foreach ( Block block in existingBlocks )
        {
            if ( block.Target != null )
            {
                allTargetsNull = false;
                break;
            }
        }

        if ( allTargetsNull && state == State.Processing )
        {
            state = State.Complete;
        }

        if ( state == State.Complete )
        {
            state = State.StandBy;

            SpawnBlockAtRandomSlot();
        }
    }
}
