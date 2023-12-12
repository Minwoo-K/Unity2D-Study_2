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

        public void Initialized()
        {
            Numeric = Random.Range(0, 100) < 90 ? 2 : 4;

            StartCoroutine(ScaleAnimation(Vector3.one * 0.5f, Vector3.one, 0.15f));
        }

        private IEnumerator ScaleAnimation(Vector3 start, Vector3 end, float time)
        {
            float current = 0;
            float percent = 0;

            while ( percent < 1 )
            {
                current += Time.deltaTime;
                percent = current / time;

                transform.localScale = Vector3.Lerp(start, end, percent);

                yield return null;
            }
        }
    }
}
