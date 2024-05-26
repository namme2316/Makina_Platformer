using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelection;
    [SerializeField] GameObject guidePanel;

    private void Awake()
    {
        GameManager.Instance.PlayMenuSound();
    }

    public void LevelSelection()
    {
        mainMenu.SetActive(false);
        levelSelection.SetActive(true);
    }

    public void MainMenu()
    {
        guidePanel.SetActive(false);
        levelSelection.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void GuidePanel()
    {
        guidePanel.SetActive(true);
    }

    public void ResetGame()
    {
        PlayerPrefs.DeleteAll();
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    // Load Levelss 

    public void Level_1() 
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Level_2() 
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Level_3() 
    {
        SceneManager.LoadScene("Level_3");
    }
    public void Level_4()
    {
        SceneManager.LoadScene("Level_4");
    }

	public void Level_5()
	{
		SceneManager.LoadScene("Level_5");
	}
	public void Level_6()
	{
		SceneManager.LoadScene("Level_6");
	}

    public void RoradShowLevel()
    {
		SceneManager.LoadScene("RoadShow Level");
	}

}
