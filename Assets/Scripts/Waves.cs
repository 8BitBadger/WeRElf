using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    float level = 0;
    int levelOfEnemies = 0;
    int enemiesToSpawn = 0;

    //Used for the wave timer
    int timeBetweenWaves = 10;
    float timer = 0;

    public GameObject OrcLvl1;
    public GameObject OrcLvl2;
    public GameObject OrcLvl3;
    public GameObject OrcLvl4;
    public GameObject OrcLvl5;

    List<GameObject> enemyList;

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
        if((Time.time - timer) > timeBetweenWaves)
        {
            timer = Time.time;
            StartWave();
        }
    }

    // Use this for initialization
    void StartWave()
    {
        //Gets the amount of enemies to spawn for he level
        enemiesToSpawn = Random.Range(0, 5);
        //Get the level of the enemies to spawn
        levelOfEnemies = 1;//Mathf.RoundToInt(level / 5);
        //Select a side to spawn on and the spawn point on that side
        //North = 0, South = 1, East = 2, West = 3;
        Vector2 spawnPoint = Vector2.zero;

        
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
    }

    // Update is called once per frame
    void Update()
    { 
        for(int i = 0; i < enemyList.Count; i++)
        {
            if(enemyList[i].GetComponent<Orc>().Health <= 0)
            {
                Destroy(enemyList[i]);
                enemyList.RemoveAt(i);
            }
        }

        //When all of the enemies are destroyed we start the new countdown for the next wave 'and 
        if (enemyList.Count == 0)
        {
            level++;
            CountDown();
        }
    }
}
