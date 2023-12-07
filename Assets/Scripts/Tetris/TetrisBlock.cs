using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    private void Update()
    {
        if ( Input.GetKeyDown(KeyCode.R))
        {
            transform.Rotate(Vector3.forward, 90);
            for (int i = 0; i < transform.childCount; i++ )
            {
                Debug.Log($"Local Position {i}:{transform.GetChild(i).localPosition}\n" +
                    $"World Position {i}: {transform.GetChild(i).position}");
            }
        }
    }
}
