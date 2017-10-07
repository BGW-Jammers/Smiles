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

    public CharacterState currentAction;

    public int positionX = 0;
    public int positionY = 4;
    public int floor = 0;
    public int speed = 1;

    float tileInternalDistance = 0.0f;
    bool isMoving = false;

    Vector3 destination;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();
        transform.position = GameObject.Find("StartTile").transform.position;
        Map = GameObject.Find("GameManager").GetComponent<Mini_Game_Level_Loader>();

        positionX = 0;
        positionY = 4;

        GetTileInternalDistance();
    }

    // Update is called once per frame
    void Update()
    {
        // UP
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
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
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
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
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
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
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (IsWalkable(-1, 0))
            {
                if (currentAction == CharacterState.IDLE)
                {
                    currentAction = CharacterState.WALKING_LEFT;
                }
            }
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
                    }
                    MoveTo(0, 1);
                    if (!isMoving)
                    {
                        if (floor == 0 && positionX == 0 && positionY == 6)
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
                    }
                    MoveTo(0, -1);
                    break;
                }
            case CharacterState.WALKING_LEFT:
                {
                    if (!isMoving)
                    {
                        destination = new Vector3(transform.position.x + (-1 * tileInternalDistance), transform.position.y + (0 * tileInternalDistance), 0);

                        isMoving = true;
                        Anim.SetBool("Walking_Left", true);
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
            string tmp = Map.jagged[positionY + additionalX][positionX + additionalY];
            Debug.Log(tmp);
            if (tmp != "1")// || tmp != "o"|| tmp != "x" || tmp != "w")// || (tmp == "s" && positionX != 0))
                if (tmp != "o")
                    if (tmp != "x")
                        if (tmp != "w")
                        {
                            return true;
                        }
        }
        else
        {
            string tmp = Map.jagged2[positionY + additionalX][positionX + additionalY];

            if (tmp != "1")
                if (tmp != "o")
                    if (tmp != "x")
                        if (tmp != "w")
                        {
                            return true;
                        }
        }
        return false;
    }
}
