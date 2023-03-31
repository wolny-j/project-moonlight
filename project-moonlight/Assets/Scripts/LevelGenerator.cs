using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    //Lists with gameobjects
    private List<GameObject> gameObjects = new List<GameObject>();
    private List<GameObject> mapObjects = new List<GameObject>();
    [SerializeField] private GameObject cam;
    [SerializeField] private List<GameObject> segment4Exits = new List<GameObject>();
    [SerializeField] private List<GameObject> segment3Exits = new List<GameObject>();
    [SerializeField] private List<GameObject> segment2Exits = new List<GameObject>();
    [SerializeField] private List<GameObject> segment1Exit = new List<GameObject>();
    
    // Map dimensions
    private const int rows = 8;
    private const int cols = 8;

    int randRow;
    int randCol;

    int centerRow;
    int centerCol;
    Tuple<int, int> point;

    //Queue with grind points to place segments
    Queue<Tuple<int, int>> points = new Queue<Tuple<int, int>>();

    // Initialize the grid
    string[,] grid;

    //Const values 
    private const int ALGHORITHM_ITERATIONS = 10;
    private const int OFFSET = 1;
    

    // Start is called before the first frame update
    void Start()
    {
        InitializeGameObjects();

        InitializeGrid();

        GenerateMap();

        CheckNeighbors();
    }



    void InitializeGameObjects()
    {
        for (int i = 0; i < rows * cols; i++)
        {
            string squareName = $"Square ({i + 1})";
            GameObject squareGO = GameObject.Find(squareName);
            gameObjects.Add(squareGO);

            string imageName = $"Image ({i})";
            GameObject imageGO = GameObject.Find(imageName);
            mapObjects.Add(imageGO);
            imageGO.GetComponent<Image>().color = Color.clear;
        }
    }


    private void InitializeGrid()
    {
        grid = new string[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = ".";
            }
        }

        centerRow = rows / 2;
        centerCol = cols / 2;
        grid[centerRow, centerCol] = "x";
    }

    void GenerateMap()
    {
        for (int iteration = 0; iteration < ALGHORITHM_ITERATIONS; iteration++)
        {
            // Generate a random number of 'x' values between 1 and 3
            System.Random random = new System.Random();
            int numX = GetRandomSegmentNumber(random);

            GetNextPointFromQueue(iteration);

            for (int i = 0; i < numX; i++)
            {
                // Place the 'x' in a random direction around the center
                int randDirection = random.Next(6);

                point = GetNeighborPoint(point, randDirection, OFFSET);

                // Ensure that the random 'x' is within the bounds of the grid
                randRow = Math.Max(0, Math.Min(rows - 1, point.Item1));
                randCol = Math.Max(0, Math.Min(cols - 1, point.Item2));

                point = new Tuple<int, int>(randRow, randCol);

                if (grid[point.Item1, point.Item2] == "x")
                {
                    continue;
                }

                grid[point.Item1, point.Item2] = "x";

            }

            points.Enqueue(point);
        }
    }

    private void CheckNeighbors()
    {
        int counter = 0;
        bool first = true;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                bool top = false;
                bool bottom = false;
                bool left = false;
                bool right = false;

                if (grid[j, i] == "x")
                {
                    if (first)
                    {
                        SetCameraSpawn(counter);
                        first = false;
                    }

                    CheckTop(j, i, ref top);
                    CheckRight(j, i, ref right);
                    CheckBottom(j, i, ref bottom);
                    CheckLeft(j, i, ref left);


                    mapObjects[counter].GetComponent<Image>().color = new Color(255, 255, 255, 0.7f);
                    InitializeSegments(top, bottom, left, right, counter);
                }
                counter++;
            }
        }
    }

    void InitializeSegments(bool top, bool bottom, bool left, bool right, int counter)
    {
        int mask = (top ? 8 : 0) | (bottom ? 4 : 0) | (left ? 2 : 0) | (right ? 1 : 0);

        switch (mask)
        {
            case 0: // 0000
                    // No walls
                break;
            case 1: // 0001
                //Right wall only
                Instantiate(segment1Exit[3], gameObjects[counter].transform);
                break;
            case 2: // 0010
                Instantiate(segment1Exit[0], gameObjects[counter].transform);
                // Left wall only
                break;
            case 3: // 0011
                Instantiate(segment2Exits[0], gameObjects[counter].transform);
                // Left and right walls
                break;
            case 4: // 0100
                Instantiate(segment1Exit[1], gameObjects[counter].transform);
                // Bottom wall only
                break;
            case 5: // 0101
                Instantiate(segment2Exits[4], gameObjects[counter].transform);
                // Bottom and right walls
                break;
            case 6: // 0110
                Instantiate(segment2Exits[3], gameObjects[counter].transform);
                // Bottom and left walls
                break;
            case 7: // 0111
                Instantiate(segment3Exits[2], gameObjects[counter].transform);
                // Bottom, left, and right walls
                break;
            case 8: // 1000
                Instantiate(segment1Exit[2], gameObjects[counter].transform);
                // Top wall only
                break;
            case 9: // 1001
                Instantiate(segment2Exits[2], gameObjects[counter].transform);
                // Top and right walls
                break;
            case 10: // 1010
                Instantiate(segment2Exits[5], gameObjects[counter].transform);
                // Top and left walls
                break;
            case 11: // 1011
                Instantiate(segment3Exits[1], gameObjects[counter].transform);
                // Top, left, and right walls
                break;
            case 12: // 1100
                Instantiate(segment2Exits[1], gameObjects[counter].transform);
                // Top and bottom walls
                break;
            case 13: // 1101
                Instantiate(segment3Exits[3], gameObjects[counter].transform);
                // Top, bottom, and right walls
                break;
            case 14: // 1110
                Instantiate(segment3Exits[0], gameObjects[counter].transform);
                // Top, bottom, and left walls
                break;
            case 15: // 1111
                Instantiate(segment4Exits[0], gameObjects[counter].transform);
                // All walls
                break;
        }
    }

    private Tuple<int, int> GetNeighborPoint(Tuple<int, int> point, int direction, int offset)
    {
        var (row, col) = point;

        switch (direction)
        {
            case 0: // North
                row -= offset;
                break;
            case 1: // East
                col += offset;
                break;
            case 2: // South
                row += offset;
                break;
            case 3: // West
                col -= offset;
                break;
            case 4: // West
                col -= offset;
                break;
            case 5: // East
                col += offset;
                break;
        }

        return new Tuple<int, int>(row, col);
    }

    private void CheckTop(int j, int i, ref bool top)
    {
        if (j > 0 && grid[j - 1, i] == "x")
        {
            top = true;
        }
    }

    private void CheckRight(int j, int i, ref bool right)
    {
        if (i < cols - 1 && grid[j, i + 1] == "x")
        {
            right = true;
        }
    }

    private void CheckBottom(int j, int i, ref bool bottom)
    {
        if (j < rows - 1 && grid[j + 1, i] == "x")
        {
            bottom = true;
        }
    }

    private void CheckLeft(int j, int i, ref bool left)
    {
        if (i > 0 && grid[j, i - 1] == "x")
        {
            left = true;
        }
    }

    private void SetCameraSpawn(int counter)
    {
        Transform cameraSpawn = gameObjects[counter].transform;
        cam.transform.position = new Vector3(cameraSpawn.position.x + 0.2f, cameraSpawn.position.y, cam.transform.position.z);
    }

    int GetRandomSegmentNumber(System.Random seed)
    {
        int rand = seed.Next(0, 10);
        if (rand < 7)
        {
            return seed.Next(1, 4); ;
        }
        else
        {
            return seed.Next(0, 1);
        }
    }

    void GetNextPointFromQueue(int iteration)
    {
        if (iteration == 0)
        {
            point = new Tuple<int, int>(centerRow, centerCol);
        }
        else
        {
            point = points.Dequeue();
        }
    }
}