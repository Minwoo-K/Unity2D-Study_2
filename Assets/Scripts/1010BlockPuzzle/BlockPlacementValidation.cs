using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlacementValidation : MonoBehaviour
{

    private Vector2Int  blockCount;
    private Vector2     blockHalf;

    public void Initialized(Vector2Int blockCount, Vector2 blockHalf)
    {
        this.blockCount = blockCount;
        this.blockHalf = blockHalf;
    }

    
}
