using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    private float dragDistance = 25;        // Minimum distance to accept an input/touch

    private Vector3 touchStart, touchEnd;   //
    private bool isTouched = false;         //

    public Direction UpdateTouch()
    {
        Direction direction = Direction.None;

        if ( Input.GetMouseButtonDown(0) )
        {
            touchStart = Input.mousePosition;
            isTouched = true;
        }
        else if ( Input.GetMouseButton(0) )
        {
            if (isTouched == false) return Direction.None;

            touchEnd = Input.mousePosition;

            float deltaX = touchEnd.x - touchStart.x;
            float deltaY = touchEnd.y - touchStart.y;

            if (Mathf.Abs(deltaX) < dragDistance && Mathf.Abs(deltaY) < dragDistance)
            {
                return Direction.None;
            }

            if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                // float f = Mathf.Sign(x);
                // if f == 0 or 1, x is a positive number
                // if f == -1, x is a negative number
                if (Mathf.Sign(deltaX) >= 0) direction = Direction.Right;
                else direction = Direction.Left;
            }
            else
            {
                if (Mathf.Sign(deltaY) >= 0) direction = Direction.Up;
                else direction = Direction.Down;
            }
            
        }

        if (direction != Direction.None) isTouched = false;

        return direction;
    }
}
