using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUnlocker : MonoBehaviour
{

	[SerializeField] public int unlockedLevels = 1;
	public static LevelUnlocker Instance;

	private void Awake()
	{
		SingletonPatten();
		LoadUnlockedLevels();
	}

	private void SingletonPatten()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	public void SaveUnlockedLevels()
	{
		PlayerPrefs.SetInt("UnlockedLevels", unlockedLevels);
		PlayerPrefs.Save();
	}

	private void LoadUnlockedLevels()
	{
		if (PlayerPrefs.HasKey("UnlockedLevels"))
		{
			unlockedLevels = PlayerPrefs.GetInt("UnlockedLevels");
		}
	}


	public void Reset_level()
	{
		PlayerPrefs.DeleteAll();
		unlockedLevels = 1;
		PlayerPrefs.Save();
		Debug.Log("Reset to 1");

	}
}
