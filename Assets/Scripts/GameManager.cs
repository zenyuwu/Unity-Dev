using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject titleUI;
    [SerializeField] TMP_Text livesUI;
    [SerializeField] TMP_Text timerUI;
    [SerializeField] Slider healthUI;
    [SerializeField] FloatVariable health;
    [SerializeField] GameObject respawn;

    [Header("Events")]
    //[SerializeField] IntEvent scoreEvent;
    [SerializeField] VoidEvent gameStartEvent;
    [SerializeField] GameObjectEvent respawnEvent;
    [SerializeField] GameObject gameWonUI;
    [SerializeField] GameObject gameLostUI;

    public enum State
    {
        TITLE,
        START_GAME,
        PLAY_GAME,
        GAME_WON,
        GAME_OVER
    }

    public State state = State.TITLE;
    public float timer = 0;
    public int lives = 0;
    public int Lives { 
        get { return lives; } 
        set { 
            lives = value; 
            livesUI.text = "LIVES: " + lives.ToString(); 
        } 
    }

    public float Timer
    {
        get { return timer; }
        set
        {
            timer = value;
            timerUI.text = string.Format("{0:F1}", timer);
        }
    }

    void Start()
    {
        //scoreEvent.Subscribe(OnAddPoints);
    }

    void Update()
    {
		switch (state)
		{
			case State.TITLE:
                titleUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                Lives = 3;
				break;
			case State.START_GAME:
                titleUI.SetActive(false);
                Timer = 60;
                health.value = 100;
				Cursor.lockState = CursorLockMode.Locked;
				Cursor.visible = false;
                gameStartEvent.RaiseEvent();
                respawnEvent.RaiseEvent(respawn);

				state = State.PLAY_GAME;
				break;
			case State.PLAY_GAME:
                Timer = Timer - Time.deltaTime;
                if(Timer <= 0)
                {
                    state = State.GAME_OVER;
                }
				break;
            case State.GAME_WON:
                gameWonUI.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
			case State.GAME_OVER:
				gameLostUI.SetActive(true);
				Cursor.lockState = CursorLockMode.None;
				Cursor.visible = true;
				break;
		}

        healthUI.value = health.value / 100.0f;
	}

    public void OnStartGame()
    {
        state = State.START_GAME;
    }

    public void OnAddPoints(int points)
    {
        print(points);
    }

    public void OnPlayerDead()
    {
        Lives--;
        if(Lives < 0)
        {
            state = State.GAME_OVER;
            return;
        }
        state = State.START_GAME;
    }

    public void OnGameWon()
    {
        state = State.GAME_WON;
    }

    public void OnAddTime()
    {
        Timer += 10;
    }
}
