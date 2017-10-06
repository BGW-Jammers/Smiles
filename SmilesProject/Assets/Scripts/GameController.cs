using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    PLAY,
    TRANSITION_TO_PLAY,
    PAUSE,
    TRANSITION_TO_PAUSE,
    END,
    MENU
}

public class GameController : MonoBehaviour
{
    // UI

    // General Values
    public GameState currentState;
    private bool tickSkipped;
    private float scale;

    // Player

    // Camera
    public Camera cam;

    void Start()
    {
        Restart();
    }

    void Update()
    {
        scale = transform.lossyScale.y;
        //Debug.Log(scale);

        switch (currentState)
        {
            case GameState.PLAY:
                {
                    /*if (nexusHealth > 0f)
                        HandleLoop();*/
                    break;
                }
            case GameState.TRANSITION_TO_PLAY:
                {
                    if (tickSkipped)
                        currentState = GameState.PLAY;
                    else
                        tickSkipped = !tickSkipped;

                    break;
                }
            case GameState.PAUSE:
                {
                    /*if (trackingImages[card0])
                    {
                        warning.SetActive(false);
                    }*/

                    break;
                }
            case GameState.TRANSITION_TO_PAUSE:
                {
                    if (tickSkipped)
                        currentState = GameState.PAUSE;
                    else
                        tickSkipped = !tickSkipped;

                    break;
                }
            default: return;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        /*if (other.tag == "Bullet")
        {
            SpawnExplosion(other.transform);
            Destroy(other.gameObject);
        }*/
    }

    private void Restart()
    {
        // Reset UI to menu
        currentState = GameState.MENU;
        /*startButton.SetActive(true);
        startPanel.SetActive(true);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        restartButton.SetActive(false);
        deathPanel.SetActive(false);*/
    }


    private void HandleLoop()
    {
        if (Input.touchCount > 0)
        {
            /*Vector3 aim = cam.transform.forward;
            aim.x = Input.GetTouch(0).position.x;
            aim.y = Input.GetTouch(0).position.y;

            ProjectileController proj = projectileSpawner.SpawnProjectile(cam.transform, aim, scale);
            proj.SetAttributes(this, projectileSpeed, null, damage);*/
        }
        else if (Input.GetMouseButtonDown(0))
        {
            /*ProjectileController proj = projectileSpawner.SpawnProjectile(cam.transform, Input.mousePosition, scale);
            proj.SetAttributes(this, projectileSpeed, null, damage);*/
        }

        /*spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnFrequency)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }*/
    }

    // Utility ===================================================

    public GameState GetState()
    {
        return currentState;
    }

    public float GetScale()
    {
        return scale;
    }

    // UI ===================================================

    public void StartGame()
    {
        /*startButton.SetActive(false);
        nexusLife.transform.parent.gameObject.SetActive(true);
        currentState = GameState.TRANSITION_TO_PLAY;
        tickSkipped = false;

        killCount = 0;
        killCountLabel.text = killCount.ToString();*/
    }

    public void Pause()
    {
        /*pauseButton.SetActive(false);
        resumeButton.SetActive(true);
        restartButton.SetActive(true);
        pausePanel.SetActive(true);
        currentState = GameState.TRANSITION_TO_PAUSE;
        tickSkipped = false;*/
    }

    public void Resume()
    {
        /*restartButton.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        currentState = GameState.TRANSITION_TO_PLAY;
        tickSkipped = false;*/
    }

    public void Reset()
    {
        /*restartButton.SetActive(false);
        resumeButton.SetActive(false);
        pauseButton.SetActive(false);
        startButton.SetActive(true);
        tickSkipped = false;
        Restart();*/
    }

    public void Exit()
    {
        Application.Quit();
    }
}