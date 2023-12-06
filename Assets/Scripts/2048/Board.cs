using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Square
{
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

            foreach (Node node in NodeList)
            {
                node.localPosition = node.GetComponent<RectTransform>().localPosition;
            }

            SpawnBlockAtRandomNode();
            SpawnBlockAtRandomNode();
        }

        private void Update()
        {
            if (Input.GetKeyDown("1")) SpawnBlockAtRandomNode();
        }

        private void SpawnBlockAtRandomNode()
        {
            List<Node> emptyNodes = NodeList.FindAll(node => node.blockInfo == null);

            if (emptyNodes.Count != 0)
            {
                int index = Random.Range(0, emptyNodes.Count);
                Vector2Int coor = emptyNodes[index].Coordinate;

                SpawnBlock(coor.x, coor.y);
            }
            else
            {
                // Check whether or not Game Over
            }
        }

        private void SpawnBlock(int x, int y)
        {
            if (NodeList[y * BlockCount.x + x].blockInfo != null) return;

            GameObject clone = Instantiate(blockPrefab, blockParent);

            Block block = clone.GetComponent<Block>();

            Node node = NodeList[y * BlockCount.x + x];

            clone.GetComponent<RectTransform>().localPosition = node.localPosition;

            node.blockInfo = block;
        }
    }
}
