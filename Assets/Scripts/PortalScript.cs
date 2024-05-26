using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;

public class PortalScript : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D col;
    [SerializeField] float loadTime = 2f;
    private Animator anim;
    [SerializeField] bool CanGoToNextLevel = true;
    [SerializeField] bool IsActive = false;
    [SerializeField] string levelToLoad;
    [SerializeField] PreLoader preloadManager;
    [SerializeField] int currentLevel;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    
    

    private void OnDisable()
    {
        GameManager.Instance.OnPlayerWin -= PlayerWin;
    }



    void Start()
    {
        if (IsActive)
        {
			spriteRenderer.enabled = true;
			anim.enabled = true;
            if (CanGoToNextLevel)
            {
				col.enabled = true;
			}
		}
        else
        {
			spriteRenderer.enabled = false;	
			anim.enabled = false;
			col.enabled = false;
			
		} 
		GameManager.Instance.OnPlayerWin += PlayerWin;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerWin()
    {
		if (CanGoToNextLevel)
		{
		    col.enabled = true;
		}
		spriteRenderer.enabled = true;
        anim.enabled = true;
        anim.SetBool("portalAppear", true);
    }

    private void SwitchToIdleAnim()
    {
        anim.SetBool("portalAppear", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player")) 
        {
            UnlockLevel();
            Invoke("LoadNextLevel", loadTime);
        }
    }

    public void LoadNextLevel()
    {
        preloadManager.LoadScene(levelToLoad);
    }

    public void UnlockLevel()
    {
        if(LevelUnlocker.Instance.unlockedLevels <= currentLevel)
        {
            LevelUnlocker.Instance.unlockedLevels++;
            LevelUnlocker.Instance.SaveUnlockedLevels();
        }
    }

}
