//-----------------------------------------------------------------
//
//              Hongyu @  2021-02-07 14:48:44
//
//----------------------------------------------------------------

using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BombController : MonoBehaviour, IGameController
{
    public GameObject BombPrefab;

    public float DropInterval = 5;
    public float DropCount = 5;

    public float DropY = 10;

    public float RandomRangeX = 5;
    public float RandomRangeZ = 5;
    private float mTimer = 0f;

    private List<BombBase> m_bombs = new List<BombBase>();
    private Dictionary<int, BombBase> m_dicBombs = new Dictionary<int, BombBase>();

    #region IGameController

    public void OnInit()
    {
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
        mTimer -= Time.deltaTime;
        if (mTimer <= 0)
        {
            for (int i = 0; i < DropCount; i++)
            {
                CreateBomb();
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
        var go = GameObject.Instantiate(BombPrefab);
        var rndx = Random.Range(-RandomRangeX, RandomRangeX);
        var rndz = Random.Range(-RandomRangeZ, RandomRangeZ);
        var bombBase = go.GetComponent<BombBase>();
        if (bombBase == null) return;

        go.transform.position = new Vector3(rndx, DropY, rndz);
        m_bombs.Add(bombBase);
        m_dicBombs[go.GetInstanceID()] = bombBase;
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
}