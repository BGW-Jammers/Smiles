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
 

  


    //Codi de prova

    public const string floor_white = "0";
    public const string floor_black = "1";
    public const string begin = "b";
    public const string exit = "e";
    public const string stairs = "s";


    void Start()
    {
        string[][] jagged = readFile("Assets/level1.txt");
        string[][] jagged2 = readFile("Assets/level1_upFloor.txt");
        jagged = CreateWorld_1(jagged);
    }


    void Update()
    {



    }

    string[][] CreateWorld_1(string[][] jagged)
    {
        
        // create planes based on matrix
        for (int y = 0; y < jagged.Length; y++)
        {
            for (int x = 0; x < jagged[0].Length; x++)
            {
                switch (jagged[y][x])
                {
                    case floor_white:
                        Instantiate(whiteTile, new Vector3(x, -y, 0), Quaternion.identity);
                        break;
                    case floor_black:
                        Instantiate(blackTile, new Vector3(x, -y, 0), Quaternion.identity);
                        break;

                    case begin:
                        Instantiate(beginTile, new Vector3(x, -y, 0), Quaternion.identity);
                        break;

                    case stairs:
                        Instantiate(stairsTile, new Vector3(x, -y, 0), Quaternion.identity);

                        break;
                    case exit:
                        Instantiate(exitTile, new Vector3(x, -y, 0), Quaternion.identity);

                        break;

                }
            }
        }
    


    return jagged;

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
            levelBase[i] = stringsOfLine;
        }
        return levelBase;
    }


}