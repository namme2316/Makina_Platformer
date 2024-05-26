using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamarawController : MonoBehaviour
{
    TamarawAnimation anim;
    Enemy enemyHealth;
	TamarawCombat combat;

	[SerializeField] LayerMask playerLayer;
	[SerializeField] GameObject player;

	private bool faceRight = true;


	enum State 
    {
        Idle,
        Charge,
        Drift,
        Death
    }

	private void Awake()
	{
		anim = GetComponent<TamarawAnimation>();
        enemyHealth = GetComponent<Enemy>();
		combat = GetComponent<TamarawCombat>();
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		combat.cooldownTimer += Time.deltaTime;
		TamarawBrain();
    }

    public void TamarawBrain()
    {
		State currentState = State.Idle;


		if (enemyHealth.currentHealth <= 0)
		{
			currentState = State.Death;
			Debug.Log("NO PROBLEM");
		}
		else if (combat.PlayerInSight() && combat.cooldownTimer >= combat.attackCooldown)
		{	
			currentState = State.Drift;
		}
		else if(combat.cooldownTimer < 0.1f)
		{
			currentState = State.Charge;
		}

		anim.SetState((int)currentState);
	}

	public void TamarawDrift() 
	{
		combat.cooldownTimer = 0;
	}


	public void Flip()
	{
		Vector3 currentScale = gameObject.transform.localScale;
		currentScale.x *= -1;
		gameObject.transform.localScale = currentScale;
		faceRight = !faceRight;

		
	}


}
