using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] FloatVariable health;
    [SerializeField] PhysicsCharacterController characterController;

    [Header("Events")]
    [SerializeField] IntEvent scoreEvent = default;
    [SerializeField] VoidEvent gameStartEvent = default;
    [SerializeField] VoidEvent playerDeadEvent = default;
    //my stuff :(
    [SerializeField] GameObjectEvent respawnEvent = default;

    private int score = 0;
    public int Score {  
        get { return score; } 
        set { 
            score = value; 
            scoreText.text = score.ToString();
            scoreEvent.RaiseEvent(score);
        } 
    }

	private void Start()
	{

	}
	public void AddPoints(int points)
    {
        Score += points;
    }

	private void OnEnable()
	{
        gameStartEvent.Subscribe(OnStartGame);
        //my line :(
        respawnEvent.Subscribe(OnRespawn);
	}

    private void OnStartGame()
    {
        characterController.enabled = true;
    }

	public void TakeDamage(float damage)
	{
		health.value -= damage;
        if (health.value <= 0)
        {
            playerDeadEvent.RaiseEvent();
        }
	}

    public void OnRespawn(GameObject respawn)
    {
        transform.position = respawn.transform.position;
        transform.rotation = respawn.transform.rotation;

        characterController.Reset();
    }
}
