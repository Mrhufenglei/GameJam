using System;
using UnityEngine;
using System.Collections.Generic;

namespace UGUI
{
    public partial class UAtlasData : ScriptableObject
    {
        public int m_padding = 2;
        public bool m_unityPacker = true;
        public bool m_forceSquare = true;
        public bool m_texturePacker = false;
        public Texture2D m_texture;
        public Material m_material;
        public USpriteData[] m_sprites;

        private Dictionary<string, USpriteData> m_spriteDataDic = null;

        private void OnEnable()
        {
            if (m_spriteDataDic == null)
            {
                if (m_sprites == null) return;
                m_spriteDataDic = new Dictionary<string, USpriteData>();
                for (int i = 0; i < m_sprites.Length; i++)
                {
                    m_spriteDataDic[m_sprites[i].m_name] = m_sprites[i];
                }
            }
        }

        private void OnDestroy()
        {
            m_spriteDataDic.Clear();
        }

        /// <summary>
        /// Get the corresponding data by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public USpriteData GetSpriteDataByName(string name)
        {
            USpriteData _data = null;
            if (m_spriteDataDic != null)
            {
                m_spriteDataDic.TryGetValue(name, out _data);
            }
            else
            {
                for (int i = 0; i < m_sprites.Length; i++)
                {
                    if (m_sprites[i].m_name == name)
                    {
                        _data = m_sprites[i];
                        break;
                    }
                }
            }
            return _data;
        }
        /// <summary>
        /// Get the sprite by name
        /// </summary>
        /// <param name="spriteName">sprite name</param>
        /// <returns></returns>
        public Sprite GetSpriteByName(string spriteName)
        {
            USpriteData _data = GetSpriteDataByName(spriteName);
            if (_data != null)
            {
                return _data.m_sprite;
            }
            return null;
        }
        /// <summary>
        /// Get the sprite by name add rectData
        /// </summary>
        /// <param name="spriteName">sprite name</param>
        /// <returns></returns>
        public Sprite GetSpriteByNameOrRect(string spriteName)
        {
            USpriteData _data = GetSpriteDataByName(spriteName);
            if (_data != null)
            {
                if (_data.m_sprite != null)
                {
                    return _data.m_sprite;
                }
                else
                {
                    if (m_texture != null)
                    {
                        return Sprite.Create(m_texture, _data.m_rect, _data.m_rect.center);
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// get the spriteTexture by spriteData(note,the method have pixedTexture)
        /// </summary>
        /// <param name="spriteData">data</param>
        /// <returns></returns>
        public Texture2D GetSpriteTextureByUISpriteData(USpriteData spriteData)
        {
            if (spriteData != null && spriteData.m_sprite && m_texture != null)
            {
                return GetSpriteTextureBySprite(spriteData.m_sprite);
            }
            return null;
        }
        /// <summary>
        /// get the spriteTexture by sprite(note,the method have pixedTexture)
        /// </summary>
        /// <param name="sprite">sprite</param>
        /// <returns></returns>
        public Texture2D GetSpriteTextureBySprite(Sprite sprite)
        {
            if (sprite != null && m_texture != null)
            {
                int _x = Mathf.RoundToInt(sprite.rect.x);
                int _y = Mathf.RoundToInt(sprite.rect.y);
                int _width = Mathf.RoundToInt(sprite.rect.width);
                int _hight = Mathf.RoundToInt(sprite.rect.height);

                Texture2D _texture = new Texture2D(_width, _hight);
                Color[] _colors = m_texture.GetPixels(_x, _y, _width, _hight);
                _texture.SetPixels(_colors);
                _texture.name = sprite.name;
                _texture.Apply();
                return _texture;
            }
            return null;
        }
        /// <summary>
        /// get the spriteTexture by spritename(note,the method have pixedTexture)
        /// </summary>
        /// <param name="spriteName">sprite name</param>
        /// <returns></returns>
        public Texture2D GetSpriteTextureByName(string spriteName)
        {
            USpriteData _data = GetSpriteDataByName(spriteName);
            if (_data != null && m_texture != null)
            {
                return GetSpriteTextureByUISpriteData(_data);
            }
            return null;
        }
    }
}
