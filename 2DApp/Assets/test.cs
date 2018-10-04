using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MainScene()
	{
		SceneManager.LoadScene("Main");
	}
	public void　SettingScene()
	{
		SceneManager.LoadScene("Setting");
	}	
	public void ExitScene()
	{
	　#if UNITY_EDITOR
   			EditorApplication.isPlaying = false;
      	 #elif UNITY_STANDALONE
    		Application.Quit();
   	 #endif
	}
}
