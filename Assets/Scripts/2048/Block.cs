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
            Numeric = value;

            blockNumeric_text.text = Numeric.ToString();
        }

        get => numeric;
    }
}
