using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace UGUI
{
    [AddComponentMenu("Tools/UGUI/Tween/Tween Position")]
    public class UTweenPosition : UTweener
    {
        public Vector3 from;
        public Vector3 to;

        public bool worldSpace = false;

        Transform mTrans;

        public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }

        /// <summary>
        /// Tween's current value.
        /// </summary>

        public Vector3 value
        {
            get
            {
                return worldSpace ? cachedTransform.position : cachedTransform.localPosition;
            }
            set
            {

                if (worldSpace) cachedTransform.position = value;
                else cachedTransform.localPosition = value;
            }
        }

        void Awake() { }

        /// <summary>
        /// Tween the value.
        /// </summary>

        protected override void OnUpdate(float factor, bool isFinished) { value = from * (1f - factor) + to * factor; }

        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public UTweenPosition Begin(GameObject go, float duration, Vector3 pos)
        {
            UTweenPosition comp = UTweener.Begin<UTweenPosition>(go, duration);
            comp.from = comp.value;
            comp.to = pos;

            if (duration <= 0f)
            {
                comp.Sample(1f, true);
                comp.enabled = false;
            }
            return comp;
        }

        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public UTweenPosition Begin(GameObject go, float duration, Vector3 pos, bool worldSpace)
        {
            UTweenPosition comp = UTweener.Begin<UTweenPosition>(go, duration);
            comp.worldSpace = worldSpace;
            comp.from = comp.value;
            comp.to = pos;

            if (duration <= 0f)
            {
                comp.Sample(1f, true);
                comp.enabled = false;
            }
            return comp;
        }

        [ContextMenu("Set 'From' to current value")]
        public override void SetStartToCurrentValue() { from = value; }

        [ContextMenu("Set 'To' to current value")]
        public override void SetEndToCurrentValue() { to = value; }

        [ContextMenu("Assume value of 'From'")]
        void SetCurrentValueToStart() { value = from; }

        [ContextMenu("Assume value of 'To'")]
        void SetCurrentValueToEnd() { value = to; }
    }
}