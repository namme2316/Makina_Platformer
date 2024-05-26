using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameUIHandler : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject deathUI;
    [SerializeField] InputManager inputManager;
    [SerializeField] FloatValueSO playerHealth;

    private void Awake()
    {
        
    }

    void OnEnable()
    {
        inputManager.ResumeEvent += Resume;
        inputManager.PauseEvent += Pause;
    }

    private void OnDisable()
    {
        inputManager.ResumeEvent -= Resume;
        inputManager.PauseEvent -= Pause;
        GameManager.Instance.OnPlayerLose -= DeathUI;
    }


	private void Start()
	{
		GameManager.Instance.OnPlayerLose += DeathUI;
	}

	public void DeathUI()
    {
        deathUI.SetActive(true);
    }

    public void Resume()
    { 
        Time.timeScale = 1;
        inputManager.SetPlayer();
        pauseUI.SetActive(false);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pauseUI.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathUI.SetActive(false);
    }

    public void Menu()
    {
        Time.timeScale =1;
        SceneManager.LoadScene(1);
        inputManager.SetUI();
    }

    






}
