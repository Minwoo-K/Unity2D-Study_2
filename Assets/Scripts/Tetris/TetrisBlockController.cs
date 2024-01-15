using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tetris
{
    public class TetrisBlockController
    {
        // Action object to register input event
        public Action inputController = null;

        // To trigger the event
        public void InputUpdate()
        {
            if (inputController != null)
                inputController.Invoke();
        }
    }
}
