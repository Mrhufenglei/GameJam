//-----------------------------------------------------------------
//
//              Hongyu @  2021-02-07 14:48:44
//
//----------------------------------------------------------------

using UnityEngine;

/// <summary>
/// 
/// </summary>
public class BombController : MonoBehaviour, IGameController
{
    
    #region IGameController

    public void OnInit()
    {
    }

    public void OnUpdate(float deltaTime, float unscaledDeltaTime)
    {
    }

    public void OnDeInit()
    {
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

    public GameObject BombPrefab;

    public float DropInterval = 5;
    public float DropCount = 5;

    public float DropY = 10;

    public float RandomRangeX = 5;
    public float RandomRangeZ = 5;
    private float mTimer = 0f;

    void Update()
    {
        mTimer -= Time.deltaTime;
        if(mTimer <= 0)
        {
            for (int i = 0; i < DropCount; i++)
            {
                CreateBomb();
            }
            
            mTimer = DropInterval;
        }
    }

    private void CreateBomb()
    {
        var go = GameObject.Instantiate(BombPrefab);
        var rndx = Random.Range(-RandomRangeX,RandomRangeX);
        var rndz = Random.Range(-RandomRangeZ,RandomRangeZ);
        Debug.Log("Droppos");
        
        Debug.Log(rndx);
        Debug.Log(rndz);

        go.transform.position = new Vector3(rndx, DropY, rndz);
    }
}