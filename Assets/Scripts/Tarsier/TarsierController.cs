using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TarsierController : MonoBehaviour
{
   private Enemy health;
    private Animator anim;

	private void Awake()
	{
		health = GetComponent<Enemy>();
        anim = GetComponent<Animator>();
	}
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (health.currentHealth <= 0)
        {
            anim.SetBool("IsDead", true);
        }
    }
}
