using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    float level = 1;
    int levelOfEnemies = 0;
    int enemiesToSpawn = 0;

    //Used for the wave timer
    //int timeBetweenWaves = 20;
    int timeBetweenWaves = 2;
    float timer = 0;
    int timeRemaining;
    [HideInInspector]
    public float startTimer;

    public GameObject OrcLvl1;
    public GameObject OrcLvl2;
    public GameObject OrcLvl3;
    public GameObject OrcLvl4;
    public GameObject OrcLvl5;

    List<GameObject> enemyList;

    [HideInInspector]
    public bool startCountdown = false;

    public float Level
    {
        get{return level;}
    }

    //The player must survive 20 waves
    //This class needs to keep track of all the enemies in every wave
    //Need to keep track of the current wave
    //Use mathf.log for level of working out how many enemies are spawned
    //Once the enemy list is empty we can move on to a new level

    private void Start()
    {
        enemyList = new List<GameObject>();
    }

    public void CountDown()
    {
        timeRemaining = Mathf.FloorToInt(timeBetweenWaves - (Time.time - startTimer - timer));
        if ((Time.time - startTimer - timer) > timeBetweenWaves)
        {
            //Reset the timer to start over again
            timer = Time.time;
            startTimer = 0;
            StartWave();
        }
    }

    public int WaveCountdown()
    {
        return timeRemaining; 
    }

    // Use this for initialization
    void StartWave()
    {
        //Gets the amount of enemies to spawn for he level
        //enemiesToSpawn = Random.Range(1, 5);
        enemiesToSpawn = 5;
        //Get the level of the enemies to spawn
        //if(level < 5) levelOfEnemies = 1; else levelOfEnemies = Mathf.CeilToInt(level / 5);
        //Select a side to spawn on and the spawn point on that side
        Vector2 spawnPoint = Vector2.zero;
        levelOfEnemies = 5;

        //Run trough how many enemies must be made and spaw them
        for (int i = 0; i < enemiesToSpawn; i++)
        {
           //Generate a different spawn point for every enemy
            int side = Random.Range(0, 4);
            if (side == 0) { spawnPoint = new Vector2(Random.Range(0, 32), 32); }
            if (side == 1) { spawnPoint = new Vector2(Random.Range(0, 32), 0); }
            if (side == 2) { spawnPoint = new Vector2(32, Random.Range(0, 32)); }
            if (side == 3) { spawnPoint = new Vector2(0, Random.Range(0, 32)); }

            if (levelOfEnemies == 1)
            {
               GameObject tempEnemy = Instantiate(OrcLvl1, spawnPoint, Quaternion.identity);
               enemyList.Add(tempEnemy);
            }
            if (levelOfEnemies == 2)
            {
                GameObject tempEnemy = Instantiate(OrcLvl2, spawnPoint, Quaternion.identity);
                enemyList.Add(tempEnemy);
            }
            if (levelOfEnemies == 3)
            {
                GameObject tempEnemy = Instantiate(OrcLvl3, spawnPoint, Quaternion.identity);
                enemyList.Add(tempEnemy);
            }
            if (levelOfEnemies == 4)
            {
                GameObject tempEnemy = Instantiate(OrcLvl4, spawnPoint, Quaternion.identity);
                enemyList.Add(tempEnemy);
            }
            if (levelOfEnemies == 5)
            {
                GameObject tempEnemy = Instantiate(OrcLvl5, spawnPoint, Quaternion.identity);
                enemyList.Add(tempEnemy);
            }
        }
        print("Health = " + enemyList[0].GetComponent<Orc>().Health);
    } 

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i].GetComponent<Orc>().Health <= 0)
            {
                Destroy(enemyList[i]);
                enemyList.RemoveAt(i);
            }
        }

        //When all of the enemies are destroyed we start the new countdown for the next wave 'and 
        if (enemyList.Count == 0 && startCountdown)
        {
            level++;
            CountDown();
        }
    }
}
