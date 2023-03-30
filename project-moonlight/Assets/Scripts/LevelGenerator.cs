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
    List<GameObject> gameObjects = new List<GameObject>();
    [SerializeField] GameObject cam;
    [SerializeField] List<GameObject> segment4Exits = new List<GameObject>();
    [SerializeField] List<GameObject> segment3Exits = new List<GameObject>();
    [SerializeField] List<GameObject> segment2Exits = new List<GameObject>();
    [SerializeField] List<GameObject> segment1Exit = new List<GameObject>();
    [SerializeField] List<GameObject> mapObjects = new List<GameObject>();
    // Map dimensions
    int rows = 8;
    int cols = 8;
    Queue<Tuple<int, int>> points = new Queue<Tuple<int, int>>();
    int randRow;
    int randCol;
    // Initialize the grid
    string[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < rows*cols; i++)
        {
            string name = $"Square ({i + 1})";
            gameObjects.Add(GameObject.Find(name));
            name = $"Image ({i})";
            mapObjects.Add(GameObject.Find(name));
            mapObjects[i].GetComponent<Image>().color = new Color(255, 255, 255, 0);
        }
        grid = new string[rows, cols];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                grid[i, j] = ".";
            }
        }

        // Place the 'x' in the center of the grid
        int centerRow = rows / 2;
        int centerCol = cols / 2;
        grid[centerRow, centerCol] = "o";

        for (int iterates = 0; iterates < 12; iterates++)
        {
            // Generate a random number of 'x' values between 1 and 3
            System.Random random = new System.Random();
            int numX = 1;
            int rand = random.Next(0, 10);
            if (rand < 8)
            {
                numX = random.Next(1, 2);
            }
            else
            {
                numX = random.Next(1, 3);
            }
        
            if (iterates == 0)
            {
                randRow = centerRow;
                randCol = centerCol;
            }
            else
            {
                Tuple<int, int> temp = points.Dequeue();
                randRow = temp.Item1;
                randCol = temp.Item2;
            }
            for (int i = 0; i < numX; i++)
            {
                // Place the 'x' in a random direction around the center
                int randDirection = random.Next(6);
                int randOffset = 1;


                switch (randDirection)
                {
                    case 0: // North
                        randRow -= randOffset;
                        break;
                    case 1: // East
                        randCol += randOffset;
                        break;
                    case 2: // South
                        randRow += randOffset;
                        break;
                    case 3: // West
                        randCol -= randOffset;
                        break;
                    case 4: // West
                        randCol -= randOffset;
                        break;
                    case 5: // East
                        randCol += randOffset;
                        break;

                }

                // Ensure that the random 'x' is within the bounds of the grid
                randRow = Math.Max(0, Math.Min(rows - 1, randRow));
                randCol = Math.Max(0, Math.Min(cols - 1, randCol));

                if (grid[randRow, randCol] == "x" || grid[randRow, randCol] == "o")
                {
                    continue;
                }

                grid[randRow, randCol] = "x";
                
            }
            points.Enqueue(new Tuple<int, int>(randRow, randCol));
        }

        int counter = 0;
        bool first = true;
        // Check each 'x' for its neighboring 'x' and from which direction
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
                    if(first)
                    {
                        Transform cameraSpawn = gameObjects[counter].transform;
                        cam.transform.position = new Vector3(cameraSpawn.position.x + 0.2f, cameraSpawn.position.y, cam.transform.position.z);
                        first = false;
                    }
                    

                    // Check north neighbor
                    if (j > 0 &&( grid[j - 1, i] == "x"))
                    {
                        top = true;
                    }

                    // Check east neighbor
                    if (i < cols - 1 && grid[j, i + 1] == "x")
                    {
                        right = true;
                    }

                    // Check south neighbor
                    if (j < rows - 1 && grid[j + 1, i] == "x")
                    {
                        bottom = true;
                    }

                    // Check west neighbor
                    if (i > 0 && grid[j, i - 1] == "x")
                    {
                        left = true;
                    }

                    mapObjects[counter].GetComponent<Image>().color = new Color(255, 255, 255, 0.7f);
                    SpawnAllSides(top, bottom, left, right, counter);
                }
                counter++;
            }
        }
    }

    void SpawnAllSides(bool top, bool bottom, bool left, bool right, int counter)
    {
        Debug.Log(counter);
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
}