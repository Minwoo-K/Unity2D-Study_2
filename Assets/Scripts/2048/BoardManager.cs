using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    private BoardSpawner boardSpawner;
    [SerializeField]
    private GameObject blockPrefab;
    

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

    private void SpawnBlock()
    {

    }
}
