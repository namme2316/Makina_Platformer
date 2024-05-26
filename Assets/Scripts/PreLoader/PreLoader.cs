using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class PreLoader : MonoBehaviour
{
    [SerializeField] GameObject loadingScreen;
    [SerializeField] Image loadingFill;


	private void Start()
	{
		loadingFill.fillAmount = 0;
	}

	public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
			float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
			Debug.Log("Loading progress: " + progressValue);
			loadingFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
