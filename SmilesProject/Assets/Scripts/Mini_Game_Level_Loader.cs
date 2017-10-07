using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Mini_Game_Level_Loader : MonoBehaviour
{
    public GameObject player;
   

    private List<GameObject> tiles_first_floor_level_1;
    public GameObject blackTile;
    public GameObject whiteTile;
    public GameObject beginTile;
    public GameObject exitTile;
    public GameObject stairsTile;
    public GameObject upperStairsTile;
    public GameObject voidTile;
    public GameObject wallTile;


    //Codi de prova

    public const string floor_white = "0";
    public const string floor_black = "1";
    public const string begin = "b";
    public const string exit = "e";
    public const string stairs = "s";
    public const string upper_stairs = "v";
    public const string tile_void = "o";
    public const string wall = "w";
    public const string transition = "x";

    public string[][] jagged;
    public string[][] jagged2;

    private bool areWeUpstairs = false;

    void Start()
    {
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
            jagged = CreateWorld_1(jagged2);
            areWeUpstairs = true;
        }

        else if (GameObject.Find("Character").GetComponent<Character>().floor == 0 && areWeUpstairs)
        {
            DestroyCurrentWorld();
            jagged = CreateWorld_1(jagged);
            areWeUpstairs = false;
        }
    }

    string[][] CreateWorld_1(string[][] jagged)
    {
        GameObject tmp = null;
        GameObject parent = new GameObject("World");
        // create planes based on matrix
        for (int y = 0; y < jagged.Length; y++)
        {
            for (int x = 0; x < jagged[0].Length; x++)
            {
                switch (jagged[y][x])
                {
                    case floor_white:
                        Instantiate(whiteTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                        break;
                    case floor_black:
                        Instantiate(blackTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                        break;

                    case begin:
                        {
                            tmp = Instantiate(beginTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            tmp.name = "StartTile";
                            break;
                        }

                    case stairs:
                        Instantiate(stairsTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);

                        break;
                    case upper_stairs:
                        {
                            Instantiate(upperStairsTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
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
                            Instantiate(wallTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }

                    case tile_void:
                        {
                            Instantiate(voidTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }

                    case transition:
                        {
                            Instantiate(voidTile, new Vector3(x, -y, 0), Quaternion.identity, parent.transform);
                            break;
                        }


                }
            }
        }
        return jagged;
    }

    void DestroyCurrentWorld()
    {
        Destroy(GameObject.Find("World"));
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


}