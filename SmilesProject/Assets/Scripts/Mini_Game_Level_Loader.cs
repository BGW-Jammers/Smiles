using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Mini_Game_Level_Loader : MonoBehaviour
{
    public int CURRENT_LEVEL = 1;
    public GameObject player;
   
    public GameObject blackTile;
    public GameObject whiteTile;
    public GameObject beginTile;
    public GameObject exitTile;
    public GameObject stairsTile;
    public GameObject upperStairsTile;
    public GameObject voidTile;
    public GameObject wallTile;
    public GameObject bombActive;
    public GameObject bombDeactive;
    public GameObject obstacleTile;

    public const string floor_white = "0";
    public const string floor_black = "1";
    public const string begin = "b";
    public const string exit = "e";
    public const string stairs = "s";
    public const string upper_stairs = "v";
    public const string tile_void = "o";
    public const string wall = "w";
    public const string transition = "x";
    public const string bomb_activated = "z";
    public const string bomb_deactivated = "Z";
    public const string obstacle = "3";

    public string[][] jagged;
    public string[][] jagged2;

    public GameObject[,] currentWorld;

    private bool areWeUpstairs = false;

    void Awake()
    {
        CURRENT_LEVEL = 1;
        jagged = readFile("Assets/level1.txt");
        jagged2 = readFile("Assets/level1_upFloor.txt");
        jagged = CreateWorld_1(jagged);
        areWeUpstairs = false;
    }


    void Update()
    {
        if (GameObject.Find("Character").GetComponent<Character>().floor == 1 && !areWeUpstairs)
        {
            DestroyCurrentWorld();
            jagged2 = CreateWorld_1(jagged2);
            areWeUpstairs = true;

            GameObject per = GameObject.Find("Character");
            per.transform.position = GameObject.Find("StartTile").transform.position;
            per.GetComponent<Character>().positionX = 0;
            per.GetComponent<Character>().positionY = 4;
        }

        if (GameObject.Find("Character").GetComponent<Character>().floor == 0 && areWeUpstairs)
        {
            DestroyCurrentWorld();
            jagged = CreateWorld_1(jagged);
            areWeUpstairs = false;

            GameObject per = GameObject.Find("Character");
            per.transform.position = GameObject.Find("UpperStairTile").transform.position;
            per.GetComponent<Character>().positionX = 0;
            per.GetComponent<Character>().positionY = 4;
        }
    }

    void LevelSelector()
    {
        if (CURRENT_LEVEL == 1)
        {
            jagged = readFile("Assets/level1.txt");
            jagged2 = readFile("Assets/level1_upFloor.txt");
        }
        if (CURRENT_LEVEL == 2)
        {

            //jagged = readFile("Assets/level1.txt");
            //jagged2 = readFile("Assets/level1_upFloor.txt");
        }
        if (CURRENT_LEVEL == 3)
        {
            //jagged = readFile("Assets/level1.txt");
            //jagged2 = readFile("Assets/level1_upFloor.txt");
        }
    }

    public string[][] CreateWorld_1(string[][] jagged)
    {
        GameObject tmp = null;
        GameObject parent = new GameObject("World");
        currentWorld = new GameObject[20, 20];
        // create planes based on matrix
        for (int y = 0; y < jagged.Length; y++)
        {
            for (int x = 0; x < jagged[0].Length; x++)
            {
                switch (jagged[y][x])
                {
                    case floor_white:
                        {
                            tmp = Instantiate(whiteTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }

                    case floor_black:
                        {
                            tmp = Instantiate(blackTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }

                    case begin:
                        {
                            tmp = Instantiate(beginTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            tmp.name = "StartTile";
                            break;
                        }

                    case stairs:
                        {
                            tmp = Instantiate(stairsTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }
                    case upper_stairs:
                        {
                            tmp = Instantiate(upperStairsTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            tmp.name = "UpperStairTile";
                            break;
                        }
                    case exit:
                        {
                            tmp = Instantiate(exitTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            tmp.name = "ExitTile";
                            break;
                        }

                    case wall:
                        {
                            tmp = Instantiate(wallTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }

                    case tile_void:
                        {
                            tmp = Instantiate(voidTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }

                    case transition:
                        {
                            tmp = Instantiate(voidTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }
                    case bomb_activated:
                        {
                            tmp = Instantiate(bombActive, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }
                    case bomb_deactivated:
                        {
                            tmp = Instantiate(bombDeactive, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }
                    case obstacle:
                        {
                            tmp = Instantiate(wallTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }
                }
                currentWorld[y,x] = tmp;
            }
        }
        return jagged;
    }

    public void DestroyCurrentWorld()
    {
        Destroy(GameObject.Find("World"));
        LevelSelector();
    }

    string[][] readFile(string file)
    {
        string text = System.IO.File.ReadAllText(file);
        string[] lines = Regex.Split(text, "\r\n");
        int rows = lines.Length;
       
        string[][] levelBase = new string[rows][];
        for (int i = 0; i < lines.Length; i++)
        {
            string[] stringsOfLine = Regex.Split(lines[i], " ");
            Debug.Log(i + " - " + lines[i]);
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }

    public void CreateBomb(int positionY, int positionX)
    {
        System.Array.Reverse(jagged);
        jagged[positionY][positionX] = "z";
        currentWorld[positionY, positionX] = Instantiate(bombActive, new Vector3(positionY, -positionX, 0), Quaternion.identity, GameObject.Find("World").transform);
        System.Array.Reverse(jagged);
        Debug.Log("bomb");
    }
}