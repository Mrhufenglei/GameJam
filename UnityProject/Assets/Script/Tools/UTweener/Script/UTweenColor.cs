using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace UGUI
{
    [AddComponentMenu("Tools/UGUI/Tween/Tween Color")]
    public class UTweenColor : UTweener
    {

        public Color from = Color.white;
        public Color to = Color.white;

        bool mCached = false;
        Material mMat;
        Light mLight;
        SpriteRenderer mSr;
        CanvasRenderer mCanvas;
        void Cache()
        {
            mSr = GetComponent<SpriteRenderer>();

            Renderer ren = GetComponent<Renderer>();
            if (ren != null)
            {
                mMat = ren.material;
            }

            mCanvas = GetComponent<CanvasRenderer>();

            mLight = GetComponent<Light>();

            mCached = true;
        }

        [System.Obsolete("Use 'value' instead")]
        public Color color { get { return this.value; } set { this.value = value; } }

        /// <summary>
        /// Tween's current value.
        /// </summary>

        public Color value
        {
            get
            {
                if (!mCached) Cache();
                if (mMat != null) return mMat.color;
                if (mSr != null) return mSr.color;
                if (mLight != null) return mLight.color;
                if (mCanvas != null) return mCanvas.GetColor();
                return Color.black;
            }
            set
            {
                if (!mCached) Cache();
                if (mMat != null) mMat.color = value;
                else if (mSr != null) mSr.color = value;
                else if (mCanvas != null) mCanvas.SetColor(value);
                else if (mLight != null)
                {
                    mLight.color = value;
                    mLight.enabled = (value.r + value.g + value.b) > 0.01f;
                }
            }
        }

        /// <summary>
        /// Tween the value.
        /// </summary>

        protected override void OnUpdate(float factor, bool isFinished) { value = Color.Lerp(from, to, factor); }

        /// <summary>
        /// Start the tweening operation.
        /// </summary>

        static public UTweenColor Begin(GameObject go, float duration, Color color)
        {
#if UNITY_EDITOR
            if (!Application.isPlaying) return null;
#endif
            UTweenColor comp = UTweener.Begin<UTweenColor>(go, duration);
            comp.from = comp.value;
            comp.to = color;

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