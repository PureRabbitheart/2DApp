using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class NextScene : MonoBehaviour
{

    [SerializeField]
    private SceneObject TitleSceneObject;
    [SerializeField]
    private SceneObject[] StageSceneObject;
    [SerializeField]
    private SceneObject SettingSceneObject;
    [SerializeField]
    private SceneObject SelectSceneObject;
    [SerializeField]
    private SceneObject GachaSceneObject;
    [SerializeField]
    private SceneObject GachaPerfomanceSceneObject;
    [SerializeField]
    private SceneObject GaScene;


    public void TitleScene()//タイトルシーン遷移
    {
        LoadScene(TitleSceneObject.name(), 0.0f);
    }
    public void StageScene(int StageNum)//メインシーン遷移
    {
        LoadScene(StageSceneObject[StageNum - 1], 0.0f);
    }
    public void SettingScene()//設定シーン遷移
    {
        LoadScene(SettingSceneObject, 0.0f);
    }
    public void SelectScene()//セレクトシーン遷移
    {
        LoadScene(SelectSceneObject, 0.0f);
    }
    public void GachaScene()//ガチャシーン遷移
    {
        LoadScene(GachaSceneObject, 0.0f);
    }

    public void PerfomanceScene()//ガチャシーン遷移
    {
        LoadScene(GachaPerfomanceSceneObject, 0.0f);
    }

    public void GAScene()//タイトルシーン遷移
    {
        LoadScene(GaScene.name(), 0.0f);
    }

    public void LoadScene(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    private IEnumerator TransScene(string scene, float interval)
    {
        float time = 0;
        while (time <= interval)
        {
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切替 .
        SceneManager.LoadScene(scene);

        time = 0;
        while (time <= interval)
        {
            time += Time.deltaTime;
            yield return 0;
        }

    }
}
