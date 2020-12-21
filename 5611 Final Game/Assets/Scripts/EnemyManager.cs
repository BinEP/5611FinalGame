using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<EnemyAgent> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<EnemyAgent>();
        EnemyAgent[] tempEnemies = GetComponentsInChildren<EnemyAgent>();
        foreach (EnemyAgent enemy in tempEnemies)
        {
            enemy.gameObject.SetActive(false);
            enemies.Add(enemy);
        }

        GlobalVars.maxNumEnemies = enemies.Capacity;
    }

    // Update is called once per frame
    void Update()
    {
        float toCompare = GlobalVars.totalCoins / 5.0f;
        if (toCompare > GlobalVars.numEnemies)
        {
            AddEnemy();
            Debug.Log("We now have " + GlobalVars.numEnemies + " enemies");
        }
    }

    public void AddEnemy()
    {
        if (GlobalVars.numEnemies < GlobalVars.maxNumEnemies)
        {
            foreach(EnemyAgent enemy in enemies)
            {
                if (!enemy.gameObject.activeInHierarchy)
                {
                    enemy.gameObject.SetActive(true);
                    GlobalVars.numEnemies++;
                    break;
                } 
            }
        }
    }
}
