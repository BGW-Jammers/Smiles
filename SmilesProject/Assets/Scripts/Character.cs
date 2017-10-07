using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState
{
    IDLE,
    WALKING_TOP,
    WALKING_BOT,
    WALKING_LEFT,
    WALKING_RIGHT,
    DEPLOYING_BOMB
}

public class Character : MonoBehaviour
{
    Mini_Game_Level_Loader Map = null;
    Animator Anim = null;
    SpriteRenderer Sprite = null;

    public CharacterState currentAction;

    public int positionX = 0;
    public int positionY = 4;
    public int floor = 0;
    public float speed = 1.0f;

    //BOMB 1
    public float timeToBreak1 = 10000;
    public float timebreak1 = 2;
    private bool isTicking1 = false;
    int cachedX1;
    int cachedY1;
    //BOMB 2
    public float timeToBreak2 = 10000;
    public float timebreak2 = 2;
    private bool isTicking2 = false;
    int cachedX2;
    int cachedY2;
    //BOMB 3
    public float timeToBreak3 = 10000;
    public float timebreak3 = 2;
    private bool isTicking3 = false;
    int cachedX3;
    int cachedY3;

    float tileInternalDistance = 0.0f;
    bool isMoving = false;

    Vector3 destination;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
        Map = GameObject.Find("GameManager").GetComponent<Mini_Game_Level_Loader>();
        transform.position = GameObject.Find("StartTile").transform.position;
        Sprite = GetComponent<SpriteRenderer>();
        
        positionX = 0;
        positionY = 4;

        GetTileInternalDistance();
    }

    // Update is called once per frame
    void Update()
    {
        // UP
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (IsWalkable(0, 1))
            {
                if (currentAction == CharacterState.IDLE)
                {
                    currentAction = CharacterState.WALKING_TOP;
                }
            }
        }
        // DOWN
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (IsWalkable(0, -1))
            {
                if (currentAction == CharacterState.IDLE)
                {
                    currentAction = CharacterState.WALKING_BOT;
                }
            }
        }
        // RIGHT
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (IsWalkable(1, 0))
            {
                if (currentAction == CharacterState.IDLE)
                {
                    currentAction = CharacterState.WALKING_RIGHT;
                }
            }
        }
        // LEFT
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (IsWalkable(-1, 0))
            {
                if (currentAction == CharacterState.IDLE)
                {
                    currentAction = CharacterState.WALKING_LEFT;
                }
            }
        }
        // SPACE
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            string tmp = "";
            if (floor == 0)
            {
                System.Array.Reverse(Map.jagged);
                tmp = Map.jagged[positionY][positionX];
                System.Array.Reverse(Map.jagged);

                if (tmp == "0")
                {
                    
                }
            }
            if (floor == 1)
            {
                System.Array.Reverse(Map.jagged);
                tmp = Map.jagged2[positionY][positionX];
                System.Array.Reverse(Map.jagged);

                if (tmp == "0")
                {
                    System.Array.Reverse(Map.jagged);
                    if (Map.jagged[positionY][positionX] == "1")
                        Map.jagged[positionY][positionX] = "0";
                    if (Map.jagged[positionY][positionX] == "0")
                        Map.jagged[positionY][positionX] = "3";
                    System.Array.Reverse(Map.jagged);


                    if (!isTicking1)
                    {
                        timeToBreak1 = timebreak1;
                        isTicking1 = true;

                        cachedX1 = positionY;
                        cachedY1 = positionX;
                    }
                    else if (!isTicking2)
                    {
                        timeToBreak2 = timebreak2;
                        isTicking2 = true;

                        cachedX2 = positionY;
                        cachedY2 = positionX;
                    }
                    else if (!isTicking3)
                    {
                        timeToBreak3 = timebreak3;
                        isTicking3 = true;

                        cachedX3 = positionY;
                        cachedY3 = positionX;
                    }

                    System.Array.Reverse(Map.jagged2);
                    Map.jagged2[positionY][positionX] = "z";
                    System.Array.Reverse(Map.jagged2);

                                        

                    Map.DestroyCurrentWorld();
                    Map.CreateWorld_1(Map.jagged2);
                    
                }
            }
        }

        timeToBreak1 -= Time.deltaTime;
        if (timeToBreak1 < 0)
        {
            System.Array.Reverse(Map.jagged2);
            Map.jagged2[cachedX1][cachedY1] = "1";
            System.Array.Reverse(Map.jagged2);

            if (floor == 1)
            {
                Map.DestroyCurrentWorld();
                Map.CreateWorld_1(Map.jagged2);
            }

            timeToBreak1 = timebreak1;
            isTicking1 = false;
        }

        timeToBreak2 -= Time.deltaTime;
        if (timeToBreak2 < 0)
        {
            System.Array.Reverse(Map.jagged2);
            Map.jagged2[cachedX2][cachedY2] = "1";
            System.Array.Reverse(Map.jagged2);

            if (floor == 1)
            {
                Map.DestroyCurrentWorld();
                Map.CreateWorld_1(Map.jagged2);
            }

            timeToBreak2 = timebreak2;
            isTicking2 = false;
        }

        timeToBreak3 -= Time.deltaTime;
        if (timeToBreak3 < 0)
        {
            System.Array.Reverse(Map.jagged2);
            Map.jagged2[cachedX3][cachedY3] = "1";
            System.Array.Reverse(Map.jagged2);

            if (floor == 1)
            {
                Map.DestroyCurrentWorld();
                Map.CreateWorld_1(Map.jagged2);
            }

            timeToBreak3 = timebreak1;
            isTicking1 = false;
        }



        string tmp2 = "";
            System.Array.Reverse(Map.jagged);
            tmp2 = Map.jagged[positionY][positionX];
            System.Array.Reverse(Map.jagged);

        if (tmp2 == "e")
        {
            //WIN CONDITION
            Debug.Log("win!");
        }



        // STATES
        switch (currentAction)
        {
            case CharacterState.IDLE:
                {
                    Anim.SetBool("Walking_Top", false);
                    Anim.SetBool("Walking_Bot", false);
                    Anim.SetBool("Walking_Right", false);
                    Anim.SetBool("Walking_Left", false);

                    break;
                }
            case CharacterState.WALKING_TOP:
                {
                    if (!isMoving)
                    {
                        destination = new Vector3(transform.position.x + (0 * tileInternalDistance), transform.position.y + (1 * tileInternalDistance), 0);

                        isMoving = true;
                        Anim.SetBool("Walking_Top", true);
                        Anim.SetBool("Walking_Bot", false);
                        Anim.SetBool("Walking_Right", false);
                        Anim.SetBool("Walking_Left", false);
                    }
                    if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Walking Top"))
                    {
                        Sprite.flipX = false;
                    }

                    MoveTo(0, 1);
                    if (!isMoving)
                    {
                        if (floor == 0 && positionX == 0 && positionY == 7)
                            floor = 1;
                    }
                    break;
                }
            case CharacterState.WALKING_BOT:
                {
                    if (!isMoving)
                    {
                        destination = new Vector3(transform.position.x + (0 * tileInternalDistance), transform.position.y + (-1 * tileInternalDistance), 0);

                        isMoving = true;
                        Anim.SetBool("Walking_Bot", true);
                        Anim.SetBool("Walking_Top", false);
                        Anim.SetBool("Walking_Right", false);
                        Anim.SetBool("Walking_Left", false);
                    }
                    if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Walking Bot"))
                    {
                        Sprite.flipX = false;
                    }

                    MoveTo(0, -1);
                    if (!isMoving)
                    {
                        if (floor == 1 && positionX == 0 && positionY == 1)
                            floor = 0;
                    }
                    break;
                }
            case CharacterState.WALKING_LEFT:
                {
                    if (!isMoving)
                    {
                        destination = new Vector3(transform.position.x + (-1 * tileInternalDistance), transform.position.y + (0 * tileInternalDistance), 0);

                        isMoving = true;
                        Anim.SetBool("Walking_Left", true);
                        Anim.SetBool("Walking_Top", false);
                        Anim.SetBool("Walking_Right", false);
                        Anim.SetBool("Walking_Bot", false);
                    }
                    if (Anim.GetCurrentAnimatorStateInfo(0).IsName("Walking Left"))
                    {
                        Sprite.flipX = false;
                    }

                    MoveTo(-1, 0);

                    break;
                }
            case CharacterState.WALKING_RIGHT:
                {
                    if (!isMoving)
                    {
                        destination = new Vector3(transform.position.x + (1 * tileInternalDistance), transform.position.y + (0 * tileInternalDistance), 0);

                        isMoving = true;
                        Anim.SetBool("Walking_Right", true);
                        Anim.SetBool("Walking_Top", false);
                        Anim.SetBool("Walking_Bot", false);
                        Anim.SetBool("Walking_Left", false);
                        Sprite.flipX = true;
                    }

                    MoveTo(1, 0);
                    break;
                }
        }

    

    }

    void GetTileInternalDistance()
    {
        if (GameObject.Find("StartTile"))
        {
            tileInternalDistance = Vector3.Distance(GameObject.Find("StartTile").transform.position, GameObject.Find("ExitTile").transform.position);
            tileInternalDistance /= 13;
        }
    }

    void MoveTo(int x, int y)
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, destination, step);

        if (Vector3.Distance(transform.position, destination) == 0.0f)
        {
            currentAction = CharacterState.IDLE;
            destination = Vector3.zero;
            isMoving = false;
            if (x == 1)
                positionX++;
            if (x == -1)
                positionX--;
            if (y == 1)
                positionY++;
            if (y == -1)
                positionY--;

            Debug.Log(positionX + ", " + positionY);
        }
    }

    bool IsWalkable(int additionalY, int additionalX)
    {

        if (floor == 0)
        {
            System.Array.Reverse(Map.jagged);
            string tmp = Map.jagged[positionY + additionalX][positionX + additionalY];
            System.Array.Reverse(Map.jagged);

            Debug.Log(tmp);
            // From Ground to Stairs
            if ((tmp == "s" && positionX == 1) || (tmp == "v" && positionX == 1))
            {
                return false;
            }
            if ((positionX == 0 && positionY == 5 && tmp == "0") || (positionX == 0 && positionY == 6 && tmp == "0"))
            {
                return false;
            }
            if (tmp != "1")
                if (tmp != "o")
                    if (tmp != "x")
                        if (tmp != "w")
                        {
                            if (tmp != "3")
                            {
                                return true;
                            }
                        }
        }
        else
        {
            System.Array.Reverse(Map.jagged2);
            string tmp = Map.jagged2[positionY + additionalX][positionX + additionalY];
            System.Array.Reverse(Map.jagged2);

            if ((tmp == "s" && positionX == 1) || (tmp == "b" && positionX == 1))
            {
                return false;
            }
            if ((positionX == 0 && positionY == 3 && tmp == "0") || (positionX == 0 && positionY == 2 && tmp == "0"))
            {
                return false;
            }
            if (tmp != "1")
                if (tmp != "o")
                    if (tmp != "x")
                        if (tmp != "w")
                        {
                            if (tmp != "3")
                            {
                                return true;
                            }
                        }
        }
        return false;
    }
}
