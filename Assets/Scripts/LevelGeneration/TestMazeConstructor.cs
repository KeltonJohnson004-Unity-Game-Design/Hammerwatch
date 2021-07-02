using System.Collections;
using System.Linq.Expressions;
using UnityEngine;
using TMPro;

public class TestMazeConstructor : MonoBehaviour
{
    public bool showDebug;


    private TestMazeDataGenerator dataGenerator;
    [SerializeField] private SpawnController spawnController;


    [SerializeField] private TestMazeGenerator mazeGenerator;


    [SerializeField] private GameObject player;
    InputMaster inputMaster;
    [SerializeField]
    private int numRows;
    [SerializeField]
    private int numCols;
    [SerializeField]
    private int numRooms;
    [SerializeField]
    private int baseNumRows = 15;
    [SerializeField]
    private int baseNumCols = 15;
    [SerializeField]
    private int baseNumRooms = 10;
    private int treasuresToSpawn = 0;
    private int difficulty;
    public TMP_Text tmp;




    public int[,] data
    {
        get; private set;
    }

    private void Awake()
    {
        data = new int[,]
        {
            {1,1,1 },
            {1,0,1 },
            {1,1,1 }
        };

        dataGenerator = new TestMazeDataGenerator();

        difficulty = -1;
        mazeGenerator.setValues(data);
        inputMaster = new InputMaster();

        BuildLevel();
    }

    public void BuildLevel()
    {
        Debug.Log("Level Build");
        StartCoroutine(levelSpawn());

    }

    IEnumerator levelSpawn()
    {
        difficulty++;
        tmp.text = (difficulty + 1).ToString();
        numRows = baseNumRows + 3 * difficulty;
        numCols = baseNumCols + 3 * difficulty;
        numRooms = baseNumRooms + 15 * difficulty;
        data = dataGenerator.FromDimensions(numRows, numCols, numRooms);
        mazeGenerator.setData(data);
        mazeGenerator.destroyMaze();
        yield return new WaitForSeconds(1);
        mazeGenerator.createMaze(player);
        spawnController.spawnTreasureChests(treasuresToSpawn);
        spawnController.spawnEnemies(difficulty, player);

    }

    public void GenerateNewMaze(int sizeRows, int sizeCols)
    {
        data = dataGenerator.FromDimensions(sizeRows, sizeCols, numRooms);
    }


    private void OnGUI()
    {
        if(!showDebug)
        {
            return;
        }

        int[,] maze = data;
        int rmax = maze.GetUpperBound(0);
        int cmax = maze.GetUpperBound(1);

        string msg = "";

        for(int i = rmax; i>= 0; i--)
        {
            for(int j = 0; j <=cmax; j++)
            {
                if(maze[i,j] == 0)
                {
                    msg += "....";
                }
                else
                {
                    msg += "==";
                }
            }
            msg += "\n";
        }
        GUI.Label(new Rect(20, 20, 500, 500), msg);
    }


}
