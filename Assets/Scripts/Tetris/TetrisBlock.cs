using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private TetrisBlockController controller;
    private float downFrame = 2f;
    private float timer = 0;

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

    public void OnBoard()
    {
        controller = new TetrisBlockController();
        controller.inputController += UponMoving;
    }

    public void OffBoard()
    {
        controller.inputController -= UponMoving;
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
