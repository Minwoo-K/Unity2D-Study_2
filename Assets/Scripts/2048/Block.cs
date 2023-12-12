using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Square
{
    public class Block : MonoBehaviour
    {
        [SerializeField]
        private Color[] blockColorList;
        [SerializeField]
        private Image blockImage;
        [SerializeField]
        private TextMeshProUGUI text_BlockNumeric;

        private int numeric;

        public int Numeric
        {
            set
            {
                numeric = value;
                text_BlockNumeric.text = value.ToString();
                blockImage.color = blockColorList[(int)Mathf.Log(value, 2) - 1];
            }
            get => numeric;
        }

        public void SetupFirstNumeric()
        {
            Numeric = Random.Range(0, 100) < 90 ? 2 : 4;
        }
    }
}
