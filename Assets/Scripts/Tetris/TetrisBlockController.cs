using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlockController : MonoBehaviour
{
    public Action inputController = null;

    public void InputUpdate()
    {
        if (inputController != null)
            inputController.Invoke();
    }
}
