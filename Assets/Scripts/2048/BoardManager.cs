using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { StandBy = 0, Processing, Complete }

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private BoardSpawner boardSpawner;
    [SerializeField]
    private UI_Controller ui_Controller;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private RectTransform blockRectParent;
    
    public List<Slot> theBoard { get; private set; }
    public Vector2Int BoardCount { get; private set; }

    private TouchController touchController;
    private List<Block> existingBlocks;
    private State state = State.StandBy;
    private int currentScore;
    private int highestScore;
    private float blockSize;

    private void Awake()
    {
        //BoardCount = new Vector2Int(4, 4);
        int boardSize = PlayerPrefs.GetInt("BoardCount");
        BoardCount = new Vector2Int(boardSize, boardSize);

        // Block's Size = (Size of the Board - Padding - (Spacing x BoardCount.x)) / Board's Count; 
        blockSize = (1080 - 85 - (25 * BoardCount.x)) / BoardCount.y;

        theBoard = boardSpawner.SpawnBoard(BoardCount, blockSize);

        touchController = GetComponent<TouchController>();

        existingBlocks = new List<Block>();

        currentScore = 0;
        ui_Controller.UpdateScore(currentScore);

        highestScore = PlayerPrefs.GetInt("HighestScore");
        ui_Controller.UpdateHighestScore(highestScore);
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
        else
        {
            if ( IsGameOver() )
            {
                GameOver();
            }
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
        // Set up the Block's size according to the BoardCount
        block.GetComponent<RectTransform>().sizeDelta = new Vector2(blockSize, blockSize);
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
                        state = State.Processing;
                        block.StartMovingToTarget();
                    }
                }
            }

            if ( IsGameOver() )
            {
                GameOver();
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
            if ( slot.placedBlock != null && neighbourSlot.placedBlock != null )
            {
                if ( slot.placedBlock.Numeric == neighbourSlot.placedBlock.Numeric )
                {
                    Combine(slot, neighbourSlot);
                }
            }
            else if (neighbourSlot != null && neighbourSlot.placedBlock == null)
            {
                Move(slot, neighbourSlot);
            }
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

    private void Combine(Slot from, Slot to)
    {
        from.placedBlock.CombineToNode(to);

        from.placedBlock = null;

        to.combined = true;
    }

    // To track the State to execute different situation
    private void WatchState()
    {
        bool allTargetsNull = true;

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
            List<Block> BlocksToDestroy = new List<Block>();
            foreach ( Block block in existingBlocks)
            {
                if ( block.NeedToDestroy ) // || block.Numeric == 2048)
                {
                    BlocksToDestroy.Add(block);
                }
            }

            BlocksToDestroy.ForEach(x =>
            {
                currentScore += x.Numeric;
                existingBlocks.Remove(x);
                Destroy(x.gameObject);
            });


            state = State.Complete;
        }

        if ( state == State.Complete )
        {
            state = State.StandBy;

            SpawnBlockAtRandomSlot();

            theBoard.ForEach(x => x.combined = false);

            ui_Controller.UpdateScore(currentScore);
        }
    }

    private bool IsGameOver()
    {
        foreach ( Slot slot in theBoard )
        {
            // if no Block placed yet, Game is still on
            if ( slot.placedBlock == null ) return false;

            for ( int i = 0; i < slot.NeighbourSlots.Length; i++ )
            {
                // If outside of the Board, skip it
                if ( slot.NeighbourSlots[i] == null ) continue;
                // Fetch the neighbour Slot's info
                Vector2Int coor = slot.NeighbourSlots[i].Value;
                Slot neighbourSlot = theBoard[coor.y * BoardCount.x + coor.x];
                // If the both Slots have a block,
                if ( slot.placedBlock != null && neighbourSlot.placedBlock != null )
                {
                    // AND the Numbers are the same,
                    if ( slot.placedBlock.Numeric == neighbourSlot.placedBlock.Numeric )
                    {
                        // Game is still on
                        return false;
                    }
                }
            }
        }
        // If none of the above validation passes, It's GameOver
        return true;
    }

    private void GameOver()
    {
        Debug.Log("GameOver");

        if ( currentScore > highestScore )
        {
            PlayerPrefs.SetInt("HighestScore", currentScore);
        }

        ui_Controller.OnGameOver();
    }
}
