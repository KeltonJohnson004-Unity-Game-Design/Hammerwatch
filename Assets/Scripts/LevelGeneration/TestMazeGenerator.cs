using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.AI;

public class TestMazeGenerator: MonoBehaviour
{
    private float width;
    int[,] data;

    public GameObject[] corridors;
    public GameObject[] corners1;
    public GameObject[] corners2;
    public GameObject[] corners3;
    public GameObject[] corners4;
    public GameObject[] singleWalls1;
    public GameObject[] singleWalls2;
    public GameObject[] singleWalls3;
    public GameObject[] singleWalls4;
    public GameObject[] deadEnds;
    public GameObject[] corridors1;
    public GameObject[] corridors2;
    public GameObject open;

    public NavMeshSurface surface;

    private List<GameObject> potentialStartAndEndLocations = new List<GameObject>();

    int rmax;
    int cmax;
    public void setValues(int[,] _data)
    {
        width = 1f;
        data = _data;

    }


    public void destroyMaze()
    {
        GameObject[] generateParts = GameObject.FindGameObjectsWithTag("Generated");
        foreach (GameObject partToDelete in generateParts)
        {

            Destroy(partToDelete);
        }
        GameObject[] chestParts = GameObject.FindGameObjectsWithTag("Chest");
        foreach (GameObject partToDelete in chestParts)
        {

            Destroy(partToDelete);
        }
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject partToDelete in enemies)
        {

            Destroy(partToDelete);
        }
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject partToDelete in projectiles)
        {

            Destroy(partToDelete);
        }
    }

    public Transform createMaze(GameObject player)
    {
        potentialStartAndEndLocations = new List<GameObject>();

        Transform playerSpawn = null;

        rmax = data.GetUpperBound(0);
        cmax = data.GetUpperBound(1);

        bool isUp = true;
        bool isRight = true;
        bool isDown = true;
        bool isLeft = true;

        List<int> potentailI = new List<int>();
        List<int> potentailj = new List<int>();


        for (int i = 0; i < rmax; i++)
        {
            for(int j = 0; j < cmax; j++)
            {

                isUp = CheckUp(i, j);
                isRight = CheckRight(i, j);
                isDown = CheckDown(i, j);
                isLeft = CheckLeft(i, j);

                GameObject createObject = null;

                if (isUp && isRight && isDown && isLeft && data[i,j] == 0)
                {

                    createObject = Instantiate(open, new Vector3(i * width, 0f, j * width),Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && isRight && isDown && isLeft && data[i,j] == 0)
                {
                    int randomWallSelection = Random.Range(0, singleWalls3.Length);
                    createObject = Instantiate(singleWalls3[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && isRight && !isDown && isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, singleWalls4.Length);
                    createObject = Instantiate(singleWalls4[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && !isRight && isDown && isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, singleWalls2.Length);
                    createObject = Instantiate(singleWalls2[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && isRight && isDown && !isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, singleWalls1.Length);
                    createObject = Instantiate(singleWalls1[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && !isRight && isDown && !isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, corridors2.Length);
                    createObject = Instantiate(corridors2[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && isRight && !isDown && isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, corridors1.Length);
                    createObject = Instantiate(corridors1[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && !isRight && isDown && isLeft && data[i, j] == 0)//up and left == 0
                {

                    int randomWallSelection = Random.Range(0, corners2.Length);
                    createObject = Instantiate(corners2[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && isRight && isDown && !isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, corners3.Length);
                    createObject = Instantiate(corners3[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && isRight && !isDown && !isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, corners4.Length);
                    createObject = Instantiate(corners4[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && !isRight && !isDown && isLeft && data[i, j] == 0)
                {
                    int randomWallSelection = Random.Range(0, corners1.Length);
                    createObject = Instantiate(corners1[randomWallSelection], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && !isRight && !isDown && isLeft && data[i, j] == 0)
                {
                     createObject = Instantiate(deadEnds[3], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && isRight && !isDown && !isLeft && data[i, j] == 0)
                {
                     createObject = Instantiate(deadEnds[1], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (!isUp && !isRight && isDown && !isLeft && data[i, j] == 0)
                {
                     createObject = Instantiate(deadEnds[0], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }
                else if (isUp && !isRight && !isDown && !isLeft && data[i, j] == 0)
                {
                     createObject = Instantiate(deadEnds[2], new Vector3(i * width, 0f, j * width), Quaternion.identity);
                    createObject.name = "Maze";
                    createObject.tag = "Generated";
                }

                if (createObject != null && !isUp)
                {
                    potentialStartAndEndLocations.Add(createObject);

                }
            }
        }
        bool startEndSet = false;
        int distanceToEnd = (int)(Math.Sqrt(rmax * rmax + cmax * cmax) * 5);
        while(!startEndSet)
        {
            int randomNum = Random.Range(0, potentialStartAndEndLocations.Count - 1);
            for(int i = 0; i < potentialStartAndEndLocations.Count; i++)
            {
                if(Vector3.Distance(potentialStartAndEndLocations[randomNum].transform.position, potentialStartAndEndLocations[i].transform.position) > distanceToEnd && randomNum != i)
                {
                    potentialStartAndEndLocations[i].GetComponent<RoomController>().setRoomStart();
                    player.transform.position = potentialStartAndEndLocations[i].GetComponent<RoomController>().locationToSpawn.transform.position + new Vector3(0f,.05f,0f);
                    potentialStartAndEndLocations[randomNum].GetComponent<RoomController>().setRoomEnd();
                    potentialStartAndEndLocations[i].GetComponent<RoomController>().hasSpawned = true;
                    potentialStartAndEndLocations[randomNum].GetComponent<RoomController>().hasSpawned = true;
                    startEndSet = true;
                    playerSpawn = potentialStartAndEndLocations[i].GetComponent<RoomController>().locationToSpawn.transform;
                    break;
                }
            }
            distanceToEnd -= 10;
        }
        surface.BuildNavMesh();
        return playerSpawn;
    }

    private bool CheckUp(int i, int j)
    {
        if(i == rmax)
        {
            return false;
        }
        return data[i + 1, j] != 1;
    }
    private bool CheckDown(int i, int j)
    {
        if (i == 0)
        {
            return false;
        }
        
        return data[i - 1, j] != 1;
    }
    private bool CheckLeft(int i, int j)
    {
        if(j == 0)
        {
            return false;
        }
        return data[i, j - 1] != 1;
    }
    private bool CheckRight(int i, int j)
    {
        if(j == cmax)
        {
            return false;
        }
        return data[i, j+1] != 1;
    }


    public void setData(int[,] data)
    {
        this.data = data;
    }
}
