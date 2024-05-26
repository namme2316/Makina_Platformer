using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameObject level1;
    [SerializeField] GameObject level2;
    [SerializeField] GameObject level3;
    [SerializeField] GameObject level4;
    [SerializeField] GameObject level5;
    [SerializeField] GameObject level6;

	[SerializeField] Button[] levelButtons;


	public static LevelManager Instance;



	
	void Start()
    {
		Check_level();
	}

    // Update is called once per frame
    void Update()
    {

	}

	public void Check_level()
	{
		for (int i = 0; i < levelButtons.Length; i++)
		{
			levelButtons[i].interactable = false;
			Debug.Log(levelButtons[i] + "is disabled");
		}

		for (int i = 0; i < LevelUnlocker.Instance.unlockedLevels; i++)
		{
			levelButtons[i].interactable = true;
			Debug.Log(levelButtons[i] + "is enabled");
		}
	}


	
	//open level info when button is clicked
	public void ShowLevel_1()
    {
        level1.SetActive(true);
    }

	public void ShowLevel_2()
	{
		level2.SetActive(true);
	}

	public void ShowLevel_3()
	{
		level3.SetActive(true);
	}

	public void ShowLevel_4()
	{
		level4.SetActive(true);
	}

	public void ShowLevel_5()
	{
		level5.SetActive(true);
	}

	public void ShowLevel_6()
	{
		level6.SetActive(true);
	}

	//close level info

	public void CloseLevel_1()
	{
		level1.SetActive(false);
	}

	public void CloseLevel_2()
	{
		level2.SetActive(false);
	}

	public void CloseLevel_3()
	{
		level3.SetActive(false);
	}

	public void CloseLevel_4()
	{
		level4.SetActive(false);
	}

	public void CloseLevel_5()
	{
		level5.SetActive(false);
	}

	public void CloseLevel_6()
	{
		level6.SetActive(false);
	}

	public void Reset()
	{
		LevelUnlocker.Instance.Reset_level();
	}

}
