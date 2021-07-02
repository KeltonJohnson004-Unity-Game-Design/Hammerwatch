using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TestMazeConstructor))]
public class TestGameController : MonoBehaviour
{

    private TestMazeConstructor generator;

    private void Start()
    {
        generator = GetComponent<TestMazeConstructor>();
        generator.GenerateNewMaze(13, 15);
    }
}
