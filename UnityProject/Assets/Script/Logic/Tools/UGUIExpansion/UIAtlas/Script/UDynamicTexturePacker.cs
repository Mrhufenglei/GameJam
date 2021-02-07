using UnityEngine;

namespace UGUI
{
    public class UDynamicTexturePacker
    {
        /// <summary>
        /// dynamic maker atlas
        /// </summary>
        /// <param name="texture">atlas texture</param>
        /// <param name="textures">sprite Textures</param>
        /// <param name="padding">padding</param>
        /// <param name="maxSize">max size</param>
        /// <param name="forceSquare">forceSquare</param>
        /// <returns></returns>
        public static UAtlasData DynamicTexturePacker(ref Texture2D texture, Texture2D[] textures, int padding, int maxSize, bool forceSquare)
        {
            Rect[] rects = UTexturePacker.PackTextures(texture, textures, 4, 4, padding, maxSize, forceSquare);
            if (rects != null)
            {
                UAtlasData _data = ScriptableObject.CreateInstance<UAtlasData>();
                _data.m_padding = padding;
                _data.m_forceSquare = forceSquare;
                _data.m_unityPacker = false;
                _data.m_sprites = new USpriteData[rects.Length];
                for (int i = 0; i < rects.Length; i++)
                {
                    _data.m_sprites[i] = new USpriteData();
                    _data.m_sprites[i].m_name = textures[i].name;
                    Rect _rect = new Rect(
                        Mathf.RoundToInt(rects[i].x * texture.width),
                        Mathf.RoundToInt(rects[i].y * texture.height),
                        Mathf.RoundToInt(rects[i].width * texture.width),
                        Mathf.RoundToInt(rects[i].height * texture.height));
                    _data.m_sprites[i].m_sprite = Sprite.Create(texture, _rect, _rect.center);
                    _data.m_sprites[i].m_rect = _rect;
                    _data.m_sprites[i].m_pivot = _data.m_sprites[i].m_sprite.pivot;
                    _data.m_sprites[i].m_uv = _data.m_sprites[i].m_sprite.uv;
                    _data.m_sprites[i].m_sprite.name = textures[i].name;
                }
                return _data;
            }
            return null;
        }
    }
}
