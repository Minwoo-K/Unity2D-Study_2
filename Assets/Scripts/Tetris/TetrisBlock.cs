using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    // The Controller when the TetrisBlock is active in the field
    private TetrisBlockController controller;
    // Time/Speed of the TetrisBlock going down
    private float downFrame = 2f;
    // A timer to measure time. Every [downFrame] time, the TetrisBlock goes 1 row lower
    private float timer = 0;

    // TetrisBlock's Color
    public Color Color { get; private set; }

    private void Update()
    {
        if ( controller != null )
            controller.InputUpdate();
    }

    public void Initialized(Color color)
    {
        for ( int i = 0; i < transform.childCount; i++ )
        {
            transform.GetChild(i).GetComponent<SpriteRenderer>().color = color;
        }
        Color = color;

    }

    // When TetrisBlock is spawned on the field
    public void OnBoard()
    {
        // Initialize the Controller
        controller = new TetrisBlockController();
        // Register the controlling method
        controller.inputController += UponMoving;
    }

    // When TetrisBlock has landed on the field
    public void OffBoard()
    {
        // Deregister the controlling method
        controller.inputController -= UponMoving;
        // Delete the Controller object
        controller = null;
    }

    private void UponMoving()
    {
        if ( Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.Rotate(Vector3.forward, -90);
        }

        timer += Time.deltaTime;
        if ( timer >= downFrame )
        {
            transform.position += Vector3.down;
            timer = 0;
        }
    }
}
