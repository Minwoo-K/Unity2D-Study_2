using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction { None = -1, Up = 0, Right, Down, Left }

public class Node : MonoBehaviour
{
    public Block blockInfo;         // Block Component that this Node is currently holding
    public Vector2 localPosition;   // Local Position, == GetComponent<RectTransform>().localPosition

    public Vector2Int       Coordinate { get; private set; }        // This Node's Coordinate/Position in (x, y)
    public Vector2Int?[]    NeighbourNodes { get; private set; }    // Neighbour Nodes of THIS Node

    private Board board;

    public void Initialized(Board board, Vector2Int?[] neighbourNodes, Vector2Int coordinate)
    {
        this.board = board;
        NeighbourNodes = neighbourNodes;
        Coordinate = coordinate;
    }

    public Node FindTargetInDirection(Node originalNode, Direction direction, Node nextNode=null)
    {
        if ( NeighbourNodes[(int)direction].HasValue ) // If a Node exists(even if a null Node)
        {
            Vector2Int coordinate = NeighbourNodes[(int)direction].Value;
            Node neighbourNode = board.NodeList[coordinate.y * board.BlockCount.x + coordinate.x];

            if ( neighbourNode.blockInfo != null && originalNode.blockInfo != null )
            {
                return nextNode;
            }

            return FindTargetInDirection(originalNode, direction, neighbourNode);
        }

        return nextNode;
    }
}
