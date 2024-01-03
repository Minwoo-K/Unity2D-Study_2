using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Block : MonoBehaviour
{
    [SerializeField]
    private Color[] blockColorList;
    [SerializeField]
    private Image block_image;
    [SerializeField]
    private TextMeshProUGUI blockNumeric_text;

    private int numeric;

    public int Numeric
    {
        set
        {
            numeric = value;

            blockNumeric_text.text = numeric.ToString();

            block_image.color = blockColorList[(int)Mathf.Log(value, 2) - 1];
        }

        get => numeric;
    }

    public void Initialized()
    {
        int random = Random.Range(1, 100);

        Numeric = random > 90 ? 4 : 2;
    }
}
