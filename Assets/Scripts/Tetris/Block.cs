using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private bool filled = false;

        public bool IsFilled { get => filled; }

        public void FillIt(Color color)
        {
            filled = true;
            spriteRenderer.color = color;
        }

        public void EmptyIt()
        {
            filled = false;
        }
    }
}
