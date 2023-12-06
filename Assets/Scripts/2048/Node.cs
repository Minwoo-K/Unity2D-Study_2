using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Square
{
    public class Node : MonoBehaviour
    {
        public Block blockInfo;
        public Vector2 localPosition;

        public Vector2Int Coordinate { get; private set; }

        public void Initialized(Vector2Int coordinate)
        {
            Coordinate = coordinate;
        }
    }
}