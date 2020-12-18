using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{

    public GameObject coin;
    private List<GameObject> coins;

    [Range(0, 100)]
    public int numberOfCoins = 10;
    private float radius = 10.0f;

    void Start()
    {
        coins = new List<GameObject>(); // init as type
        for (int index = 0; index < numberOfCoins; index++)
        {
            Vector3 newCoinLoc = RandomLocation(radius);
            GameObject spawned = Instantiate(coin, newCoinLoc, Quaternion.identity) as GameObject;
            coins.Add(spawned);
        }
    }

    public Vector3 RandomLocation(float radius)
    {
        Vector3 newValidPos = new Vector3(-10000001.0f, -1000000000.0f);
        while(!isLocValid(newValidPos))
        {
            newValidPos = newLoc();
        }

        return newValidPos;
    }

    private bool isLocValid(Vector3 pos)
    {
        if (GlobalVars.Instance.getCurrentDim() != null)
        {
            Collider2D[] colliders = GlobalVars.Instance.getCurrentDim().GetComponentsInChildren<Collider2D>();
            foreach (Collider2D c in colliders)
            {
                if (pos.x < -10000000.0f || c.OverlapPoint(new Vector2(pos.x, pos.y))) return false;
            }
        } else
        {
            Debug.Log("WARNING: we don't have a current dimension!!!");
        }
            return true;
        
    }

    private Vector3 newLoc()
    {
        Vector3 minPos = new Vector3(0.0f, 0.0f);
        Vector3 maxPos = new Vector3(0.0f, 0.0f);

        if (GlobalVars.Instance.getCurrentDim() != null)
        {
            Collider2D[] colliders = GlobalVars.Instance.getCurrentDim().GetComponentsInChildren<Collider2D>();
            foreach (Collider2D c in colliders)
            {
                minPos = c.bounds.min;
                maxPos = c.bounds.max;
                break;
            }
        }
        else
        {
            Debug.Log("WARNING: we don't have a current dimension!!!");
        }
        float randomDirX = Random.Range(minPos.x, maxPos.x);
        float randomDirY = Random.Range(minPos.y, maxPos.y);
        Vector3 randomPos = new Vector3(randomDirX, randomDirY);

        return randomPos;
    }

    void Update()
    {
        foreach (GameObject coin in coins)
        {
            CoinScript actualCoin = coin.GetComponent<CoinScript>();

            if (actualCoin != null && !actualCoin.IsAlive())
            {
                Vector3 newLoc = RandomLocation(radius);
                Debug.Log("reviving at " + newLoc);
                actualCoin.revive(newLoc, GlobalVars.Instance.numDimensions);
            } else if (actualCoin == null)
            {
                Debug.Log("couldn't find any coins to revive");
            }

            if (actualCoin.dimension != GlobalVars.Instance.currentDimensionIter)
            {
                coin.SetActive(false);
            } else
            {
                coin.SetActive(true);
            }

        }
    }
}
