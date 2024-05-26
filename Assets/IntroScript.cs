using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
public class IntroScript : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
			SceneManager.LoadScene("MenuScene");
		}
    }

    public void NextScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

}
