using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    private float dragDistance = 25f;   // The minimum distance of a touch drag

    private Vector3 touchStart;         // Initial touch position
    private Vector3 touchEnd;           // End of the touch position
    private bool isTouch = false;

    public Direction UpdateTouch()
    {
        Direction direction = Direction.None;

        if ( Input.GetMouseButtonDown(0))   // initial touch detect
        {
            isTouch = true;
            touchStart = Input.mousePosition;
        }
        else if ( Input.GetMouseButton(0) ) // If touch continues,
        {
            // In case the Initial Touch detection was skipped
            if ( isTouch == false ) return Direction.None;
            // Save the Input position while the touch continues
            touchEnd = Input.mousePosition;
            // Calculate the distance between the start and end positions
            float diffX = touchEnd.x - touchStart.x;
            float diffY = touchEnd.y - touchStart.y;

            // Decide whether horizontal or vertical touch based on distance
            if ( Mathf.Abs(diffX) > Mathf.Abs(diffY) )
            { // Horizontal
                // Mathf.Sign(f): if 'f' is 0 or a positive number, the result is 1. Otherwise(negative number), -1
                if (Mathf.Sign(diffX) >= 0) direction = Direction.Right;
                else                        direction = Direction.Left;
            }
            else
            { // Vertical
                if (Mathf.Sign(diffY) >= 0) direction = Direction.Up;
                else                        direction = Direction.Down;
            }
        }

        if (direction != Direction.None) isTouch = false;

        return direction;
    }
}
