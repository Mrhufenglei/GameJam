using UnityEngine;

namespace UGUI
{
    [System.Serializable]
    public class USpriteData
    {
        /// <summary>
        /// name
        /// </summary>
        public string m_name;
        /// <summary>
        /// sprite rect
        /// </summary>
        public Rect m_rect;
        /// <summary>
        /// sprite pivot
        /// </summary>
        public Vector2 m_pivot;
        /// <summary>
        /// sprite uv 
        /// </summary>
        public Vector2[] m_uv;
        /// <summary>
        /// sprite
        /// </summary>
        public Sprite m_sprite;
        /// <summary>
        /// guid for source Texture
        /// </summary>
        public string m_sourceTextureGuid;
    }
}
