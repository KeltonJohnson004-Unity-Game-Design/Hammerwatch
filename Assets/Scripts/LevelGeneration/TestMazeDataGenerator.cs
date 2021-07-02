using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TestMazeDataGenerator
{
    public float placementThreshold;

    public TestMazeDataGenerator()
    {
        placementThreshold = .1f;
    }
    int timesCreated = 0;
    int totalRooms;
    public int[,] FromDimensions(int sizeRows, int sizeCols, int _numOfRooms)
    {

        int[,] maze = new int[sizeRows, sizeCols];
        for (int i = 0; i < sizeRows; i++)
        {
            for (int j = 0; j < sizeCols; j++)
            {
                maze[i, j] = 1;
            }
        }
        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);

        int rStart = rMax / 2;
        int cStart = cMax / 2;

        //maze[rStart, cStart] = 2;

        int numOfRooms = _numOfRooms;
        int invalidInRow = 0;
        int previousDirection = -1;
        int randomNum = 0;
        int attempts = 0;
        do
        {

            if (maze[rStart, cStart] == 1)
            {
                maze[rStart, cStart] = 0;
                numOfRooms--;
                invalidInRow = 0;
            }
            else
            {
                invalidInRow++;
                if(invalidInRow > 5)
                {
                    rStart = rMax / 2;
                    cStart = cMax / 2;
                    invalidInRow = 0;
                    previousDirection = -1;
                    attempts++;
                    if(attempts > 15)
                    {
                        break;
                    }
                }
            }
            
            if(previousDirection >= 0)
            {
                randomNum = Random.Range(0, 7);
                if(randomNum > 4)
                {
                    randomNum = previousDirection;
                }
            }
            else
            {
                randomNum = Random.Range(0, 4);
                previousDirection = randomNum;
            }

            if(randomNum == 0)
            {
                if (rStart + 1 < rMax)
                {
                    rStart += 1;
                   
                }
                else
                {
                    previousDirection = -1;
                }
            }
            else if(randomNum == 1)
            {
                if(cStart + 1 < cMax)
                {
                    cStart += 1;
                   
                }
                else
                {
                    previousDirection = -1;
                }
            }
            else if(randomNum ==2)
            {
                if(rStart - 1 > 0)
                {
                    rStart -= 1;
                   
                }
                else
                {
                    previousDirection = -1;
                }
            }
            else
            {
                if(cStart - 1 > 0)
                {
                    cStart -= 1; 
                   
                }
                else
                {
                    previousDirection = -1;
                }
            }
           
        } while (numOfRooms > 0);

        timesCreated++;
        totalRooms += (_numOfRooms - numOfRooms);

        return maze;
    }
}
