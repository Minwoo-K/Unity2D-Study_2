using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField]
    private NodeSpawner nodeSpawner;
    [SerializeField]
    private GameObject blockPrefab;
    [SerializeField]
    private RectTransform blockParent;

    public List<Node> NodeList { get; private set; }
    public Vector2Int BlockCount { get; private set; }

    private void Awake()
    {
        BlockCount = new Vector2Int(4, 4);

        NodeList = nodeSpawner.SpawnNodes(BlockCount);
    }

    private void Start()
    {
        UnityEngine.UI.LayoutRebuilder.ForceRebuildLayoutImmediate(nodeSpawner.GetComponent<RectTransform>());


    }
}
