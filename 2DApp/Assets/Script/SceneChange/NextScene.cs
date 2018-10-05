using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour {

	[SerializeField]
	private SceneObject TitleSceneObject;
	[SerializeField]
	private SceneObject MainSceneObject;
	[SerializeField]
	private SceneObject SettingSceneObject;
	[SerializeField]
	private SceneObject SelectSceneObject;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TitleScene()//タイトルシーン遷移
	{
		SceneManager.LoadScene(TitleSceneObject);
	}
	public void MainScene()//メインシーン遷移
	{
		SceneManager.LoadScene(MainSceneObject);
	}
	public void　SettingScene()//設定シーン遷移
	{
		SceneManager.LoadScene(SettingSceneObject);
	}	
	public void SelectScene()//セレクトシーン遷移
	{
	　SceneManager.LoadScene(SelectSceneObject);
	}

}
