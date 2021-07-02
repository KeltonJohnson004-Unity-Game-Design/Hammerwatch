

using System.Collections.Generic;
using UnityEngine;


public class SpawnController:MonoBehaviour
{

    [SerializeField] private GameObject[] treasureChests;
    private Transform playerSpawn;
    public GameObject enemies;
    private int enemiesToSpawn;
    public int baseEnemiesToSpawn;

    public void spawnTreasureChests(int chestsToSpawn)
    {

        GameObject[] roomsToSpawnTreasure = GameObject.FindGameObjectsWithTag("Generated");
        while (chestsToSpawn > 0)
        {
            int randomNum = Random.Range(0, roomsToSpawnTreasure.Length);
            if(!roomsToSpawnTreasure[randomNum].GetComponent<RoomController>().hasSpawned)
            {
                GameObject temp = Instantiate(treasureChests[0], roomsToSpawnTreasure[randomNum].GetComponent<RoomController>().locationToSpawn.transform.position, Quaternion.identity);
                temp.name = "Chest";
                temp.tag = "Chest";
                roomsToSpawnTreasure[randomNum].GetComponent<RoomController>().hasSpawned = true;
                chestsToSpawn--;
            }



        }
    }

    public void spawnEnemies(int difficulty, GameObject player)
    {
        enemiesToSpawn = baseEnemiesToSpawn + 3 * difficulty;
        
        GameObject[] roomsToSpawnEnemies = GameObject.FindGameObjectsWithTag("Generated");
        int attempts = 0;
        while(enemiesToSpawn > 0)
        {
            int random = Random.Range(0, roomsToSpawnEnemies.Length);
            RoomController room = roomsToSpawnEnemies[random].GetComponent<RoomController>();
            if (!room.hasSpawned)
            {
                if(Vector3.Distance(room.locationToSpawn.position, player.transform.position) > 5)
                {
                    
                    room.hasSpawned = true;
                    GameObject spawnedEnemies = Instantiate(enemies, room.locationToSpawn.position, Quaternion.identity);
                    spawnedEnemies.name = "Enemy";
                    spawnedEnemies.tag = "Enemy";
                    EnemyMovement[] newEnemies = spawnedEnemies.GetComponentsInChildren<EnemyMovement>();
                    foreach(EnemyMovement changeEnemy in newEnemies)
                    {
                        //changeEnemy.health += 25 * difficulty;
                        changeEnemy.damage += 3 * difficulty;
                    }
                    enemiesToSpawn--;
                }
                else
                {
                    attempts++;
                }
                    
            }
            else
            {
                attempts++;
            }

            if(attempts > 50)
            {
                break;
            }

        }
    }
}
