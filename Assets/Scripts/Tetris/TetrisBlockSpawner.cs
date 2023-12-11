using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tetrisBlockPrefabs;
    [SerializeField]
    private Color[] colourList;

    public TetrisBlock SpawnTetrisBlock(Transform spawningPoint)
    {
        // Generate a random number to randomly spawn a TetrisBlock
        int random = Random.Range(0, tetrisBlockPrefabs.Length);
        // Spawn a TetrisBlock with the random number
        GameObject clone = Instantiate(tetrisBlockPrefabs[random], spawningPoint);
        // Pull the TetrisBlock component to initialize
        TetrisBlock tetrisBlock = clone.GetComponent<TetrisBlock>();
        // Get another random number for a random colour from the list
        random = Random.Range(0, colourList.Length);
        // Initialize TetrisBlock component
        tetrisBlock.Initialized(colourList[random]);
        // Return the TetrisBlock component
        return tetrisBlock;
    }
}
