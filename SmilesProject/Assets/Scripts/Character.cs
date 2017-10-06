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

    Animator Anim = null;

    public CharacterState currentAction;

    public int positionX = 0;
    public int positionY = 0;

    // Use this for initialization
    void Start ()
    {
        Anim = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        // UP
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            currentAction = CharacterState.WALKING_TOP;
        }
        // DOWN
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            currentAction = CharacterState.WALKING_TOP;
        }
        // RIGHT
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            currentAction = CharacterState.WALKING_RIGHT;
        }
        // LEFT
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            currentAction = CharacterState.WALKING_LEFT;
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
                    Anim.SetBool("Walking_Top", true);
                    break;
                }
            case CharacterState.WALKING_BOT:
                {
                    Anim.SetBool("Walking_Bot", true);
                    break;
                }
            case CharacterState.WALKING_LEFT:
                {
                    Anim.SetBool("Walking_Left", true);
                    break;
                }
            case CharacterState.WALKING_RIGHT:
                {
                    Anim.SetBool("Walking_Right", true);
                    break;
                }

        }
    }

    void MoveTo (int positionX, int positionY)
    {
        
    }

    bool IsWalkable (int positionX, int positionY)
    {
        return true;
    }
}
