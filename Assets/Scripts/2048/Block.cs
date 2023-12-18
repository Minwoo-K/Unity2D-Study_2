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
    private Image blockImage;
    [SerializeField]
    private TextMeshProUGUI textBlockNumber;

    private int numeric;

    public int Numeric
    {
        set
        {
            numeric = value;
            textBlockNumber.text = numeric.ToString();
            blockImage.color = blockColorList[(int)Mathf.Log(value, 2) - 1];
        }
        get => numeric;
    }

    public void Initialized()
    {
        Numeric = Random.Range(0, 100) < 90 ? 2 : 4;
    }
}
