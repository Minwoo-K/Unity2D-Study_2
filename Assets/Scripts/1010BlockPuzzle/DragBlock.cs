using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BlockPuzzle
{
    public class DragBlock : MonoBehaviour
    {
        [SerializeField]
        private Vector2Int blockNumber;
        [SerializeField]
        private AnimationCurve movingCurve;
        [SerializeField]
        private AnimationCurve scaleCurve;

        private BlockPlacementValidation blockPlacementValidation;
        private Vector2 blockHalf = new Vector3(0.5f, 0.5f);
        private float movingTime = 0.2f;
        private float returningTime = 0.1f;

        public Color Color { get; private set; }
        public Vector3[] ChildBlockPositions { get; private set; }
        public Vector2Int BlockNumber { get => blockNumber; }

        public void Initialized(BlockPlacementValidation blockPlacementValidation, Vector3 position)
        {
            this.blockPlacementValidation = blockPlacementValidation;

            Color = GetComponentInChildren<SpriteRenderer>().color;

            ChildBlockPositions = new Vector3[transform.childCount];
            for (int i = 0; i < ChildBlockPositions.Length; i++)
            {
                ChildBlockPositions[i] = transform.GetChild(i).localPosition;
            }

            StartCoroutine(MoveTo(position, 0.7f));
        }

        // When clicked to be moved
        private void OnMouseDown()
        {
            StopCoroutine("ScaleTo");
            StartCoroutine(ScaleTo(Vector3.one, movingTime));
        }

        // When being moved
        private void OnMouseDrag()
        {
            Vector3 gap = new Vector3(0, 0, 10) + Vector3.up * blockNumber.y * 0.5f;
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + gap;
        }

        // When dropped
        private void OnMouseUp()
        {
            float snappedX = Mathf.RoundToInt(transform.position.x - blockNumber.x % 2 * 0.5f) + blockNumber.x % 2 * blockHalf.x;
            float snappedY = Mathf.RoundToInt(transform.position.y - blockNumber.y % 2 * 0.5f) + blockNumber.y % 2 * blockHalf.y;
            transform.position = new Vector3(snappedX, snappedY);

            bool success = blockPlacementValidation.TryPlaceBlock(this);

            // if failed to place the block
            if (success == false)
            {
                StartCoroutine(MoveTo(transform.parent.position, returningTime));
                StartCoroutine(ScaleTo(Vector3.one * 0.5f, returningTime));
            }
        }

        private IEnumerator ScaleTo(Vector3 end, float time)
        {
            Vector3 start = transform.localScale;
            float current = 0;
            float percent = 0;

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / time;

                transform.localScale = Vector3.Lerp(start, end, scaleCurve.Evaluate(percent));

                yield return null;
            }
        }

        private IEnumerator MoveTo(Vector3 end, float time)
        {
            Vector3 start = transform.position;
            float current = 0;
            float percent = 0;

            while (percent < 1)
            {
                current += Time.deltaTime;
                percent = current / time;

                transform.position = Vector3.Lerp(start, end, movingCurve.Evaluate(percent));

                yield return null;
            }
        }
    }
}
