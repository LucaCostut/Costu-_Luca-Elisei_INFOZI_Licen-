using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private GameObject player;

    private System.Random rnd = new System.Random();

    [SerializeField]
    private float waveTimer;

    private int maxNrOfEnemies = 1000;
    private int nrOfEnenmies   = 0;
    [SerializeField] private int enemyInWave    = 10;
    [HideInInspector]public GameStage currentGameStage = GameStage.stageOne;


    public enum GameStage
    {
        stageOne    = 0,
        stageTwo    = 1,
        stageTree   = 2
    };

    [Serializable]
    public struct EnemySpawnStats
    {
        public GameObject prefab;
        [Range(1,100)] public int chanceToSpawn;
        public GameStage gameStage;
    }
    [SerializeField] private EnemySpawnStats[] enemies; 

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine(SpawnWave());
    }

    IEnumerator SpawnWave()
    {
        int nrToSpawnInWave = rnd.Next(enemyInWave - 5, enemyInWave + 6);

        if (nrOfEnenmies + nrToSpawnInWave > maxNrOfEnemies)
            nrToSpawnInWave = maxNrOfEnemies - nrOfEnenmies;

        for (int i = 0; i < nrToSpawnInWave; i++)
        {
            if (player == null)
                break;

            int enemyType = GetEnemyType();
            GameObject enemy = Instantiate(enemies[enemyType].prefab);

            enemy.transform.parent = transform;

            int nr = rnd.Next(1,3);
            int posX, posY;

            if (nr == 1) 
            {
                posX = Random.Range(6, 12);
                posY = Random.Range(-5, 5);

                if (posX >= 9)
                    posX = 3 - posX;

                posX = (int)player.transform.position.x + posX;
                posY = (int)player.transform.position.y + posY;
            }
            else
            {
                posX = Random.Range(-5, 5);
                posY = Random.Range(6, 12);

                if (posY >= 9)
                    posY = 3 - posY;

                posX = (int)player.transform.position.x + posX;
                posY = (int)player.transform.position.y + posY;

            }

            enemy.transform.position = new Vector2(posX, posY);
            nrOfEnenmies++;
        }

        if(enemyInWave < 100)
            enemyInWave++;

        yield return new WaitForSeconds(waveTimer);

        if (player != null)
            StartCoroutine(SpawnWave());
    }

    int GetEnemyType()
    {
        int val = 0;
        for (int i = 0; i < enemies.Length; i++)
            val += enemies[i].chanceToSpawn;

        int nr = rnd.Next(1, val + 1);
        int auxVal = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            auxVal += enemies[i].chanceToSpawn;
            if (nr <= auxVal)
                if (enemies[i].gameStage <= currentGameStage)
                    return i;
                else
                    return 0;
        }
        return 0;
    }
}
