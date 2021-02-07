using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBase : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ExplosionEfx;
    public float ExplosionDelay = 3f;

    private float mTimer = 0;

    bool isExplosion = false;
    void Start()
    {
        mTimer = ExplosionDelay;
        isExplosion = false;
    }

    // Update is called once per frame
    void Update()
    {
        mTimer -= Time.deltaTime;
        if (!isExplosion && mTimer <= 0)
        {
            isExplosion = true;
            var efx = GameObject.Instantiate(ExplosionEfx);
            efx.transform.position = this.transform.position;
            this.gameObject.SetActive(false);
        }
    }
}
