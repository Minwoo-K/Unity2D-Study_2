using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    [SerializeField]
    private float dragDistance = 25;        //

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

            if ( Input.GetMouseButtonUp(0) )
            {
                touchEnd = Input.mousePosition;

                float deltaX = touchStart.x - touchEnd.x;
                float deltaY = touchStart.y - touchEnd.y;

                if ( Mathf.Abs(deltaX) < dragDistance && Mathf.Abs(deltaY) < dragDistance )
                {
                    return Direction.None;
                }

                if ( Mathf.Abs(deltaX) > Mathf.Abs(deltaY) )
                {
                    // float f = Mathf.Sign(x);
                    // if f == 0 or 1, x is a positive number
                    // if f == -1, x is a negative number
                    if (Mathf.Sign(deltaX) >= 0) direction = Direction.Right;
                    else direction = Direction.Left;
                }
                else
                {
                    if (Mathf.Sign(deltaY) >= 0) direction = Direction.Down;
                    else direction = Direction.Up;
                }
            }
        }

        if (direction != Direction.None) isTouched = false;

        return direction;
    }
}
