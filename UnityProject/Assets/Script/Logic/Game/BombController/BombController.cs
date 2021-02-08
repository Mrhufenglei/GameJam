//-----------------------------------------------------------------
//
//              Hongyu @  2021-02-07 14:48:44
//
//----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 
/// </summary>
public class BombController : MonoBehaviour, IGameController
{
    [Serializable]
    public class WavaData
    {
        public List<WavaBombData> m_wavaBombDatas = new List<WavaBombData>();
    }

    [Serializable]
    public class WavaBombData
    {
        public int m_index;
        public int m_count;
    }

    [Header("Wava Setting")] public GameObject[] m_prefabs;
    public List<WavaData> m_wavaData = new List<WavaData>(1);
    public int m_wavaIndex = 0;

    [Header("Bound Setting")] public float DropInterval = 5;
    public float DropY = 10;
    public float RandomRangeX = 5;
    public float RandomRangeZ = 5;
    [Header("Create Setting")] private float mTimer = 0f;

    private List<BombBase> m_bombs = new List<BombBase>();
    private Dictionary<int, BombBase> m_dicBombs = new Dictionary<int, BombBase>();

    #region IGameController

    public void OnInit()
    {
        m_wavaIndex = 0;
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        mTimer -= Time.deltaTime;
        if (mTimer <= 0)
        {
            CreateBomb();

            m_wavaIndex++;

            if (m_wavaIndex >= m_wavaData.Count)
            {
                m_wavaIndex = m_wavaData.Count - 1;
            }

            mTimer = DropInterval;
        }
    }

    public void OnDeInit()
    {
        DestroyAllBomb();
    }

    public void OnReset()
    {
    }

    public void OnGameStart()
    {
    }

    public void OnPause(bool pause)
    {
    }

    public void OnGameOver(GameOverType gameOverType)
    {
    }

    #endregion

    private void CreateBomb()
    {
        WavaData data = GetWavaData();
        for (int i = 0; i < data.m_wavaBombDatas.Count; i++)
        {
            var wava = data.m_wavaBombDatas[i];
            var prefab = m_prefabs[wava.m_index];
            var count = wava.m_count;
            if (prefab == null) continue;
            if (count == 0) continue;

            for (int j = 0; j < count; j++)
            {
                var obj = GameObject.Instantiate(prefab);
                var rndx = Random.Range(-RandomRangeX, RandomRangeX);
                var rndz = Random.Range(-RandomRangeZ, RandomRangeZ);
                var bombBase = obj.GetComponent<BombBase>();
                if (bombBase == null) return;

                obj.transform.position = transform.TransformPoint(new Vector3(rndx, DropY, rndz));
                m_bombs.Add(bombBase);
                m_dicBombs[obj.GetInstanceID()] = bombBase;
            }
        }
    }

    public void DestroyBomb(BombBase bombBase)
    {
        if (bombBase == null) return;
        m_bombs.Remove(bombBase);
        m_dicBombs.Remove(bombBase.gameObject.GetInstanceID());
        GameObject.Destroy(bombBase.gameObject);
    }

    public void DestroyAllBomb()
    {
        for (int i = 0; i < m_bombs.Count; i++)
        {
            var bomb = m_bombs[i];
            if (bomb == null) continue;
            GameObject.Destroy(bomb.gameObject);
        }

        m_bombs.Clear();
    }

    /// <summary>
    /// 获得Bomb
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public BombBase GetBomb(int key)
    {
        return m_dicBombs[key];
    }

    public WavaData GetWavaData()
    {
        if (m_wavaIndex >= m_wavaData.Count)
        {
            m_wavaIndex = m_wavaData.Count - 1;
        }

        return m_wavaData[m_wavaIndex];
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(RandomRangeX * 2, DropY, RandomRangeZ * 2));
    }
}