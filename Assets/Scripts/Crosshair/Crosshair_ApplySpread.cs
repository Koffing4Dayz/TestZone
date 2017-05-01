using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crosshair
{
    public class Crosshair_ApplySpread : MonoBehaviour
    {
        public enum Direction
        {
            None,
            Up,
            Down,
            Left,
            Right
        }

        public Crosshair_Master MasterCrosshair;
        public Direction SpreadDirection;
        public float SpreadFactor = 1;
        private RectTransform myTransform;
        private float defualtLocation;
        private float newSpread;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            MasterCrosshair.EventSpreadCaptured += ApplySpread;
        }

        private void OnDisable()
        {
            MasterCrosshair.EventSpreadCaptured -= ApplySpread;
        }

        private void Initialize()
        {
            if (MasterCrosshair == null)
            {
                MasterCrosshair = GetComponent<Crosshair_Master>();
            }
            myTransform = GetComponent<RectTransform>();

            switch (SpreadDirection)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    defualtLocation = myTransform.localPosition.y;
                    break;
                case Direction.Down:
                    defualtLocation = myTransform.localPosition.y;
                    break;
                case Direction.Left:
                    defualtLocation = myTransform.localPosition.x;
                    break;
                case Direction.Right:
                    defualtLocation = myTransform.localPosition.x;
                    break;
                default:
                    break;
            }
        }

        private void ApplySpread(float spread)
        {
            newSpread = spread * SpreadFactor;
            switch (SpreadDirection)
            {
                case Direction.None:
                    break;
                case Direction.Up:
                    newSpread += defualtLocation;
                    if (newSpread >= defualtLocation)
                    {
                        myTransform.localPosition = new Vector3(myTransform.localPosition.x, newSpread, myTransform.localPosition.z);
                    }
                    break;
                case Direction.Down:
                    newSpread *= -1;
                    newSpread += defualtLocation;
                    if (newSpread <= defualtLocation)
                    {
                        myTransform.localPosition = new Vector3(myTransform.localPosition.x, newSpread, myTransform.localPosition.z);
                    }
                    break;
                case Direction.Left:
                    newSpread *= -1;
                    newSpread += defualtLocation;
                    if (newSpread <= defualtLocation)
                    {
                        myTransform.localPosition = new Vector3(newSpread, myTransform.localPosition.y, myTransform.localPosition.z);
                    }
                    break;
                case Direction.Right:
                    newSpread += defualtLocation;
                    if (newSpread >= defualtLocation)
                    {
                        myTransform.localPosition = new Vector3(newSpread, myTransform.localPosition.y, myTransform.localPosition.z);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
