using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class BlockSlot : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private float scalingTime = 0.1f;

        public bool IsFilled { get; private set; } = false;

        public void Initialized()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void GetFilled(Color color)
        {
            IsFilled = true;

            spriteRenderer.color = color;
        }

        public void GetEmpty()
        {
            IsFilled = false;

            StartCoroutine(ScaleTo(Vector3.zero));
        }

        private IEnumerator ScaleTo(Vector3 end)
        {
            Vector3 start = transform.localScale;
            float current = 0;
            float percent = 0;

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / scalingTime;

                transform.localScale = Vector3.Lerp(start, end, percent);

                yield return null;
            }

            spriteRenderer.color = Color.white;
            transform.localScale = Vector3.one;
        }
    }
}