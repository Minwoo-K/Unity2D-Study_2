using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class TetrisBlock : MonoBehaviour
    {
        private TetrisBlockController controller;   // The Controller when the TetrisBlock is active in the field
        private TetrisBlockValidation tetrisBlockValidation; // The Validation component
        private float downFrame = 2f;               // Time/Speed of the TetrisBlock going down
        private float timer = 0;                    // A timer to measure time. Every [downFrame] time, the TetrisBlock goes 1 row lower

        // TetrisBlock's Color
        public Color Color { get; private set; }

        private void Update()
        {
            if (controller != null)
                controller.InputUpdate();
        }

        // Initialize the TetrisBlock
        public void Initialized(TetrisBlockValidation tetrisBlockValidation, Color color)
        {
            // Set up TetrisBlockValidation component
            this.tetrisBlockValidation = tetrisBlockValidation;
            // Configure the given color with all the blocks under the TetrisBlock object
            for (int i = 0; i < transform.childCount; i++)
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

        // Input Controlling Method
        private void UponMoving()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if ( tetrisBlockValidation.IsBoardInDirection(this, Vector3.right))
                    transform.position += Vector3.right;
                
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (tetrisBlockValidation.IsBoardInDirection(this, Vector3.left))
                    transform.position += Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (tetrisBlockValidation.IsBoardInDirection(this, Vector3.down))
                    transform.position += Vector3.down;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.Rotate(Vector3.forward, -90);
            }

            // Each "downFrame" time,
            timer += Time.deltaTime;
            if (timer >= downFrame)
            {
                // If no block under NOR end of the board, OR above the block(on one of the SpawningPoints)
                if ( tetrisBlockValidation.boardCount.y < transform.position.y || tetrisBlockValidation.IsEmptyUnder(this) )
                {
                    // The TetrisBlock goes down
                    transform.position += Vector3.down;
                    timer = 0;
                }
                else // If block or end of the board underneath,
                {
                    // Lock the position by deleting the input controller
                    OffBoard();
                    tetrisBlockValidation.FillTheBoardWith(this);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}