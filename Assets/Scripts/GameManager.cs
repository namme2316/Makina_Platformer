using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource enemyDeathSound;
    [SerializeField] AudioSource shoot1;
    [SerializeField] AudioSource playerDeathSound;
    [SerializeField] AudioSource shoot2;
    [SerializeField] AudioSource healthPickUpSound;
    [SerializeField] AudioSource playerMeleeAttackSound;
    [SerializeField] AudioSource playerRangeAttackSound;
    [SerializeField] AudioSource enemyMeleeAttackSound;
    public static GameManager Instance;

    public event Action  OnPlayerWin;
    public event Action OnPlayerLose;


	private void OnEnable()
	{
		SingletonPatten();
	}

	private void Awake()
    {
        audioSource.Play();
    }


    private void SingletonPatten() 
    {
        if(Instance != null) 
        {
            Destroy(gameObject);
        }
        else 
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayerWin()
    {
        OnPlayerWin?.Invoke();
    }

    public void PlayerLose() 
    {
        OnPlayerLose.Invoke();
    }

    public void PlayMenuSound()
    {
        audioSource.Play();
    }

    public void StopMenuSound()
    {
        audioSource.Stop();
    }

    public void PlayShootSound1()
    {
        shoot1.Play();
    }

    public void PlayShootSound2() 
    {
        shoot2.Play();
    }

    public void HealthPickUpSound()
    {
        healthPickUpSound.Play();
    }

    public void PlayerDeathSound()
    {
        playerDeathSound.Play();
    }

    public void EnemyDeathSound() 
    {
        enemyDeathSound.Play();
    }

    public void EnemyMeleeAttackSound()
    {
        enemyMeleeAttackSound.Play();
    }

    public void PlayerMeleeAttackSound()
    {
        playerMeleeAttackSound.Play();
    }

    public void PlayerRangeAttackSound()
    {
        playerRangeAttackSound.Play();
    }


}
